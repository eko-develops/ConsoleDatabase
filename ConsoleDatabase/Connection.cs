using Microsoft.Data.Sqlite;
using System;


namespace ConsoleDatabase
{
    class Connection
    {

        SqliteConnection connection;

        public Connection(string dataSource)
        {
            connection = new SqliteConnection(dataSource);    
        }

        public void Open()
        {
            this.connection.Open();
        }
    }
}
