using System;
using System.Threading;
using System.Threading.Tasks;
using Starling.Domain.Models;

namespace Starling.Application.Contracts.Repositories
{
    public interface IFileRepository
    {
        Task<File> FirstOrDefaultAsync(Guid key, CancellationToken cancellationToken = default);
        Task<Guid> AddAsync(string filename, byte[] content, CancellationToken cancellationToken = default);

        Task UpdateAsync(Guid fileId, byte[] content, CancellationToken cancellationToken = default);
        Task<File> GetFileContentAsync(Guid fileId, int? version = null, CancellationToken cancellationToken = default);
    }
}