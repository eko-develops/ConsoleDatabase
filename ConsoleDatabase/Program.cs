using Microsoft.Data.Sqlite;
using System.Net.Http.Headers;

namespace ConsoleDatabase
{
    class Program
    {
        static void Main(string[] args)
        {

            // DatabaseHelper.CreatePersonTable();

            // DatabaseHelper.AddRow(1, "John", 23, "developer");

            DatabaseHelper.ViewAll("Person");

            // DatabaseHelper.DropTable("Person");

        }
    }
}