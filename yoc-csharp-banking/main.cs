﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;

// should be working now

namespace yoc_csharp_banking
{
    class main
    {
        // let's initialize the public variables that SQLite will need whenever it wants to perform an operation
        public static string database = "database.db";
        public static SQLiteConnection connection = new SQLiteConnection($"Data Source = {database}");

        // constant login
        public static bool userLoggedIn = false;

        public static void Main()
        {
            using (connection)
            {
                // since Main can call Main, let's just make sure we don't raise an error by opening an open connection
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    // let's populate some information - simple defaults (michael and I are rich!)
                    PopulateData.populateDefaults();
                }

                // now, let's get the user to enter their id
                Console.WriteLine("Please enter your id: ");
                string id = Console.ReadLine();

                if (id == "")
                    return;

                bool worked = int.TryParse(id, out int _);
                if (!(worked))
                {
                    Console.Clear();
                    Console.WriteLine("I'm sorry, that is not a valid id.");
                    Main();
                }

                // we need to authenticate their id, so:
                if (loginAuthentication.idExists(id))
                {
                    // since it exists, we should get their password. to the login!
                    string[] loginInformation = login(id);

                    // well, something went wrong if this raises false
                    if (loginInformation[0] != "LOGIN ERROR")
                    {
                        // no errors! woo! let's give them their information.
                        if (loginInformation[0] == "admin")
                            // hey! an admin has logged in!
                        {
                            Console.Clear();
                            Console.WriteLine("Hello, admin!");
                            Console.WriteLine("What would you like to do?");
                            // let's figure out what they want to do, then do it
                            admin.options();
                            // done!
                            Console.Clear();
                            Main();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Hello, {loginInformation[0]}! You currently have a balance of ${loginInformation[1]}.");
                            bool done = loggedIn.features(id);
                            if (done)
                            {
                                Console.Clear();
                                Main();
                            }
                        }
                    }

                    // ...uh oh something went wrong
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(loginInformation[0]);
                    }
                }

                // ...who are you?
                else
                {
                    Console.Clear();
                    Console.WriteLine($"I'm sorry, but the account number {id} does not exist.");
                    // let's try again I guess
                    Main();
                }
            }
        }

        public static void CreateUpdateOrDelete(string SQLcommand)
            // the function used to create, update, or delete in our SQLite table
        {
            // this should never be caught, as we're only sending prewritten commands, but YOU NEVER KNOW
            try
            {
                // SQLite stuff. It confuses me too.
                using var command = new SQLiteCommand(connection);
                {
                    command.CommandText = SQLcommand;
                    command.ExecuteNonQuery();
                }
            }
            // something has gone very wrong if this catches something
            catch
            {
                Console.WriteLine($"There was an error executing {SQLcommand}");
            }
        }

        public static string[] Read(string where, string requestedData, string overrideCommand = "")
            // this is the by far more useful function - it deals with getting all of our information!
        {
            string SQLcommand;
            // don't hate, it works and was the only way I could think to handle this (also lets me debug)
            if (overrideCommand == "")
                SQLcommand = $"SELECT {requestedData} FROM bankData WHERE {where}";
            else
                SQLcommand = overrideCommand;

            // let's prepare our data variable
            List<string> data = new List<string>();

            // more SQLite stuff. still confuses me, but hey! it works!
            using (var command = new SQLiteCommand(SQLcommand, connection))
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    // this makes sure that we can read multiple fields if we call for it
                    for (int i = 0; i < reader.FieldCount; i++)
                        data.Add(reader[i].ToString());
            }
            return data.ToArray();
        }

        public static string[] login(string id)
            // login function
        {
            // complicated SHA256 stuff - just matching the password
            Console.WriteLine("Please enter your password:");
            if (Read($"id={id}", "password")[0].ToString() == encryptSHA256.encrypt(loginAuthentication.getPassword()))
                // haha! they're real!
                return Read($"id={id}", "name, balance");

            // SOME SNEAKY PEOPLE ARE ABOUT
            else
            {
                Console.WriteLine("Incorrect password entered. ");
                return login(id);
            }
        }
    }
}
