using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Starling.Application.Contracts;
using Starling.Application.DTOs;
using Starling.Domain.Repositories.Contracts;
using Starling.Shared.Utilities;

namespace Starling.Application.Requests.Users.Commands.RefreshAuthorizationToken
{
    public class RefreshAuthorizationTokenCommandHandler : IRequestHandler<RefreshAuthorizationTokenCommand, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationOptions _authorizationOptions;

        public RefreshAuthorizationTokenCommandHandler(IUserRepository userRepository, IAuthorizationOptions authorizationOptions)
        {
            _userRepository = userRepository;
            _authorizationOptions = authorizationOptions;
        }
        
        public async Task<TokenDto> Handle(RefreshAuthorizationTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _authorizationOptions.SymmetricSecurityKey,
                ValidateLifetime = false 
            };
            var principal = tokenHandler.ValidateToken(request.Token, tokenValidationParameters, out var securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var username = principal.Identity.Name;
            var isValidRefreshToken = await _userRepository.ValidateRefreshToken(username, request.RefreshToken);
            if (!isValidRefreshToken) throw new Exception();

            var tokenOptions = new JwtSecurityToken(
                _authorizationOptions.Issuer,
                _authorizationOptions.Audience,
                principal.Claims,
                expires: DateTime.Now.AddMinutes(_authorizationOptions.Lifetime),
                signingCredentials: new SigningCredentials(_authorizationOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            var refreshToken = StringUtility.GetRandomStringKey();
            await _userRepository.AddRefreshToken(username, refreshToken);

            return new TokenDto
            {
                Token = tokenHandler.WriteToken(tokenOptions),
                RefreshToken = refreshToken,
                ExpirationDate = tokenOptions.ValidTo
            };
        }
    }
}