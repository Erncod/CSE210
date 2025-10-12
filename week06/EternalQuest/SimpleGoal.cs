using System;

namespace EternalQuest
{
    // Represents a simple goal that can be completed once
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        // Constructor
        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
            _isComplete = false; // Default value
        }

        // Override to record completion and return earned points
        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                Console.WriteLine($"Congratulations! You have completed '{ShortName}' and earned {Points} points!");
                return Points;
            }
            else
            {
                Console.WriteLine($"'{ShortName}' is already complete.");
                return 0;
            }
        }

        // Override to check if the goal is complete
        public override bool IsComplete()
        {
            return _isComplete;
        }

        // Include completion status in the file save string
        public override string GetStringRepresentation()
        {
            return $"{GetType().Name}:{ShortName},{Description},{Points},{_isComplete}";
        }
    }
}
