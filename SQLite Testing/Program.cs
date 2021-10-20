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
            List<string> data = new List<string>();
            using (var connection = new SQLiteConnection($"Data Source = {database}"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(SQLcommand, connection))
                using (SQLiteDataReader rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Console.WriteLine($"{rdr.GetInt32(0)} | {rdr.GetString(1)} | {rdr.GetInt32(2)}");
                        data.Add($"{rdr.GetInt32(0)} | {rdr.GetString(1)} | {rdr.GetInt32(2)}");
                    }
                }
            }
            return data.ToArray();
        }

        static void Main(string[] args)
        {
            string db = "database.db";
            Console.WriteLine("Hello World!");
            CRUDwithouttheR(db, "DROP TABLE testTable");
            CRUDwithouttheR(db, "CREATE TABLE IF NOT EXISTS testTable (id integer primary key autoincrement, name text, balance integer)");
            CRUDwithouttheR(db, "INSERT INTO testTable (name, balance) VALUES ('chaim', 100)");
            CRUDwithouttheR(db, "INSERT INTO testTable (name, balance) VALUES ('michael', 90)");
            Array data = CRUDwithouttheCUD(db, "SELECT * FROM testTable");
            foreach (string datapoint in data)
            {
                Console.WriteLine(datapoint);
            }
        }
    }
}
