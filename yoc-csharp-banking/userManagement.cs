// Chaim Straus

using System;
// using System.Text.RegularExpressions;

namespace yoc_csharp_banking
{
    class userManagement
    {
        public static void addUser(string name = null, string password = null)
            // add a user to the database (admin only)
        {
            //var allowed = new Regex("^[a-zA-Z0-9 ]*$");
            int balance;

            // if the name is still default, let them enter a name and password
            if (name == null)
            {
                Console.Clear();
                Console.WriteLine($"Please enter user's name: ");
                name = Console.ReadLine();
                Console.WriteLine($"Please enter {name}'s password: ");
                password = Console.ReadLine();
            }

            // get their balance, then make them enter it until it's a number
            Console.WriteLine($"Please enter {name}'s balance: ");
            bool worked = int.TryParse(Console.ReadLine(), out balance);
            if (!(worked))
            {
                Console.Clear();
                Console.WriteLine("That is not a valid balance.");
                addUser(name, password);
                return;
            }

            // add them to the database
            main.CreateUpdateOrDelete($"INSERT INTO bankData (name, password, balance) VALUES ('{name}', '{encryptSHA256.encrypt(password)}', {balance})");
        }

        public static Array displayData()
            // displays all data in the database
        {
            Array data = main.Read(null, null, $"SELECT * FROM bankData");
            return data;
        }
    }
}
