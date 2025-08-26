using AutoMapper;
using Bmg.Application.Exceptions;
using Bmg.Application.Integrations.Payment;
using Bmg.Application.Integrations.Payment.Models;
using Bmg.Application.Repositories;
using Bmg.Application.Services.Payments;
using Bmg.Application.Services.Payments.Models;
using Bmg.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace Bmg.Test;

public class PaymentServiceTests
{
    private readonly Mock<ILogger<PaymentService>> _logger = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IValidator<PayByCreditCardRequest>> _ccValidator = new();
    private readonly Mock<IValidator<PayByPixRequest>> _pixValidator = new();
    private readonly Mock<IPaymentGatewayFactory> _gatewayFactory = new();
    private readonly Mock<IPaymentRepository> _paymentRepository = new();
    private readonly Mock<ICartRepository> _cartRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    private PaymentService CreateService() =>
        new(
            _logger.Object,
            _mapper.Object,
            _ccValidator.Object,
            _pixValidator.Object,
            _gatewayFactory.Object,
            _paymentRepository.Object,
            _cartRepository.Object,
            _unitOfWork.Object);

    [Fact]
    public async Task PayAsync_ShouldProcessCreditCardPayment_WhenValidAndApproved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cart = new CartEntity
        {
            UserId = userId,
            Items =
            [
                new CartItemEntity
                {
                    Product = new ProductEntity { Price = 100 },
                    Quantity = 2
                }
            ]
        };

        _cartRepository.Setup(r => r.GetCurrentAsync(userId)).ReturnsAsync(cart);

        var ccRequest = new PayByCreditCardRequest { };
        var ccModel = new CreditCardPaymentRequest { Amount = 200 };
        var ccResponse = new CreditCardPaymentResponse(true, Guid.NewGuid());

        _mapper.Setup(m => m.Map<CreditCardPaymentRequest>(ccRequest)).Returns(ccModel);

        _gatewayFactory.Setup(f => f
            .GetGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>(PaymentMethod.CreditCard))
            .Returns(Mock.Of<IPaymentGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>>(g =>
                g.ProcessPaymentAsync(ccModel) == Task.FromResult(ccResponse)));

        _unitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
            .Returns<Func<Task>>(f => f());

        var service = CreateService();

        // Act
        await service.PayAsync(userId, ccRequest);

