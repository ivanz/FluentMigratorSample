using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseMigrations;

namespace WebApp.Tests
{
    public class IntegrationTestLifecycle : IDisposable
    {
        private const int DEFAULT_SQL_COMMAND_TIMEOUT = 60;
        private bool _isDisposed = false;

        public string ConnectionString { get; private set; }

        public void SetUp(string connectionStringTemplate)
        {
            string uniqueDatabaseConnectionString = GenerateUniqueDatabaseName(connectionStringTemplate);

            CreateDatabase(uniqueDatabaseConnectionString);

            DatabaseMigrationRunner.MigrateDatabase("SqlServer", uniqueDatabaseConnectionString);

            ConnectionString = uniqueDatabaseConnectionString;
        }

        public void TearDown()
        {
            DeleteDatabase();
        }

        private static string GenerateUniqueDatabaseName(string originialConnectionString)
        {
            SqlConnectionStringBuilder connectionInfo = new SqlConnectionStringBuilder(originialConnectionString);
            connectionInfo.InitialCatalog = String.Format(CultureInfo.InvariantCulture, "{0}_{1}", connectionInfo.InitialCatalog, Guid.NewGuid().ToString());

            return connectionInfo.ToString();
        }

        private void CreateDatabase(string connectionString)
        {
            SqlConnectionStringBuilder connectionInfo = new SqlConnectionStringBuilder(connectionString);

            SqlConnectionStringBuilder master = new SqlConnectionStringBuilder(connectionString) {
                InitialCatalog = "master"
            };

            using (SqlConnection sqlConnection = new SqlConnection(master.ToString()))
            using (SqlCommand createDbCommand = sqlConnection.CreateCommand()) {
                sqlConnection.Open();
                createDbCommand.CommandTimeout = DEFAULT_SQL_COMMAND_TIMEOUT;
                createDbCommand.CommandText = String.Format(
@"CREATE DATABASE [{0}] 
ALTER DATABASE [{0}] SET ALLOW_SNAPSHOT_ISOLATION ON 
ALTER DATABASE [{0}] SET RECOVERY SIMPLE", connectionInfo.InitialCatalog);
                createDbCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        private void DeleteDatabase()
        {
            SqlConnectionStringBuilder connectionInfo = new SqlConnectionStringBuilder(ConnectionString);

            SqlConnectionStringBuilder master = new SqlConnectionStringBuilder(ConnectionString) {
                InitialCatalog = "master"
            };

            using (SqlConnection sqlConnection = new SqlConnection(master.ToString()))
            using (SqlCommand createDbCommand = sqlConnection.CreateCommand()) {
                sqlConnection.Open();
                createDbCommand.CommandTimeout = DEFAULT_SQL_COMMAND_TIMEOUT;

                createDbCommand.CommandText = String.Format(
@"if db_id('{0}') is not null 
BEGIN 
    ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE 
    DROP DATABASE [{0}] 
END",
                connectionInfo.InitialCatalog);
                createDbCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed) {
                DeleteDatabase();
                _isDisposed = true;
            }
        }
    }
}


