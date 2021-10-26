using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLite_Testing
{
    class main
    {
        public static void Main()
        {
            using (connection)
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                PopulateData.populateDefaults();

                Console.WriteLine("Please enter your id: ");
                string id = Console.ReadLine();

                if (loginAuthentication.idExists(id))
                {
                    string[] loginInformation = login(id);
                    if (loginInformation[0] != "LOGIN ERROR")
                        Console.WriteLine($"Hello, {loginInformation[0]}! You currently have a balance of ${loginInformation[1]}.");
                    else
                    {
                        Console.WriteLine(loginInformation[0]);
                    }
                }
                else
                {
                    Console.WriteLine($"I'm sorry, but the account number {id} does not exist.");
                    Main();
                }
            }
        }

        public static string database = "database.db";
        public static SQLiteConnection connection = new SQLiteConnection($"Data Source = {database}");

        public static void CreateUpdateOrDelete(string SQLcommand)
        {
            try
            {
                using var command = new SQLiteCommand(connection);
                {
                    command.CommandText = SQLcommand;
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                Console.WriteLine($"There was an error executing {SQLcommand}");
            }
        }

        public static string[] Read(string where, string requestedData, string overrideCommand = "")
        {
            string SQLcommand;
            if (overrideCommand == "")
            {
                SQLcommand = $"SELECT {requestedData} FROM testTable WHERE {where}";
            }
            else
            {
                SQLcommand = overrideCommand;
            }

            List<string> data = new List<string>();

            using (var command = new SQLiteCommand(SQLcommand, connection))
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        data.Add(reader[i].ToString());
                    }
                }
            }
            return data.ToArray();
        }

        public static string[] login(string id)
        {

            try
            {
                if (Read($"id={id}", "password")[0].ToString() == encryptSHA256.encrypt(loginAuthentication.getPassword()))
                {
                    return Read($"id={id}", "name, balance");
                }
                else
                {
                    Console.WriteLine("Incorrect password entered. ");
                    return login(id);
                }
            }
            catch { }
            string[] failure = { "LOGIN ERROR" };
            return failure;
        }
    }
}
