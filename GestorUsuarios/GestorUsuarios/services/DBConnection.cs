using MySql.Data.MySqlClient;
using System;

namespace GestorUsuarios.services
{
    public class DBConnection
    {
        private readonly string connectionString = "Server=localhost;Database=managerusers;Uid=root;Pwd=;";

        protected MySqlConnection connection;

        public DBConnection()
        {
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection Open()
        {
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public void Close()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }
    }
}
