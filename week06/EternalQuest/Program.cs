using System;

namespace EternalQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            bool running = true;

            Console.WriteLine("=======================================");
            Console.WriteLine("   🌟 Welcome to the Eternal Quest! 🌟");
            Console.WriteLine(" Track goals. Earn points. Level up! ");
            Console.WriteLine("=======================================\n");

            while (running)
            {
                Console.WriteLine("\nMenu Options:");
                Console.WriteLine("1. Display Player Info");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Create New Goal");
                Console.WriteLine("4. Record Event");
                Console.WriteLine("5. Save Goals");
                Console.WriteLine("6. Load Goals");
                Console.WriteLine("7. Quit");
                Console.Write("Select a choice from the menu: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        manager.DisplayScore();
                        break;

                    case "2":
                        manager.ListGoals();
                        break;

                    case "3":
                        manager.CreateGoal();
                        break;

                    case "4":
                        manager.RecordEvent();
                        break;

                    case "5":
                        Console.Write("Enter filename to save to (e.g., goals.txt): ");
                        string saveFile = Console.ReadLine();
                        manager.SaveGoals(saveFile);
                        break;

                    case "6":
                        Console.Write("Enter filename to load from (e.g., goals.txt): ");
                        string loadFile = Console.ReadLine();
                        manager.LoadGoals(loadFile);
                        break;

                    case "7":
                        running = false;
                        Console.WriteLine("Goodbye! Keep chasing your goals ✨");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select again.");
                        break;
                }
            }
        }
    }
}
