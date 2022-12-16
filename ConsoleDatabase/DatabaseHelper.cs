using Microsoft.Data.Sqlite;
using System;
using System.Reflection.PortableExecutable;


namespace ConsoleDatabase
{
    class DatabaseHelper
    {
        
        public static void CreatePersonTable()
        {
            try
            {
                Connection connection = new Connection("DataSource=database.db");
                connection.OpenConnection();

                //SqliteConnection connection = new SqliteConnection("DataSource=database.db");
                //connection.Open();

                // creating table
                string table = $"CREATE TABLE Person (ID integer primary key, NAME text, AGE integer, OCCUPATION text)";
                SqliteCommand query = new SqliteCommand(table, connection);
                query.ExecuteNonQuery();

                connection.CloseConnection();
                //connection.Close();

                Console.WriteLine("Table created successfully");
                ConsoleHelper.CommandEnd();
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

        }

        public static void AddRow(int id, string name, int age, string occupation)
        {
            try
            {
                Connection connection = new Connection("DataSource=database.db");

                SqliteParameter idParam = new SqliteParameter("$id", 1);
                idParam.Value = id;

                SqliteParameter nameParam = new SqliteParameter("$name", 3);
                nameParam.Value = name;

                SqliteParameter ageParam = new SqliteParameter("$age", 1);
                ageParam.Value = age;

                SqliteParameter occupationParam = new SqliteParameter("$occupation", 3);
                occupationParam.Value = occupation;

                string row =
                    @"
                        INSERT INTO Person (id, name, age, occupation)
                        VALUES ($id, $name, $age, $occupation)
                    ";

                connection.OpenConnection();

                SqliteCommand query = new SqliteCommand(row, connection);

                SqliteParameter[] sqliteParams = { idParam, nameParam, ageParam, occupationParam };

                foreach(SqliteParameter param in sqliteParams)
                {
                    query.Parameters.Add(param);
                }

                query.ExecuteNonQuery();


                connection.CloseConnection();

                Console.WriteLine($"Row added successfully");
                ConsoleHelper.CommandEnd();
            }
            catch (Exception e) {
                Console.WriteLine("Error adding row");
                Console.WriteLine(e.Message);
            }
        }

        public static void DeleteRow(string tableName, int id)
        {
            try
            {
                Connection connection = new Connection("DataSource=database.db");
                connection.OpenConnection();

                string stringQuery =
                    $@"
                        DELETE FROM {tableName}
                        WHERE id={id}
                    ";

                SqliteCommand query = new SqliteCommand(stringQuery, connection);
                query.ExecuteNonQuery();

                connection.CloseConnection();

                Console.WriteLine($"Row with ID {id} table dropped successfully from table {tableName}");
                ConsoleHelper.CommandEnd();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error deleting row");
                Console.WriteLine(e.Message);
            }
        }

        public static void UpdateRow(string tableName, string column, string updateColumn, string needle, string data)
        {
            try
            {
                Connection connection = new Connection("DataSource=database.db");
                connection.OpenConnection();


                string row = $@"UPDATE Person
                               SET {updateColumn}='{data}'
                                WHERE {column}='{needle}'";

                SqliteCommand query = new SqliteCommand(row, connection);
                query.ExecuteNonQuery();


                connection.CloseConnection();

                Console.WriteLine($"Updated {updateColumn} to {data} where {column} = {needle}");
                ConsoleHelper.CommandEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating {updateColumn} to {data} where {column} = {needle}");
                Console.WriteLine(e.Message);
            }
        }

        public static void ViewAll(string name)
        {
            try
            {
                Connection connection = new Connection("DataSource=database.db");
                connection.OpenConnection();

                string stringQuery =
                    $@"
                        SELECT * FROM {name}
                    ";

                SqliteCommand query = new SqliteCommand(stringQuery, connection);
                
                SqliteDataReader result = query.ExecuteReader();

                if (result.HasRows)
                {
                    // displays the column names
                    for (var i = 0; i < result.FieldCount; i++)
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

                
                connection.CloseConnection();

                Console.WriteLine($"\nViewing {name} table");
                ConsoleHelper.CommandEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error viewing {name} tables");
                Console.WriteLine(e.Message);
            }
        }

        public static void DropTable(string name)
        {
            try
            {

                Connection connection = new Connection("DataSource=database.db");
                connection.OpenConnection();

                string row = $"DROP TABLE {name}";

                SqliteCommand query = new SqliteCommand(row, connection);
                query.ExecuteNonQuery();


                connection.CloseConnection();

                Console.WriteLine($"{name} table dropped successfully");
                ConsoleHelper.CommandEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error dropping {name} tables");
                Console.WriteLine(e.Message);
            }
        }

        public static void GetAllTableNames()
        {
            try
            {
                List<SqliteDataReader> resultList = new List<SqliteDataReader>();

                Connection connection = new Connection("DataSource=database.db");
                connection.OpenConnection();

                string tablesQuery = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";

                SqliteCommand query = new SqliteCommand(tablesQuery, connection);
                SqliteDataReader result = query.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        Console.WriteLine("\n{0}\t{1}\n", result.GetInt32(0), result.GetString(0));
                    }
                }
                else
                {
                    Console.WriteLine("\n\nNo tables found in database.\n\n");
                }

                result.Close();
                connection.CloseConnection();
                ConsoleHelper.CommandEnd();
            }
            catch(Exception e)
            {
                Console.WriteLine("\n\nError occured getting all table names");
                Console.WriteLine(e.Message + "\n\n");
            }
        }

    }
}
