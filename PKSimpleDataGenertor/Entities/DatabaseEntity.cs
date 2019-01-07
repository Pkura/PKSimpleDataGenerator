using System.Collections.Generic;
using System.Data.SqlClient;

namespace PKSimpleDataGenerator.Entities
{
    public class DatabaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public string InitialCatalog { get; set; }
        public string DataSource { get; set; }
        public bool IntegratedSecurity { get; set; }

        public string GetConnectionString()
        {
            return new SqlConnectionStringBuilder
            {
                UserID = UserId,
                Password = Password,
                DataSource = DataSource,
                IntegratedSecurity = IntegratedSecurity
            }.ConnectionString;
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public List<TableEntity> Tables { get; set; }
    }
}