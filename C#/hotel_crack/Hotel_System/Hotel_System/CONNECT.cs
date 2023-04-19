using Npgsql;
using System.Data;

namespace Hotel_System
{
    internal class CONNECT
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Username=postgres;Password=salavat;Database=hotel");
        public NpgsqlConnection getConnection()
        {
            return connection;
        }

        public void openConnection()
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if(connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
