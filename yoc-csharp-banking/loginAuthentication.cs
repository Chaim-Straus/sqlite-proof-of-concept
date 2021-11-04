// Chaim Straus

using System;

namespace yoc_csharp_banking
{
    class loginAuthentication
    {
        public static string getPassword()
            // a function to get password input while obscuring console text logging
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();

            return pass.ToString();
        }

        public static bool idExists(string id)
            // check to see if an id exists
        {
            return int.Parse(main.Read(null, null, $"SELECT EXISTS(SELECT * from bankData WHERE id={id})")[0]) == 1;
        }
    }
}
