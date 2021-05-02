using System.Data;

namespace dotNetCore5.DataAccess.Infrastructure.Connection
{
    public interface IConnectionHelper
    {
        /// <summary>
        /// Get Northwind Connection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetNorthwindConnection();
    }
}