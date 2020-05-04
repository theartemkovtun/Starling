using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Starling.Domain.Models;
using Starling.Domain.Models.Filters;
using Starling.Domain.Repositories.Contracts;
using Starling.Shared.Layout.Database;

namespace Starling.Domain.Repositories
{
    public class FileRepository : Repository, IFileRepository 
    {
        public FileRepository(IConfiguration configuration) : base(configuration) { }
        
        public async Task<File> FirstOrDefaultAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<File>(Functions.GetFileMetadata, new {file_id = id});
        }

        public async Task<Guid> AddAsync(string filename, byte[] content, CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<Guid>(Functions.AddFile, new {filename, content});
        }

        public async Task UpdateAsync(Guid fileId, byte[] content, CancellationToken cancellationToken = default)
        {
            await QueryFirstOrDefaultAsync<Guid>(Functions.UpdateFile, new {_file_id = fileId, _content = content});
        }

        public async Task<File> GetFileContentAsync(Guid fileId, int? version = null, CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<File>(Functions.GetFileContent, new {file_id = fileId});
        }

        public async Task<IEnumerable<FileWithTotal>> GetFiles(GetFilesFilter filter)
        {
            return await QueryAsync<FileWithTotal>(Functions.GetFiles, new
            {
                _username = filter.Username,
                _page = filter.Page,
                _take = filter.Take,
                _search = filter.Search,
                _ownership_status = filter.OwnershipStatus
            });
        }
    }
}