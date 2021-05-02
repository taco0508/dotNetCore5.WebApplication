using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace dotNetCore5.DataAccess.Infrastructure.SqlHelper
{
    public class SqlHelper : ISqlHelper
    {
        private readonly ILogger<SqlHelper> logger;

        public SqlHelper(ILogger<SqlHelper> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public TResult CustomExecute<TResult>(IDbConnection conn, Func<IDbConnection, TResult> func)
        {
            using (conn)
            {
                try
                {
                    var result = func(conn);
                    return result;
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, ex.Message);
                    return default(TResult);
                }
            }
        }

        public async Task<TResult> CustomExecuteAsync<TResult>(IDbConnection conn, Func<IDbConnection, Task<TResult>> func)
        {
            using (conn)
            {
                try
                {
                    var result = await func(conn);
                    return result;
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, ex.Message);
                    return default(TResult);
                }
            }
        }

        public TResult CustomExecute<TResult>(IDbConnection conn, Func<IDbConnection, IDbTransaction, TResult> func)
        {
            using (conn)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var result = func(conn, trans);
                        trans.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex, ex.Message);
                        return default(TResult);
                    }
                }
            }
        }

        public async Task<TResult> CustomExecuteAsync<TResult>(IDbConnection conn, Func<IDbConnection, IDbTransaction, Task<TResult>> func)
        {
            using (conn)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var result = await func(conn, trans);
                        trans.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex, ex.Message);
                        return default(TResult);
                    }
                }
            }
        }
    }
}
