using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Manages all goals, user score, and save/load functionality
    public class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _score = 0;
        private int _level = 1; // 🎮 Creativity feature: Level system

        // Display all goals
        public void ListGoals()
        {
            Console.WriteLine("\n--- Your Goals ---");
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals created yet.");
                return;
            }

            int count = 1;
            foreach (Goal goal in _goals)
            {
                Console.WriteLine($"{count}. {goal.GetDetailsString()}");
                count++;
            }
        }

        // Create a new goal
        public void CreateGoal()
        {
            Console.WriteLine("\nWhich type of goal would you like to create?");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            Console.Write("Enter the name of your goal: ");
            string name = Console.ReadLine();

            Console.Write("Enter a short description: ");
            string description = Console.ReadLine();

            Console.Write("Enter the point value for this goal: ");
            int points = int.Parse(Console.ReadLine());

            Goal newGoal = null;

            switch (choice)
            {
                case "1":
                    newGoal = new SimpleGoal(name, description, points);
                    break;
                case "2":
                    newGoal = new EternalGoal(name, description, points);
                    break;
                case "3":
                    Console.Write("How many times must this goal be accomplished for a bonus? ");
                    int target = int.Parse(Console.ReadLine());

                    Console.Write("What is the bonus for completing it that many times? ");
                    int bonus = int.Parse(Console.ReadLine());

                    newGoal = new ChecklistGoal(name, description, points, target, bonus);
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    return;
            }

            _goals.Add(newGoal);
            Console.WriteLine("Goal created successfully!");
        }

        // Record event for a selected goal
        public void RecordEvent()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals available to record.");
                return;
            }

            Console.WriteLine("\nWhich goal did you accomplish?");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].ShortName}");
            }

            Console.Write("Enter the number of the goal: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= _goals.Count)
            {
                Goal goal = _goals[choice - 1];
                int earnedPoints = goal.RecordEvent();
                _score += earnedPoints;
                CheckLevelUp(); // 🎮 check for level up
                Console.WriteLine($"Your total score is now: {_score}");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        // Save goals and score to a text file
        public void SaveGoals(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);
                writer.WriteLine(_level); // save level
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine("Goals saved successfully!");
        }

        // Load goals and score from a text file
        public void LoadGoals(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("No saved file found.");
                return;
            }

            _goals.Clear();
            string[] lines = File.ReadAllLines(filename);
            _score = int.Parse(lines[0]);
            _level = int.Parse(lines[1]);

            for (int i = 2; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string type = parts[0];
                string[] data = parts[1].Split(',');

                Goal goal = null;
                switch (type)
                {
                    case "SimpleGoal":
                        goal = new SimpleGoal(data[0], data[1], int.Parse(data[2]));
                        if (bool.TryParse(data[3], out bool done) && done)
                        {
                            // Mark as complete if it was completed before
                            goal.RecordEvent();
                        }
                        break;

                    case "EternalGoal":
                        goal = new EternalGoal(data[0], data[1], int.Parse(data[2]));
                        break;

                    case "ChecklistGoal":
                        int bonus = int.Parse(data[3]);
                        int target = int.Parse(data[4]);
                        int completed = int.Parse(data[5]);
                        ChecklistGoal checklistGoal = new ChecklistGoal(data[0], data[1], int.Parse(data[2]), target, bonus);

                        // Restore completion count
                        for (int j = 0; j < completed; j++)
                        {
                            checklistGoal.RecordEvent();
                        }
                        goal = checklistGoal;
                        break;
                }

                if (goal != null)
                    _goals.Add(goal);
            }

            Console.WriteLine("Goals loaded successfully!");
        }

        // Display score and level
        public void DisplayScore()
        {
            Console.WriteLine($"\nYour current score is: {_score}");
            Console.WriteLine($"🎯 Level: {_level}");
        }

        // 🎮 Creativity feature: check for level-up milestone
        private void CheckLevelUp()
        {
            int newLevel = (_score / 1000) + 1;
            if (newLevel > _level)
            {
                _level = newLevel;
                Console.WriteLine($"🎉 Congratulations! You have reached Level {_level}!");
            }
        }
    }
}
