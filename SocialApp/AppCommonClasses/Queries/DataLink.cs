namespace SocialApp.Queries
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using AppCommonClasses.Interfaces;

    public sealed partial class DataLink : IDisposable, IDataLink
    {
        [Obsolete]
        private static readonly Lazy<DataLink> InstanceValue = new(() => new DataLink());
        private readonly string connectionString;
        [Obsolete]
        private SqlConnection? sqlConnection;
        private bool disposedValue;
        private string loginString = "Server=DESKTOP-S99JALT;Database=SocialApp;Trusted_Connection=True;TrustServerCertificate=True;";

        [Obsolete]
        public DataLink()
        {

            this.connectionString = this.loginString;

            using var connection = new SqlConnection(this.connectionString);
            connection.Open();

        }

        [Obsolete]
        public static DataLink Instance => InstanceValue.Value;

        [Obsolete]
        public T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();

                using var command = new SqlCommand(query, connection)
                {
                    CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteScalar operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteScalar operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public int ExecuteQuery(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();
                using var command = new SqlCommand(query, connection)
                {
                    CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
                };
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public T? ExecuteBool<T>(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();
                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteBool operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteBool operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public DataTable ExecuteReader(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteReader operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteReader operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteNonQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteNonQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteNonQueryAsync operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteNonQueryAsync operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Obsolete]
        public DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = this.GetConnection();
                connection.Open();

                using var command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text,  // Always a raw SQL query
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteSqlQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteSqlQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        private SqlConnection GetConnection()
        {
            ObjectDisposedException.ThrowIf(this.disposedValue, new ObjectDisposedException("DataLink"));

            if (this.sqlConnection == null || this.sqlConnection.State != System.Data.ConnectionState.Open)
            {
                this.sqlConnection = new SqlConnection(this.connectionString);
            }

            return this.sqlConnection;
        }

        [Obsolete]
        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (this.sqlConnection != null)
                    {
                        if (this.sqlConnection.State == ConnectionState.Open)
                        {
                            this.sqlConnection.Close();
                        }

                        this.sqlConnection.Dispose();
                        this.sqlConnection = null;
                    }
                }

                this.disposedValue = true;
            }
        }
    }
}
