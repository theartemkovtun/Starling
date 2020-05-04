using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Starling.Application.Contracts;
using Starling.Application.DTOs;
using Starling.Domain.Repositories.Contracts;
using Starling.Shared.Extensions;
using Starling.Shared.Utilities;

namespace Starling.Application.Requests.Users.Commands.AuthorizeUser
{
    public class AuthorizeUserCommandHandler : IRequestHandler<AuthorizeUserCommand, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationOptions _authorizationOptions;
        private readonly IValidator<AuthorizeUserCommand> _validator;

        public AuthorizeUserCommandHandler(IUserRepository userRepository, IAuthorizationOptions authorizationOptions, IValidator<AuthorizeUserCommand> validator)
        {
            _userRepository = userRepository;
            _authorizationOptions = authorizationOptions;
            _validator = validator;
        }
        
        public async Task<TokenDto> Handle(AuthorizeUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, null, cancellationToken);
            
            var tokenOptions = new JwtSecurityToken(
                _authorizationOptions.Issuer,
                _authorizationOptions.Audience,
                new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, request.Username)
                },
                expires: DateTime.Now.AddMinutes(_authorizationOptions.Lifetime),
                signingCredentials: new SigningCredentials(_authorizationOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );
            
            var token = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                ExpirationDate = tokenOptions.ValidTo
            };

            if (request.Remember)
            {
                var refreshToken = StringUtility.GetRandomStringKey();
                await _userRepository.AddRefreshToken(request.Username, refreshToken);
                token.RefreshToken = refreshToken;
            }

            return token;
        }
    }
}