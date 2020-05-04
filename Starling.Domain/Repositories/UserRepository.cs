using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Starling.Domain.Enums;
using Starling.Domain.Models;
using Starling.Domain.Repositories.Contracts;
using Starling.Shared.Layout.Database;

namespace Starling.Domain.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<User> FirstOrDefaultAsync(string username, CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<User>(Functions.GetUser, new
            {
                _username = username
            });
        }

        public async Task<bool> IsUserExists(string username, string password, CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<bool>(Functions.IsUserExists, new
            {
                _username = username,
                _password = password
            });
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await QueryFirstOrDefaultAsync<string>(Functions.AddUser, new
            {
                username = user.Username,
                password = user.Password,
                public_key = user.PublicKey,
                private_key = user.PrivateKey,
                private_key_password = user.PrivateKeyPassword
            });
        }

        public async Task AssignFileAsync(string username, Guid fileId, UserFileStatus status,CancellationToken cancellationToken = default)
        {
            await QueryFirstOrDefaultAsync<int>(Functions.AddUserFile, new
            {
                _username = username,
                _file_id = fileId,
                _status = status
            });
        }
        
        public async Task<IEnumerable<User>> GetUsersAsync(string requester, string search, int take)
        {
            return await QueryAsync<User>(Functions.GetUsers, new
            {
                _requester = requester,
                _search = search,
                _take = take
            });
        }

        public async Task<bool> ValidateRefreshToken(string username, string value)
        {
            return await QueryFirstOrDefaultAsync<bool>(Functions.ValidateRefreshToken, new
            {
                _username = username,
                _value = value
            });
        }

        public async Task<bool> AddRefreshToken(string username, string value)
        {
            return await QueryFirstOrDefaultAsync<bool>(Functions.AddRefreshToken, new
            {
                _username = username,
                _value = value
            });
        }

        public async Task<bool> DeleteRefreshToken(string username, string value)
        {
            return await QueryFirstOrDefaultAsync<bool>(Functions.DeleteRefreshToken, new
            {
                _username = username,
                _value = value
            });
        }
    }
}