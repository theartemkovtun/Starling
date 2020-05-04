using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Starling.Domain.Enums;
using Starling.Domain.Models;
using Starling.Domain.Models.Filters;

namespace Starling.Domain.Repositories.Contracts
{
    public interface IShareRepository
    {
        Task<Guid> AddShareAsync(string title, string description, CancellationToken cancellationToken = default);
        Task<Guid> AddShareUserAsync(Guid shareId, string username, ShareUserOwnershipStatus ownershipStatus, CancellationToken cancellationToken = default);
        Task<Guid> AddShareFileAsync(Guid shareId, Guid fileId, CancellationToken cancellationToken = default);
        Task<Guid> AddShareFileUserAsync(Guid shareId, Guid fileId, string username, string signature, CancellationToken cancellationToken = default);
        Task<IEnumerable<ShareWithTotal>> GetShares(GetSharesFilter filter);
        Task<ShareForUser> GetShareForUser(string username, Guid shareId);
        Task<IEnumerable<ShareFileSigning>> GetShareFilesSigning(string username, Guid shareId);
        Task<IEnumerable<ShareUserInfo>> ShareUserApprovements(Guid shareId);
        Task AcceptAsync(Guid shareId, string username);
        Task RejectAsync(Guid shareId, string username);
        Task<int> GetFilesQuantity(Guid shareId);
        Task<int> GetUsersQuantity(Guid shareId);
        Task<IEnumerable<SignatureVerification>> GetSignatureVerificationData(Guid shareId, string username);
        Task<bool> DeleteAsync(Guid id);
    }
}