using System;

namespace SQLite_Testing
{
    class loginAuthentication
    {
        public static string getPassword()
        {
            Console.WriteLine("Please enter your password:");
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
        {
            return int.Parse(main.Read(null, null, $"SELECT EXISTS(SELECT * from testTable WHERE id={id})")[0]) == 1;
        }
    }
}
