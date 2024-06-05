﻿using System;
using System.Data.SqlClient;

namespace StroyCompany
{
    class DataBase : IDisposable
    {
        public SqlConnection sqlConnection = new SqlConnection(@"Data Source=DBSrv\vip2023;Initial Catalog=Stroy;Integrated Security=True;TrustServerCertificate=True");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlDataReader ExecuteQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            return cmd.ExecuteReader();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                sqlConnection.Dispose();
            }
        }
    }
}
