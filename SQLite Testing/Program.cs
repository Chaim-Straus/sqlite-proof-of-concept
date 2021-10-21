using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLite_Testing
{
    class Program
    {
        public static string database = "database.db";
        public static SQLiteConnection connection = new SQLiteConnection($"Data Source = {database}");

        public static void CRUDwithouttheR(string SQLcommand)
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

        public static Array CRUDwithouttheCUD(string SQLcommand)
        {
            List<Array> data = new List<Array>();
            try
            {
                using (var command = new SQLiteCommand(SQLcommand, connection))
                using (SQLiteDataReader rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        object[] row = { rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2) };
                        data.Add(row);
                    }
                }
                return data.ToArray();
            }
            catch
            {
                Console.WriteLine($"There was an error executing {SQLcommand}");
                data.Clear();
                object[] error = {"Error!"};
                data.Add(error);
                return data.ToArray();
            }
        }

        static void Main(string[] args)
        {
            using (connection)
            {
                connection.Open();

                CRUDwithouttheR("DROP TABLE testTable");
                Console.WriteLine("Table dropped.");
                CRUDwithouttheR("CREATE TABLE IF NOT EXISTS testTable (id integer primary key autoincrement, name text, balance integer)");
                Console.WriteLine("Table initialized.");
                Console.WriteLine("Inserting data...");
                CRUDwithouttheR("INSERT INTO testTable (name, balance) VALUES ('chaim', 100)");
                CRUDwithouttheR("INSERT INTO testTable (name, balance) VALUES ('michael', 90)");

                Console.WriteLine("Insert some of your own data!");

                Console.WriteLine("What name would you like to insert?");
                string name = Console.ReadLine();

                Console.WriteLine($"What balance would you like to associate with {name}?");

                int balance;
                bool converted = int.TryParse(Console.ReadLine(), out balance);
                if (!(converted)) {
                    Console.WriteLine("That is not an integer, and I do NOT want to deal with this right now. \nCancelling run.");
                    return;
                }

                CRUDwithouttheR($"INSERT INTO testTable (name, balance) VALUES ('{name}', {balance})");

                Console.WriteLine("Now try your own SQLite command! Note that the initialized table is called testTable.");
                string SQLite_command = Console.ReadLine();
                string upper = SQLite_command.ToUpper();
                if (upper.StartsWith("SELECT"))
                {
                    Array subData = CRUDwithouttheCUD(SQLite_command);
                    foreach (object[] subDatapoint in subData)
                    {
                        foreach (object subItem in subDatapoint)
                        {
                            Console.Write($"{subItem} ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("FULL TABLE BELOW");
                }
                else if (upper.StartsWith("INSERT") || upper.StartsWith("UPDATE"))
                {
                    CRUDwithouttheR(SQLite_command);
                }
                else
                {
                    Console.WriteLine("Sorry, that's not a proccessable command as of right now.");
                }

                Array data = CRUDwithouttheCUD("SELECT * FROM testTable");
                foreach (object[] datapoint in data)
                {
                    foreach (object item in datapoint)
                    {
                        Console.Write($"{item} ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
