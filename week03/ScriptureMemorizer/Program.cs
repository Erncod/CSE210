using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // --- Scripture Library ---
        List<(Reference, string)> scriptures = new List<(Reference, string)>
        {
            (new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart and lean not unto thine own understanding."),
            (new Reference("John", 3, 16), "For God so loved the world that he gave his only begotten Son, that whosoever believeth in him should not perish but have everlasting life."),
            (new Reference("Psalm", 23, 1), "The Lord is my shepherd I shall not want.")
        };

        // Pick a random scripture
        Random rand = new Random();
        var chosen = scriptures[rand.Next(scriptures.Count)];
        Scripture scripture = new Scripture(chosen.Item1, chosen.Item2);

        // --- Difficulty Selection ---
        Console.WriteLine("Choose difficulty: Easy(1), Medium(3), Hard(5)");
        int wordsToHide = 3; // default medium
        string choice = Console.ReadLine();

        if (choice == "1")
            wordsToHide = 1;
        else if (choice == "3")
            wordsToHide = 3;
        else if (choice == "5")
            wordsToHide = 5;

        // --- Main Loop ---
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(wordsToHide);

            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words hidden. Program will now exit.");
                break;
            }
        }
    }
}

/*
EXCEED REQUIREMENTS:
1. Program allows user to choose difficulty level (1, 3, or 5 words hidden per step).
2. Program randomly selects a scripture from a library instead of always using the same one.
*/
