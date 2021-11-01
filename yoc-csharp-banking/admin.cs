using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace yoc_csharp_banking
{
    class admin
    {
        public static List<string> validOptions = new List<string> { "add user", "display data" };
        public static List<string> dataFields = new List<string> { "id", "name", "password (SHA256)", "balance"};
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
                case "display data":
                    Array data = userManagement.displayData();
                    int counter = 1;
                    foreach (string field in dataFields)
                    {
                        Console.Write($"{field} | ");
                    }
                    Console.WriteLine();
                    foreach (string point in data)
                    {
                        Console.Write(point);
                        if (counter % 4 == 0)
                        {
                            Console.WriteLine();
                        }
                        else
                            Console.Write(" | ");
                        counter++;
                    }
                    Console.WriteLine("\nPress enter to exit:");
                    Console.ReadLine();
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
