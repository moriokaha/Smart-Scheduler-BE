﻿using SmartScheduler.Data.Models;

namespace SmartScheduler.Services.Contracts
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        Task<User?> RegisterUserAsync(UserDto request);
    }
}
