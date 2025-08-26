using AutoMapper;
using Bmg.Application.Exceptions;
using Bmg.Application.Repositories;
using Bmg.Application.Services.Users.Models;
using Bmg.Domain;
using FluentValidation;

namespace Bmg.Application.Services.Users;

public class UserService(IMapper mapper, IValidator<CreateUserRequest> validator, IUserRepository userRepository) : IUserService
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IValidator<CreateUserRequest> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<CreatedUserResponse> CreateUserAsync(CreateUserRequest createUserRequest, UserRole? userRole = UserRole.Customer)
    {
        await _validator.ValidateAndThrowAsync(createUserRequest);

        var existingUser = await _userRepository.GetByEmailAsync(createUserRequest.Email);
        if (existingUser != null)
            throw new BusinessErrorException($"Já existe um usuário com o email {createUserRequest.Email}.");

        var userEntity = _mapper.Map<UserEntity>(createUserRequest);
        userEntity.Role = userRole ?? UserRole.Customer;
        userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserRequest.Password);

        await _userRepository.AddAsync(userEntity);

        var response = _mapper.Map<CreatedUserResponse>(userEntity);
        return response;
    }

    public async Task<VerifyCredentialsResponse?> VerifyCredentialsAsync(AuthRequest authRequest)
    {
        var user = await _userRepository.GetByEmailAsync(authRequest.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Credenciais inválidas");

        var response = _mapper.Map<VerifyCredentialsResponse>(user);
        return response;
    }
}
