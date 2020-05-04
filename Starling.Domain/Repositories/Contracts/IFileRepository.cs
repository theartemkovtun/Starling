using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Starling.Domain.Models;
using Starling.Domain.Models.Filters;

namespace Starling.Domain.Repositories.Contracts
{
    public interface IFileRepository
    {
        Task<File> FirstOrDefaultAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> AddAsync(string filename, byte[] content, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid fileId, byte[] content, CancellationToken cancellationToken = default);
        Task<File> GetFileContentAsync(Guid fileId, int? version = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<FileWithTotal>> GetFiles(GetFilesFilter filter);
    }
}