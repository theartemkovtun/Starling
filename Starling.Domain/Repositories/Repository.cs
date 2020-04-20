using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Starling.Shared.Layout.Database;

namespace Starling.Persistence.Repositories
{
    public abstract class Repository
    {
        protected string ConnectionString { get; }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string functionName, object parameters)
        {
            await using var db = new NpgsqlConnection(ConnectionString);
            return await db.QueryAsync<T>(functionName, parameters, commandType: CommandType.StoredProcedure);
        }
        
        protected async Task<T> QueryFirstOrDefaultAsync<T>(string functionName, object parameters)
        {
            await using var db = new NpgsqlConnection(ConnectionString);
            return await db.QueryFirstOrDefaultAsync<T>(functionName, parameters, commandType: CommandType.StoredProcedure);
        }

        protected Repository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("dev");
        }
    }
}