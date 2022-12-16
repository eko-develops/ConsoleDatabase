

namespace ConsoleDatabase
{
    class ConsoleHelper
    {
        public static string GetCommand()
        {
            Console.Write("\nEnter Command: ");
            
            string command = Console.ReadLine();
            
            return command;
        }

        public static void DisplayMenu()
        {
            string title = "Database Manager";
            Console.WriteLine(title.PadLeft(20, ' '));

            ConsoleHelper.AddDivider(40, true);

            ConsoleHelper.IndentSpaces();
            Console.Write("Please select an option: \n\n");

            string[] menuItems =
            {
                    "Create Table",
                    "View Table",
                    "Add Row",
                    "Update Row",
                    "Delete Row",
                    "Delete Table",
                    "View All Tables",
                    "Quit"
                };

            int menuItemMaxChar = 20;

            for (int i = 0; i < menuItems.Length; i++)
            {
                string prompt = menuItems[i];
                int n = menuItemMaxChar - prompt.Length;
                string pad = new String(' ', n);
                Console.WriteLine("{0}{1} - {2}", pad, menuItems[i], i + 1);
            }
        }

        public static void IndentSpaces(int amount = 8)
        {
            Console.Write(string.Concat(Enumerable.Repeat(" ", amount)));
        }

        public static void AddDivider(int length = 10, bool pad = false)
        {
            if (pad)
            {
                Console.WriteLine("\n" + string.Concat(Enumerable.Repeat("-", length)) + "\n");
            }
            else
            {
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", length)));
            }

        }
    }
}
