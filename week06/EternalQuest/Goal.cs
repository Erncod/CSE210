using System;

namespace EternalQuest
{
    // Base class for all goal types
    public abstract class Goal
    {
        private string _shortName;
        private string _description;
        private int _points;

        // Constructor
        public Goal(string name, string description, int points)
        {
            _shortName = name;
            _description = description;
            _points = points;
        }

        // Properties to access member variables
        public string ShortName => _shortName;
        public string Description => _description;
        public int Points => _points;

        // Virtual method for returning goal details
        public virtual string GetDetailsString()
        {
            return $"[{GetStatusSymbol()}] {_shortName} ({_description})";
        }

        // Virtual method to get string for file saving
        public virtual string GetStringRepresentation()
        {
            return $"{GetType().Name}:{_shortName},{_description},{_points}";
        }

        // Abstract method – must be implemented by each derived class
        public abstract int RecordEvent();

        // Abstract property – defines whether a goal is complete
        public abstract bool IsComplete();

        // Protected helper method for display
        protected virtual string GetStatusSymbol()
        {
            return IsComplete() ? "X" : " ";
        }
    }
}
