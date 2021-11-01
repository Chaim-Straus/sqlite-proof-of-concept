using System;
// using System.Text.RegularExpressions;

namespace yoc_csharp_banking
{
    class userManagement
    {
        public static void addUser(string name = "default", string password = "default", int balance = 0)
        {
            //var allowed = new Regex("^[a-zA-Z0-9 ]*$");

            if (name == "default")
            {
                Console.Clear();
                Console.WriteLine($"Please enter user's name: ");
                name = Console.ReadLine();
                Console.WriteLine($"Please enter {name}'s password: ");
                password = Console.ReadLine();
            }

            Console.WriteLine($"Please enter {name}'s balance: ");
            bool worked = int.TryParse(Console.ReadLine(), out balance);
            if (!(worked))
            {
                Console.Clear();
                Console.WriteLine("That is not a valid balance.");
                addUser(name, password);
            }

            main.CreateUpdateOrDelete($"INSERT INTO bankData (name, password, balance) VALUES ('{name}', '{encryptSHA256.encrypt(password)}', {balance})");
        }

        public static Array displayData()
        {
            Array data = main.Read(null, null, $"SELECT * FROM bankData");
            return data;
        }
    }
}
