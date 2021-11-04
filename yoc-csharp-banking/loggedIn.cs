// Chaim Straus

using System;
using System.Collections.Generic;

namespace yoc_csharp_banking
{
    class loggedIn
        // controls options when the user is logged in
    {
        public static List<string> options = new List<string> { "withdraw", "deposit", "change password", "quit"};
        public static bool features(string id)
            // use their id when we validate features
        {
            // print available options and let them pick one
            Console.WriteLine("What would you like to do today?");
            foreach (string option in options)
                Console.WriteLine($"    {option}");
            string choice = Console.ReadLine().ToLower();
            Console.Clear();

            // based on their choice, perform that operation
            switch (choice)
            {
                case "withdraw":
                    Console.WriteLine("Sorry, but withdrawing is not yet available.");
                    Console.WriteLine("Press ENTER to continue:");
                    Console.ReadLine();
                    break;
                case "deposit":
                    Console.WriteLine("Sorry, but depositing is not yet available.");
                    Console.WriteLine("Press ENTER to continue:");
                    Console.ReadLine();
                    break;
                case "change password":
                    UpdatePassword.Update(id);
                    break;
                case "quit":
                    break;
                case "":
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"I'm sorry, but {choice} is not a valid option.");
                    features(id);
                    break;
            }
            return true;
        }
    }
}
