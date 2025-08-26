using AutoMapper;
using Bmg.Application.Exceptions;
using Bmg.Application.Integrations.Payment;
using Bmg.Application.Integrations.Payment.Models;
using Bmg.Application.Repositories;
using Bmg.Application.Services.Payments.Models;
using Bmg.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Bmg.Application.Services.Payments;

public class PaymentService(
    ILogger<PaymentService> logger,
    IMapper mapper,
    IValidator<PayByCreditCardRequest> ccValidator,
    IValidator<PayByPixRequest> pixValidator,
    IPaymentGatewayFactory gatewayFactory,
    IPaymentRepository paymentRepository,
    ICartRepository cartRepository,
    IUnitOfWork unitOfWork) : IPaymentService
{
    private readonly ILogger<PaymentService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IValidator<PayByCreditCardRequest> _ccValidator = ccValidator ?? throw new ArgumentNullException(nameof(ccValidator));
    private readonly IValidator<PayByPixRequest> _pixValidator = pixValidator ?? throw new ArgumentNullException(nameof(pixValidator));
    private readonly IPaymentGatewayFactory _gatewayFactory = gatewayFactory ?? throw new ArgumentNullException(nameof(gatewayFactory));
    private readonly IPaymentRepository _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
    private readonly ICartRepository _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task PayAsync(Guid userId, PayRequest payRequest)
    {
        await ValidateAndThrowAsync(payRequest);

        await _unitOfWork.ExecuteAsync(async () =>
        {
            var cart = await _cartRepository.GetCurrentAsync(userId) ??
                throw new NotFoundException("Carrinho não encontrado.");

            var totalAmount = cart.Items.Sum(item => item.Product?.Price * item.Quantity) ?? 0;
            if (cart.Items.Count == 0 || totalAmount == 0)
                throw new BusinessErrorException("Carrinho está vazio.");

            var paymentResponse = await ProcessPaymentAsync(payRequest, totalAmount);

            var paymentEntity = new PaymentEntity
            {
                CartId = cart.Id,
                Amount = totalAmount,
                Method = payRequest.Method,
                Status = paymentResponse.Status ? PaymentStatus.Approved : PaymentStatus.Rejected,
                TransactionId = paymentResponse.TransactionId,
            };

            await _paymentRepository.AddAsync(paymentEntity);
        });
    }

    private async Task ValidateAndThrowAsync(PayRequest payRequest)
    {
        switch (payRequest)
        {
            case PayByCreditCardRequest ccRequest:
                await _ccValidator.ValidateAndThrowAsync(ccRequest);
                break;

            case PayByPixRequest pixRequest:
                await _pixValidator.ValidateAndThrowAsync(pixRequest);
                break;

            default:
                throw new BusinessErrorException("Tipo de pagamento inválido.");
        }
    }

    private async Task<PaymentResponse> ProcessPaymentAsync(PayRequest payRequest, decimal totalAmount)
    {
        try
        {
            switch (payRequest)
            {
                case PayByCreditCardRequest ccRequest:
                    var ccModel = _mapper.Map<CreditCardPaymentRequest>(ccRequest);
                    ccModel.Amount = totalAmount;

                    var ccResponse = await _gatewayFactory
                        .GetGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>(PaymentMethod.CreditCard)
                        .ProcessPaymentAsync(ccModel);
                    return ccResponse;

                case PayByPixRequest pixRequest:
                    var pixModel = _mapper.Map<PixPaymentRequest>(pixRequest);
                    pixModel.Amount = totalAmount;

                    var pixResponse = await _gatewayFactory
                        .GetGateway<PixPaymentRequest, PixPaymentResponse>(PaymentMethod.Pix)
                        .ProcessPaymentAsync(pixModel);
                    return pixResponse;

                default:
                    throw new BusinessErrorException("Tipo de pagamento inválido.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao processar pagamento.");
            throw new BusinessErrorException($"Falha ao processar pagamento.");
        }
    }
}
