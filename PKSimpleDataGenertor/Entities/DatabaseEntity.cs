using System;
using System.Collections.Generic;
using System.Data;
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
        public List<TableEntity> Tables { get; set; }

        public string Errors { get; private set; }

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

        public bool CheckConnection()
        {
            var connIsOk = false;

            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    if (conn.State == ConnectionState.Open) connIsOk = true;
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Errors = e.Message;
                return false;
            }

            return connIsOk;
        }
    }
}