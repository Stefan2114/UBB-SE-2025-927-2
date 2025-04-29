using System.Data;
using System.Data.SqlClient;

namespace AppCommonClasses.Interfaces
{
    public interface IDataLink
    {
        [Obsolete]
        T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure);
        [Obsolete]
        int ExecuteQuery(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure);
        [Obsolete]
        int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters);
        [Obsolete]
        DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters);
    }
}
