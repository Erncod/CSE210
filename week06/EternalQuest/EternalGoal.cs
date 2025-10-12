using System;

namespace EternalQuest
{
    // Represents an eternal goal that can never be completed,
    // but awards points every time it is recorded.
    public class EternalGoal : Goal
    {
        // Constructor
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        // Each time the goal is recorded, award points (no completion)
        public override int RecordEvent()
        {
            Console.WriteLine($"You have earned {Points} points for recording '{ShortName}'!");
            return Points;
        }

        // Eternal goals are never "complete"
        public override bool IsComplete()
        {
            return false;
        }

        // Uses the base GetDetailsString() and GetStringRepresentation() methods (no need to override)
    }
}
