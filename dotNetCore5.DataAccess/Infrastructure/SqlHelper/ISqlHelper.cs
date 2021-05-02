using System;
using System.Data;
using System.Threading.Tasks;

namespace dotNetCore5.DataAccess.Infrastructure.SqlHelper
{
    public interface ISqlHelper
    {
        TResult CustomExecute<TResult>(IDbConnection conn, Func<IDbConnection, TResult> func);

        Task<TResult> CustomExecuteAsync<TResult>(IDbConnection conn, Func<IDbConnection, Task<TResult>> func);

        TResult CustomExecute<TResult>(IDbConnection conn, Func<IDbConnection, IDbTransaction, TResult> func);

        Task<TResult> CustomExecuteAsync<TResult>(IDbConnection conn, Func<IDbConnection, IDbTransaction, Task<TResult>> func);
    }
}
