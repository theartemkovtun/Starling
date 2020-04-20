using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Starling.Application.Contracts.Repositories;
using Starling.Domain.Models;
using Microsoft.Extensions.Configuration;
using Starling.Shared.Layout.Database;

namespace Starling.Persistence.Repositories
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
            await QueryFirstOrDefaultAsync<Guid>(Functions.UpdateFile, new {file_id = fileId, _content = content});
        }

        public async Task<File> GetFileContentAsync(Guid fileId, int? version = null, CancellationToken cancellationToken = default)
        {
            return await QueryFirstOrDefaultAsync<File>(Functions.GetFileContent, new {file_id = fileId});
        }
    }
}