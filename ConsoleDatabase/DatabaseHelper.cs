using Microsoft.Data.Sqlite;

namespace ConsoleDatabase
{
    class DatabaseHelper
    {
        
        public static void CreateConnection()
        {

            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");

                connection.Open();

                Console.WriteLine($"Connection is {connection.State}");

            }
            catch(SqliteException e)
            {
                Console.WriteLine("SQlite Exception:");
                Console.WriteLine(e.Message);
    
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(e.Message);
            }

        }

        public static void CreatePersonTable()
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");

                connection.Open();

                // creating table
                string table = $"CREATE TABLE Person (ID integer primary key, NAME text, AGE integer, OCCUPATION text)";
                SqliteCommand query = new SqliteCommand(table, connection);
                query.ExecuteNonQuery();

                connection.Close();

                Console.WriteLine("Table created successfully");
                Console.ReadLine();
            }
            catch(SqliteException e)
            {
                Console.WriteLine("SqliteException: attempted to create table");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: attempted to create table");
                Console.WriteLine(e.Message);

            }

        }

        public static void AddRow(int id, string name, int age, string occupation)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");

                SqliteParameter idParam = new SqliteParameter(Convert.ToString(id), 1);
                SqliteParameter nameParam = new SqliteParameter(name, 3);
                SqliteParameter ageParam = new SqliteParameter(Convert.ToString(age), 1);
                SqliteParameter occupationParam = new SqliteParameter(occupation, 3);


                connection.Open();

                string row =
                    @"
                        INSERT INTO Person
                        VALUES ($id, $name, $age, $occupation)
                    ";

                SqliteCommand query = new SqliteCommand(row, connection);

                query.Parameters.AddWithValue("$id", id);
                query.Parameters.AddWithValue("$name", name);
                query.Parameters.AddWithValue("$age", age);
                query.Parameters.AddWithValue("$occupation", occupation);
                query.ExecuteNonQuery();


                connection.Close();

                Console.WriteLine($"Row added successfully");
                Console.ReadLine();
            }
            catch (Exception e) {
                Console.WriteLine("Error adding row");
                Console.WriteLine(e.Message);
            }
        }

        public static void DeleteRow()
        {

        }

        public static void UpdateRow()
        {

        }

        public static void ViewAll(string name)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                connection.Open();

                string stringQuery =
                    $@"
                        SELECT * FROM {name};
                    ";

                SqliteCommand query = new SqliteCommand(stringQuery, connection);
                
                SqliteDataReader result = query.ExecuteReader();

                if (result.HasRows)
                {
                    // displays the column names
                    for (var i = 0; i < result.FieldCount; i++)
                    {
                        Console.Write(result.GetName(i));
                        ConsoleFormatter.IndentSpaces(8);
                    }

                    ConsoleFormatter.AddDivider(result.FieldCount * 20);

                    // displays rows
                    while (result.Read())
                    {
                        Console.Write(result["id"]);
                        ConsoleFormatter.IndentSpaces(9);
                        Console.Write(result["name"]);
                        ConsoleFormatter.IndentSpaces(9);
                        Console.Write(result["age"]);
                        ConsoleFormatter.IndentSpaces(9);
                        Console.Write(result["occupation"]);
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Table is empty");
                }

                connection.Close();

                Console.WriteLine($"\nViewing {name} table");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error dropping {name} tables");
                Console.WriteLine(e.Message);
            }
        }

        public static void DropTable(string name)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                connection.Open();

                string row = $"DROP TABLE {name}";

                SqliteCommand query = new SqliteCommand(row, connection);
                query.ExecuteNonQuery();


                connection.Close();

                Console.WriteLine($"{name} table dropped successfully");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error dropping {name} tables");
                Console.WriteLine(e.Message);
            }
        }

    }
}
