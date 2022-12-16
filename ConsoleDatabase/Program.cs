using Microsoft.Data.Sqlite;

namespace ConsoleDatabase
{
    class Program
    {
        static void Main(string[] args)
        {

            bool running = true;


            while (running == true)
            {
                string title = "Database Manager";
                Console.WriteLine(title.PadLeft(20, ' '));

                ConsoleFormatter.AddDivider(40, true);

                ConsoleFormatter.IndentSpaces();
                Console.Write("Please select an option: \n\n");

                string[] menuItems =
                {
                    "Create Table",
                    "View Table",
                    "Add Row",
                    "Delete Row",
                    "Delete Table",
                    "Quit"
                };

                int menuItemMaxChar = 20;

                for(int i = 0; i < menuItems.Length; i++)
                {
                    string prompt = menuItems[i];
                    int n = menuItemMaxChar - prompt.Length;
                    string pad = new String(' ', n);
                    Console.WriteLine("{0}{1} - {2}", pad, menuItems[i], i + 1);
                }

                Console.Write("\nenter command: ");
                string input = Console.ReadLine();

                Console.WriteLine(input);

                running = false;

            }

            //DatabaseHelper.CreatePersonTable();

            // DatabaseHelper.AddRow(2, "Jane", 25, "manager");

            // DatabaseHelper.ViewAll("Person");

            // DatabaseHelper.DropTable("Person");

            Console.ReadKey();
        }
    }
}