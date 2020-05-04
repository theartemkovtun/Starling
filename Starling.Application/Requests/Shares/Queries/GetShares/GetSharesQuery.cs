using System.Collections.Generic;
using MediatR;
using Starling.Application.DTOs;
using Starling.Domain.Enums;

namespace Starling.Application.Requests.Shares.Queries.GetShares
{
    public class GetSharesQuery : IRequest<ShareSearchResult>
    {
        public GetSharesQuery(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
        public ShareUserOwnershipStatus? UserOwnershipStatus { get; set; }
        public SharesFilterStatus? SharesFilterStatus { get; set; }
        public int? Page { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
    }
}