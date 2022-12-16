using Microsoft.Data.Sqlite;
using System;


namespace ConsoleDatabase
{
    class Connection : SqliteConnection
    {

        SqliteConnection connection;

        public Connection(string dataSource)
        {
            connection = new SqliteConnection(dataSource);    
        }

        public void OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("\n\nConnection open\n\n");
            }
            catch (Exception e )
            {
                Console.WriteLine("Error attempting to connect to data source");
                Console.WriteLine(e.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                connection.Close();
                Console.WriteLine("\n\nConnection closed\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error attempting to close connection to data source");
                Console.WriteLine(e.Message);
            }
        }
    }
}
