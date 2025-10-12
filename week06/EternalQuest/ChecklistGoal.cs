using System;

namespace EternalQuest
{
    // Represents a goal that must be completed multiple times
    // before it is considered finished.
    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        // Constructor
        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            _amountCompleted = 0;
            _target = target;
            _bonus = bonus;
        }

        // Each time it's recorded, increment progress
        public override int RecordEvent()
        {
            if (_amountCompleted < _target)
            {
                _amountCompleted++;
                int earnedPoints = Points;

                if (_amountCompleted == _target)
                {
                    earnedPoints += _bonus;
                    Console.WriteLine($"Congratulations! You completed '{ShortName}' and earned {Points} points + {_bonus} bonus points!");
                }
                else
                {
                    Console.WriteLine($"Progress recorded for '{ShortName}'. You earned {Points} points! ({_amountCompleted}/{_target})");
                }

                return earnedPoints;
            }
            else
            {
                Console.WriteLine($"'{ShortName}' has already been completed!");
                return 0;
            }
        }

        // Checks if the goal is fully completed
        public override bool IsComplete()
        {
            return _amountCompleted >= _target;
        }

        // Override to show checklist progress
        public override string GetDetailsString()
        {
            string status = IsComplete() ? "X" : " ";
            return $"[{status}] {ShortName} ({Description}) -- Currently completed: {_amountCompleted}/{_target}";
        }

        // Include progress and bonus info for saving
        public override string GetStringRepresentation()
        {
            return $"{GetType().Name}:{ShortName},{Description},{Points},{_bonus},{_target},{_amountCompleted}";
        }
    }
}
