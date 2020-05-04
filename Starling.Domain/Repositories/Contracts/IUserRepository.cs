using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Starling.Domain.Enums;
using Starling.Domain.Models;

namespace Starling.Domain.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User> FirstOrDefaultAsync(string username, CancellationToken cancellationToken = default);
        Task<bool> IsUserExists(string username, string password, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task AssignFileAsync(string username, Guid fileId, UserFileStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetUsersAsync(string requester, string search, int take);
        Task<bool> ValidateRefreshToken(string username, string value);
        Task<bool> AddRefreshToken(string username, string value);
        Task<bool> DeleteRefreshToken(string username, string value);
    }
}