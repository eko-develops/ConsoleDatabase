using Microsoft.Data.Sqlite;

namespace ConsoleDatabase
{
    class Program
    {
        static void Main(string[] args)
        {

            //bool running = true;

            //while(running == true)
            //{
            //    Console.WriteLine("Database Manager");
            //}

            // DatabaseHelper.CreatePersonTable();

            // DatabaseHelper.AddRow(1, "John", 23, "developer");

            DatabaseHelper.ViewAll("Person");

            // DatabaseHelper.DropTable("Person");

        }
    }
}