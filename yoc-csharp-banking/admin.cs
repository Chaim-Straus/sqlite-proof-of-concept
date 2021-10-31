using System;
using System.Collections.Generic;
using System.Text;

namespace yoc_csharp_banking
{
    class admin
    {
        public static List<string> validOptions = new List<string> { "add user", "remove user" };
        public static void options()
        {
            Console.WriteLine("Valid options: ");
            foreach (string option in validOptions)
            {
                Console.WriteLine($"    {option}");
            }

            string chosen = Console.ReadLine();
            switch (chosen)
            {
                case "add user":
                    userManagement.addUser();
                    break;
                case "remove user":
                    userManagement.removeUser();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"I'm sorry, but {chosen} is not a valid option.");
                    Console.WriteLine("What would you like to do?");
                    options();
                    break;
            }
        }
    }
}
