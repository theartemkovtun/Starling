using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Starling.Domain.Enums;
using Starling.Domain.Models;
using Starling.Domain.Models.Filters;
using Starling.Domain.Repositories.Contracts;
using Starling.Shared.Layout.Database;

namespace Starling.Domain.Repositories
{
    public class ShareRepository : Repository, IShareRepository
    {
        public ShareRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<Guid> AddShareAsync(string title, string description, CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<Guid>(Functions.AddShare, new {title, description});
        }

        public async Task<Guid> AddShareUserAsync(Guid shareId, string username, ShareUserOwnershipStatus ownershipStatus,
            CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<Guid>(Functions.AddShareUser, new
            {
                share_id = shareId,
                username,
                user_status = ownershipStatus
            });
        }

        public async Task<Guid> AddShareFileAsync(Guid shareId, Guid fileId, CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<Guid>(Functions.AddShareFile, new
            {
                share_id = shareId,
                file_id = fileId
            });
        }

        public async Task<Guid> AddShareFileUserAsync(Guid shareId, Guid fileId,  string username, string signature,
            CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<Guid>(Functions.AddShareFileUser, new
            {
                _share_id = shareId,
                _file_id = fileId,
                _username = username,
                _signature = signature
            });
        }

        public async Task<IEnumerable<ShareWithTotal>> GetShares(GetSharesFilter filter)
        {
            return await QueryAsync<ShareWithTotal>(Functions.GetUserShares, new
            {
                _username = filter.Username,
                _userstatus = filter.UserOwnershipStatus,
                _sharestatus = filter.SharesFilterStatus,
                _page = filter.Page,
                _take = filter.Take,
                _search = filter.Search
            });
        }

        public async Task<ShareForUser> GetShareForUser(string username, Guid shareId)
        {
            return await QueryFirstOrDefaultAsync<ShareForUser>(Functions.GetShareForUser, new
            {
                _username = username,
                _share_id = shareId
            });
        }

        public async Task<IEnumerable<ShareFileSigning>> GetShareFilesSigning(string username, Guid shareId)
        {
            return await QueryAsync<ShareFileSigning>(Functions.GetShareFileSigning, new
            {
                _username = username,
                _share_id = shareId
            });
        }

        public async Task<IEnumerable<ShareUserInfo>> ShareUserApprovements(Guid shareId)
        {
            return await QueryAsync<ShareUserInfo>(Functions.GetShareUsersInfo, new
            {
                _share_id = shareId
            });
        }

        public async Task AcceptAsync(Guid shareId, string username)
        {
            await QueryFirstOrDefaultAsync<Guid>(Functions.AcceptShare, new
            {
                _share_id = shareId,
                _username = username
            });
        }

        public async Task RejectAsync(Guid shareId, string username)
        {
            await QueryFirstOrDefaultAsync<Guid>(Functions.RejectShare, new
            {
                _share_id = shareId,
                _username = username
            });
        }

        public async Task<int> GetFilesQuantity(Guid shareId)
        {
            return await QueryFirstOrDefaultAsync<int>(Functions.GetShareFilesQuantity, new
            {
                _share_id = shareId
            });
        }

        public async Task<int> GetUsersQuantity(Guid shareId)
        {
            return await QueryFirstOrDefaultAsync<int>(Functions.GetShareUsersQuantity, new
            {
                _share_id = shareId
            });
        }

        public async Task<IEnumerable<SignatureVerification>> GetSignatureVerificationData(Guid shareId, string username)
        {
            return await QueryAsync<SignatureVerification>(Functions.GetSignaturesVerificationData, new
            {
                _share_id = shareId,
                _username = username
            });
        }
        
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await QueryFirstOrDefaultAsync<bool>(Functions.DeleteShare, new
            {
                _share_id = id
            });
        }
    }
}