        // Assert
        _paymentRepository.Verify(r => r.AddAsync(It.Is<PaymentEntity>(p =>
            p.Status == PaymentStatus.Approved &&
            p.Amount == 200 &&
            p.Method == PaymentMethod.CreditCard)), Times.Once);
    }

    [Fact]
    public async Task PayAsync_ShouldProcessCreditCardPayment_WhenValidAndRejected()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cart = new CartEntity
        {
            UserId = userId,
            Items =
            [
                new CartItemEntity
                {
                    Product = new ProductEntity { Price = 100 },
                    Quantity = 2
                }
            ]
        };

        _cartRepository.Setup(r => r.GetCurrentAsync(userId)).ReturnsAsync(cart);

        var ccRequest = new PayByCreditCardRequest { };
        var ccModel = new CreditCardPaymentRequest { Amount = 200 };
        var ccResponse = new CreditCardPaymentResponse(false, Guid.NewGuid());

        _mapper.Setup(m => m.Map<CreditCardPaymentRequest>(ccRequest)).Returns(ccModel);

        _gatewayFactory.Setup(f => f
            .GetGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>(PaymentMethod.CreditCard))
            .Returns(Mock.Of<IPaymentGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>>(g =>
                g.ProcessPaymentAsync(ccModel) == Task.FromResult(ccResponse)));

        _unitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
            .Returns<Func<Task>>(f => f());

        var service = CreateService();

        // Act
        await service.PayAsync(userId, ccRequest);

        // Assert
        _paymentRepository.Verify(r => r.AddAsync(It.Is<PaymentEntity>(p =>
            p.Status == PaymentStatus.Rejected &&
            p.Amount == 200 &&
            p.Method == PaymentMethod.CreditCard)), Times.Once);
    }

    [Fact]
    public async Task PayAsync_ShouldProcessPixPayment_WhenValidAndApproved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cart = new CartEntity
        {
            UserId = userId,
            Items =
            [
                new CartItemEntity
                {
                    Product = new ProductEntity { Price = 50 },
                    Quantity = 2
                }
            ]
        };

        _cartRepository.Setup(r => r.GetCurrentAsync(userId)).ReturnsAsync(cart);

        var pixRequest = new PayByPixRequest { };
        var pixModel = new PixPaymentRequest { Amount = 100 };
        var pixResponse = new PixPaymentResponse(true, Guid.NewGuid());

        _mapper.Setup(m => m.Map<PixPaymentRequest>(pixRequest)).Returns(pixModel);

        _gatewayFactory.Setup(f => f
            .GetGateway<PixPaymentRequest, PixPaymentResponse>(PaymentMethod.Pix))
            .Returns(Mock.Of<IPaymentGateway<PixPaymentRequest, PixPaymentResponse>>(g =>
                g.ProcessPaymentAsync(pixModel) == Task.FromResult(pixResponse)));

        _unitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
            .Returns<Func<Task>>(f => f());

        var service = CreateService();

        // Act
        await service.PayAsync(userId, pixRequest);

        // Assert
        _paymentRepository.Verify(r => r.AddAsync(It.Is<PaymentEntity>(p =>
            p.Status == PaymentStatus.Approved &&
            p.Amount == 100 &&
            p.Method == PaymentMethod.Pix)), Times.Once);
    }

    [Fact]
    public async Task PayAsync_ShouldProcessPixPayment_WhenValidAndRejected()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cart = new CartEntity
        {
            UserId = userId,
            Items =
            [
                new CartItemEntity
                {
                    Product = new ProductEntity { Price = 50 },
                    Quantity = 2
                }
            ]
        };

        _cartRepository.Setup(r => r.GetCurrentAsync(userId)).ReturnsAsync(cart);

        var pixRequest = new PayByPixRequest { };
        var pixModel = new PixPaymentRequest { Amount = 100 };
        var pixResponse = new PixPaymentResponse(false, Guid.NewGuid());

        _mapper.Setup(m => m.Map<PixPaymentRequest>(pixRequest)).Returns(pixModel);

        _gatewayFactory.Setup(f => f
            .GetGateway<PixPaymentRequest, PixPaymentResponse>(PaymentMethod.Pix))
            .Returns(Mock.Of<IPaymentGateway<PixPaymentRequest, PixPaymentResponse>>(g =>
                g.ProcessPaymentAsync(pixModel) == Task.FromResult(pixResponse)));

        _unitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
            .Returns<Func<Task>>(f => f());

        var service = CreateService();

        // Act
        await service.PayAsync(userId, pixRequest);

        // Assert
        _paymentRepository.Verify(r => r.AddAsync(It.Is<PaymentEntity>(p =>
            p.Status == PaymentStatus.Rejected &&
            p.Amount == 100 &&
            p.Method == PaymentMethod.Pix)), Times.Once);
    }

    [Fact]
    public async Task PayAsync_ShouldThrowBusinessError_WhenPixGatewayFails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cart = new CartEntity
        {
            UserId = userId,
            Items =
            [
                new CartItemEntity
                {
                    Product = new ProductEntity { Price = 100 },
                    Quantity = 1
                }
            ]
        };

        _cartRepository.Setup(r => r.GetCurrentAsync(userId)).ReturnsAsync(cart);

        var pixRequest = new PayByPixRequest { };
        var pixModel = new PixPaymentRequest { Amount = 100 };

        _mapper.Setup(m => m.Map<PixPaymentRequest>(pixRequest)).Returns(pixModel);

        var mockGateway = new Mock<IPaymentGateway<PixPaymentRequest, PixPaymentResponse>>();
        mockGateway.Setup(g => g.ProcessPaymentAsync(It.IsAny<PixPaymentRequest>()))
                .ThrowsAsync(new Exception("Gateway Pix error"));

        _gatewayFactory.Setup(f => f
            .GetGateway<PixPaymentRequest, PixPaymentResponse>(PaymentMethod.Pix))
            .Returns(mockGateway.Object);

        _unitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
            .Returns<Func<Task>>(f => f());

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<BusinessErrorException>(() => service.PayAsync(userId, pixRequest));
    }

    [Fact]
    public async Task PayAsync_ShouldThrow_WhenCartNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _cartRepository.Setup(r => r.GetCurrentAsync(userId)).ReturnsAsync((CartEntity?)null);

        var ccRequest = new PayByCreditCardRequest { };

        _unitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
            .Returns<Func<Task>>(f => f());

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => service.PayAsync(userId, ccRequest));
    }

    [Fact]
    public async Task PayAsync_ShouldThrow_WhenCartIsEmpty()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cart = new CartEntity { UserId = userId, Items = [] };
        _cartRepository.Setup(r => r.GetCurrentAsync(userId)).ReturnsAsync(cart);

        var ccRequest = new PayByCreditCardRequest { };

        _unitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
            .Returns<Func<Task>>(f => f());

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<BusinessErrorException>(() => service.PayAsync(userId, ccRequest));
    }

    [Fact]
    public async Task PayAsync_ShouldThrowBusinessError_WhenGatewayFails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cart = new CartEntity
        {
            UserId = userId,
            Items =
            [
                new CartItemEntity
                {
                    Product = new ProductEntity { Price = 100 },
                    Quantity = 1
                }
            ]
        };

        _cartRepository.Setup(r => r.GetCurrentAsync(userId)).ReturnsAsync(cart);

        var ccRequest = new PayByCreditCardRequest { };
        var ccModel = new CreditCardPaymentRequest { Amount = 100 };

        _mapper.Setup(m => m.Map<CreditCardPaymentRequest>(ccRequest)).Returns(ccModel);

        var mockGateway = new Mock<IPaymentGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>>();
        mockGateway.Setup(g => g.ProcessPaymentAsync(It.IsAny<CreditCardPaymentRequest>()))
                   .ThrowsAsync(new Exception("Gateway error"));

        _gatewayFactory.Setup(f => f
            .GetGateway<CreditCardPaymentRequest, CreditCardPaymentResponse>(PaymentMethod.CreditCard))
            .Returns(mockGateway.Object);

        _unitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
            .Returns<Func<Task>>(f => f());

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<BusinessErrorException>(() => service.PayAsync(userId, ccRequest));
    }
}
