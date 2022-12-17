using Microsoft.Data.Sqlite;
using System;
using System.Reflection.PortableExecutable;


namespace ConsoleDatabase
{
    class DatabaseHelper
    {
        
        public static void CreateTable(string name, string schema)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                connection.Open();

                string query = $"CREATE TABLE {name} ({schema})";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                Console.WriteLine("Table created successfully");

            }
            catch(SqliteException e)
            {
                Console.WriteLine("SqliteException: attempted to create table");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\nException: attempted to create table");
                Console.WriteLine(e.Message+ "\n\n");

            }

            ConsoleHelper.CommandEnd();

        }

        public static void AddRow(int id, string name, int age, string occupation)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");

                SqliteParameter idParam = CreateSqliteParameter(Convert.ToString(id), "$id", 1);
                SqliteParameter nameParam = CreateSqliteParameter(name, "$name", 3);
                SqliteParameter ageParam = CreateSqliteParameter(Convert.ToString(age), "age", 1);
                SqliteParameter occupationParam = CreateSqliteParameter(occupation, "occupation", 3);
                SqliteParameter[] sqliteParams = { idParam, nameParam, ageParam, occupationParam };

                string query =
                    @"
                        INSERT INTO Person (id, name, age, occupation)
                        VALUES ($id, $name, $age, $occupation)
                    ";

                connection.Open();

                SqliteCommand command = new SqliteCommand(query, connection);

                foreach(SqliteParameter param in sqliteParams)
                {
                    command.Parameters.Add(param);
                }

                command.ExecuteNonQuery();

                connection.Close();

                Console.WriteLine($"Row added successfully");
            }
            catch (Exception e) {
                Console.WriteLine("Error adding row");
                Console.WriteLine(e.Message);
            }

            ConsoleHelper.CommandEnd();

        }

        public static void DeleteRow(string tableName, int id)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                connection.Open();

                string query =
                    $@"
                        DELETE FROM {tableName}
                        WHERE id={id}
                    ";

                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                Console.WriteLine($"Row with ID {id} table dropped successfully from table {tableName}");

            }
            catch (Exception e)
            {
                Console.WriteLine("Error deleting row");
                Console.WriteLine(e.Message);
            }

            ConsoleHelper.CommandEnd();

        }

        public static void UpdateRow(string tableName, string column, string updateColumn, string needle, string data)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                connection.Open();


                string query = $@"UPDATE Person
                               SET {updateColumn}='{data}'
                                WHERE {column}='{needle}'";

                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();


                connection.Close();

                Console.WriteLine($"Updated {updateColumn} to {data} where {column} = {needle}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating {updateColumn} to {data} where {column} = {needle}");
                Console.WriteLine(e.Message);
            }

            ConsoleHelper.CommandEnd();

        }

        public static void ViewAll(string name)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                connection.Open();

                string query =
                    $@"
                        SELECT * FROM {name}
                    ";

                SqliteCommand command = new SqliteCommand(query, connection);
                
                SqliteDataReader result = command.ExecuteReader();

                if (result.HasRows)
                {
                    // displays the column names
                    for (int i = 0; i < result.FieldCount; i++)
                    {
                        Console.Write(result.GetName(i));
                        ConsoleHelper.IndentSpaces(8);
                    }

                    ConsoleHelper.AddDivider(result.FieldCount * 20, true);

                    // displays rows
                    while (result.Read())
                    {
                        Console.Write(result["id"]);
                        ConsoleHelper.IndentSpaces(9);
                        Console.Write(result["name"]);
                        ConsoleHelper.IndentSpaces(4);
                        Console.Write(result["age"]);
                        ConsoleHelper.IndentSpaces(9);
                        Console.Write(result["occupation"]);
                        Console.WriteLine();
                    }

                    result.Close();
                }
                else
                {
                    Console.WriteLine("Table is empty");
                }

                connection.Close();

                Console.WriteLine($"\nViewing {name} table");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error viewing {name} tables");
                Console.WriteLine(e.Message);
            }

            ConsoleHelper.CommandEnd();

        }

        public static void DropTable(string name)
        {
            try
            {

                SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                connection.Open();

                string query = $"DROP TABLE {name}";

                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                Console.WriteLine($"{name} table dropped successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error dropping {name} tables");
                Console.WriteLine(e.Message);
            }

            ConsoleHelper.CommandEnd();

        }

        public static void GetAllTableNames()
        {
            try
            {
                SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                connection.Open();

                string query = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";

                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader result = command.ExecuteReader();

                if (result.HasRows)
                {
                    int count = 1;
                    Console.Write("\n\n");
                    while (result.Read())
                    {
                        Console.WriteLine("{0}\t{1}", count, result.GetString(0));
                        count++;
                    }
                    Console.WriteLine($"\n\nTotal Tables: {count - 1}\n\n");
                    result.Close();
                    connection.Open();
                }
                else
                {
                    Console.WriteLine("\n\nNo tables found in database.\n\n");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("\n\nError occured getting all table names");
                Console.WriteLine(e.Message + "\n\n");
            }

            ConsoleHelper.CommandEnd();

        }
        public static SqliteParameter CreateSqliteParameter(string value, string variable, int type)
        {
            SqliteParameter param = new SqliteParameter(variable, type);
            param.Value = value;
            return param;
        }

    }
}
