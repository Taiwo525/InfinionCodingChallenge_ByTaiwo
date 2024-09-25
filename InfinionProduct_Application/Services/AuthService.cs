using InfinionProduct_Application.DTOs.AuthDTOs;
using InfinionProduct_Application.Interfaces;
using InfinionProduct_Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;




namespace InfinionProduct_Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IJwtTokenGenerator jwtTokenGenerator, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
        }
        public async Task<AuthResponseDto> RegisterAsync(RegistrationRequestDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var confirmationToken = await GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"{_configuration["AppUrl"]}/confirm-email?userId={user.Id}&token={confirmationToken}";

                await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your email by clicking this link: {confirmationLink}");

                return new AuthResponseDto { Success = true, Message = "User created successfully. Please check your email for confirmation." };
            }
            var errors = result.Errors.Select(e => e.Description);
            return new AuthResponseDto { Success = false, Message = "User creation failed." };
            
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResponseDto { Success = false, Message = "Invalid email or password." };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return new AuthResponseDto { Success = false, Message = "Invalid email or password." };
            }

            if (!user.EmailConfirmed)
            {
                return new AuthResponseDto { Success = false, Message = "Email not confirmed. Please check your email for the confirmation link." };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthResponseDto { Success = true, Token = token };
            
        }

        public async Task<AuthResponseDto> ConfirmEmailAsync(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new AuthResponseDto { Success = false, Message = "User not found." };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new AuthResponseDto { Success = true, Message = "Email confirmed successfully." };
            }

            return new AuthResponseDto { Success = false, Message = "Email confirmation failed." };
            
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

    }
}

