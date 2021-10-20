using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLite_Testing
{
    class Program
    {
        public static void CRUDwithouttheR(string database, string SQLcommand)
        {
            using (var connection = new SQLiteConnection($"Data Source = {database}"))
            {
                connection.Open();
                using var command = new SQLiteCommand(connection);
                {
                    command.CommandText = SQLcommand;
                    command.ExecuteNonQuery();
                }
            }
        }

        public static Array CRUDwithouttheCUD(string database, string SQLcommand)
        {
            List<Array> data = new List<Array>();
            using (var connection = new SQLiteConnection($"Data Source = {database}"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(SQLcommand, connection))
                using (SQLiteDataReader rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Console.WriteLine($"{rdr.GetInt32(0)} | {rdr.GetString(1)} | {rdr.GetInt32(2)}");
                        object[] row = { rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2)};
                        data.Add(row);
                    }
                }
            }
            return data.ToArray();
        }

        static void Main(string[] args)
        {
            string db = "database.db";
            CRUDwithouttheR(db, "DROP TABLE testTable");
            Console.WriteLine("Table dropped.");
            CRUDwithouttheR(db, "CREATE TABLE IF NOT EXISTS testTable (id integer primary key autoincrement, name text, balance integer)");
            Console.WriteLine("Table initialized.");
            Console.WriteLine("Inserting data...");
            CRUDwithouttheR(db, "INSERT INTO testTable (name, balance) VALUES ('chaim', 100)");
            CRUDwithouttheR(db, "INSERT INTO testTable (name, balance) VALUES ('michael', 90)");

            Console.WriteLine("Insert some of your own data!");

            Console.WriteLine("What name would you like to insert?");
            string name = Console.ReadLine();

            Console.WriteLine($"What balance would you like to associate with {name}?");
            int balance = int.Parse(Console.ReadLine());

            CRUDwithouttheR(db, $"INSERT INTO testTable (name, balance) VALUES ('{name}', {balance})");

            Array data = CRUDwithouttheCUD(db, "SELECT * FROM testTable");
            foreach (object[] datapoint in data)
            {
                foreach (object item in datapoint)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
                //string column = string.Concat(datapoint);
                //Console.WriteLine(column);
            }
        }
    }
}
