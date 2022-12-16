using Microsoft.Data.Sqlite;

namespace ConsoleDatabase
{
    class Program
    {
        static void Main(string[] args)
        {

            bool running = true;
            string tableName;
            string username;
            string occupation;
            string column;
            int id;
            int age;
            string data;
            string needle;


            while (running == true)
            {
                ConsoleHelper.DisplayMenu();

                string input = ConsoleHelper.GetCommand();

                switch (input)
                {
                    case "1":
                        DatabaseHelper.CreatePersonTable();
                        break;
                    case "2":
                        Console.Write("Enter a table name to view: ");
                        tableName = Console.ReadLine();
                        
                        DatabaseHelper.ViewAll(tableName);

                        break;
                    case "3":
                        Console.Write("Enter an ID: ");
                        id = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter a name: ");
                        username = Console.ReadLine();

                        Console.Write("Enter an age: ");
                        age = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter an occupation: ");
                        occupation = Console.ReadLine();

                        DatabaseHelper.AddRow(id, username, age, occupation);
                      
                        break;
                    case "4":
                        Console.Write("Enter a table to search: ");
                        tableName = Console.ReadLine();

                        Console.Write("Enter a column to search: ");
                        column = Console.ReadLine();

                        Console.Write("Enter data to search for: ");
                        needle = Console.ReadLine();

                        Console.Write("Enter new data: ");
                        data = Console.ReadLine();

                        DatabaseHelper.UpdateRow(tableName, column, needle, data);

                        break;
                    case "5":
                        Console.Write("Enter a table to search: ");
                        tableName = Console.ReadLine();

                        Console.Write("Enter an ID to delete: ");
                        id = Convert.ToInt32(Console.ReadLine());

                        DatabaseHelper.DeleteRow(tableName, id);
                       
                        break;
                    case "6":
                        Console.Write("Enter a table name: ");
                        tableName = Console.ReadLine();
                       
                        DatabaseHelper.DropTable(tableName);
                       
                        break;
                    case "7":
                        running = false;
                       
                        break;
                }


            }

            Console.WriteLine("Terminating program...");




            Console.ReadKey();
        }
    }
}