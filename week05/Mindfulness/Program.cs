using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

/*
 * Mindfulness Program - Console App
 * Implements: BreathingActivity, ReflectionActivity, ListingActivity
 * Uses inheritance: base class Activity, derived classes implement RunActivity
 * Encapsulation: private fields with protected accessors where appropriate
 * 
 * Extra features (to exceed requirements):
 *  - Keeps an in-session history so prompts/questions are not reused until all have been used.
 *  - Logs completed activities to a local file "mindfulness_log.txt" with timestamps and durations.
 *  - Adds a simple additional "Gratitude" activity (demonstrates extending the set).
 *  - Fancy breathing animation that grows/shrinks text length to simulate inhaling/exhaling.
 *
 * Note: This is a console application designed to emulate the referenced demo's behavior.
 */

namespace MindfulnessProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ActivityManager manager = new ActivityManager();
            manager.Run();
        }
    }

    /// <summary>
    /// Manages menu and activity selection, plus logging.
    /// </summary>
    public class ActivityManager
    {
        private readonly List<Activity> _activities;
        private readonly string _logFile = "mindfulness_log.txt";

        public ActivityManager()
        {
            _activities = new List<Activity>
            {
                new BreathingActivity(),
                new ReflectionActivity(),
                new ListingActivity(),
                new GratitudeActivity() // extra activity
            };
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Mindfulness Program ===\n");
                Console.WriteLine("Choose an activity by number:");
                for (int i = 0; i < _activities.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {_activities[i].Name} - {_activities[i].ShortDescription}");
                }
                Console.WriteLine("  0. Exit");
                Console.Write("Selection: ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid selection. Press Enter to continue.");
                    Console.ReadLine();
                    continue;
                }

                if (choice == 0) break;
                if (choice < 0 || choice > _activities.Count)
                {
                    Console.WriteLine("Invalid selection. Press Enter to continue.");
                    Console.ReadLine();
                    continue;
                }

                Activity selected = _activities[choice - 1];
                int duration = PromptForDuration();
                Console.Clear();
                selected.Run(duration);
                LogActivity(selected.Name, duration);

                Console.WriteLine();
                Console.WriteLine("Press Enter to return to the menu...");
                Console.ReadLine();
            }

            Console.WriteLine("Goodbye! Stay mindful.");
        }

        private int PromptForDuration()
        {
            while (true)
            {
                Console.Write("Enter duration in seconds (e.g. 30): ");
                string s = Console.ReadLine();
                if (int.TryParse(s, out int seconds) && seconds > 0)
                    return seconds;
                Console.WriteLine("Please enter a positive integer value for seconds.");
            }
        }

        private void LogActivity(string name, int durationSeconds)
        {
            try
            {
                string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{name}\t{durationSeconds}s";
                File.AppendAllLines(_logFile, new[] { line });
            }
            catch
            {
                // Logging failure should not crash the app; ignore silently.
            }
        }
    }

    /// <summary>
    /// Base class for all activities.
    /// </summary>
    public abstract class Activity
    {
        private readonly string _name;
        private readonly string _description;
        private readonly string _shortDesc;

        protected Activity(string name, string description, string shortDesc)
        {
            _name = name;
            _description = description;
            _shortDesc = shortDesc;
        }

        public string Name => _name;
        public string Description => _description;
        public string ShortDescription => _shortDesc;

        // Common start behavior
        protected void StartRoutine(int durationSeconds)
        {
            Console.WriteLine($"--- {Name} ---\n");
            Console.WriteLine(Description + "\n");
            Console.WriteLine($"This activity will run for {durationSeconds} second(s).");
            Console.WriteLine("Prepare to begin...");
            PauseWithSpinner(4);
        }

        // Common end behavior
        protected void EndRoutine(int durationSeconds)
        {
            Console.WriteLine();
            Console.WriteLine("Well done! You have completed the activity.");
            PauseWithSpinner(3);
            Console.WriteLine($"Activity: {Name}");
            Console.WriteLine($"Duration: {durationSeconds} second(s)");
            PauseWithSpinner(3);
        }

        // Simple spinner animation for N seconds
        protected void PauseWithSpinner(int seconds)
        {
            string[] spinner = { "|", "/", "-", "\\" };
            Stopwatch sw = Stopwatch.StartNew();
            int i = 0;
            while (sw.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(spinner[i % spinner.Length]);
                Thread.Sleep(250);
                Console.Write('\b');
                i++;
            }
            Console.WriteLine();
        }

        protected void Countdown(int seconds, string message = null)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write($"{message ?? i.ToString()} {i}   ");
                Thread.Sleep(1000);
                Console.Write('\r');
            }
            Console.Write(new string(' ', 20) + "\r");
        }

        public abstract void Run(int durationSeconds);
    }

    /// <summary>
    /// Breathing activity implementation.
    /// </summary>
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.",
            "Guided deep breathing")
        { }

        public override void Run(int durationSeconds)
        {
            StartRoutine(durationSeconds);

            Stopwatch sw = Stopwatch.StartNew();
            bool inhale = true;
            int breathPhaseSeconds = 4; // default pacing (4s in, 4s out)

            while (sw.Elapsed.TotalSeconds < durationSeconds)
            {
                if (inhale)
                {
                    Console.Write("Breathe in...");
                    FancyBreathAnimation(breathPhaseSeconds, inhaling: true);
                    inhale = false;
                }
                else
                {
                    Console.Write("Breathe out...");
                    FancyBreathAnimation(breathPhaseSeconds, inhaling: false);
                    inhale = true;
                }

                // small safety: break if time exceeded
                if (sw.Elapsed.TotalSeconds >= durationSeconds) break;
            }

            EndRoutine(durationSeconds);
        }

        // A breathing animation that grows/shrinks a string to emulate lungs
        private void FancyBreathAnimation(int seconds, bool inhaling)
        {
            int steps = Math.Max(2, seconds); // one update per second
            for (int s = 0; s < steps; s++)
            {
                double t = (double)s / (steps - 1 == 0 ? 1 : (steps - 1));
                if (!inhaling) t = 1 - t;
                int width = 2 + (int)Math.Round(t * 20);
                string bar = new string('◼', width);
                Console.Write($" {bar}");
                Thread.Sleep(1000);
                Console.Write('\r');
                Console.Write(new string(' ', width + 2) + '\r');
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Reflection activity implementation.
    /// </summary>
    public class ReflectionActivity : Activity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private readonly Random _rng = new Random();
        private readonly SessionSelector _promptSelector;
        private readonly SessionSelector _questionSelector;

        public ReflectionActivity() : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.",
            "Deep reflection prompts")
        {
            _promptSelector = new SessionSelector(_prompts);
            _questionSelector = new SessionSelector(_questions);
        }

        public override void Run(int durationSeconds)
        {
            StartRoutine(durationSeconds);

            // show random starting prompt
            string prompt = _promptSelector.Next();
            Console.WriteLine("Prompt: " + prompt);
            Console.WriteLine();

            Stopwatch sw = Stopwatch.StartNew();

            // Ask sequential questions (randomized but without repeats until cycle ends)
            while (sw.Elapsed.TotalSeconds < durationSeconds)
            {
                string q = _questionSelector.Next();
                Console.WriteLine(q);
                PauseWithSpinner(5);

                // small check to break if we're out of time
                if (sw.Elapsed.TotalSeconds >= durationSeconds) break;
            }

            EndRoutine(durationSeconds);
        }
    }

    /// <summary>
    /// Listing (Enumeration) activity implementation.
    /// </summary>
    public class ListingActivity : Activity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private readonly SessionSelector _promptSelector;
        private readonly Random _rng = new Random();

        public ListingActivity() : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.",
            "List items quickly (enumeration)")
        {
            _promptSelector = new SessionSelector(_prompts);
        }

        public override void Run(int durationSeconds)
        {
            StartRoutine(durationSeconds);

            string prompt = _promptSelector.Next();
            Console.WriteLine("Prompt: " + prompt);
            Console.WriteLine("You will have a few seconds to think, then list as many items as you can. Press Enter after each item.");
            Countdown(5, "Get ready:");

            List<string> entries = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < durationSeconds)
            {
                Console.Write($"[{(int)(durationSeconds - sw.Elapsed.TotalSeconds)}s left] > ");
                // Wait at most remaining seconds for input — use ReadLineWithDeadline
                string line = ReadLineWithDeadline(durationSeconds - (int)sw.Elapsed.TotalSeconds);
                if (line == null) // timed out
                    break;
                if (!string.IsNullOrWhiteSpace(line)) entries.Add(line.Trim());
            }

            Console.WriteLine($"\nYou listed {entries.Count} item(s):");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {entries[i]}");
            }

            EndRoutine(durationSeconds);
        }

        // Reads a line but enforces a deadline in seconds. If the user doesn't press enter before the deadline, returns null.
        private string ReadLineWithDeadline(int secondsLeft)
        {
            if (secondsLeft <= 0) return null;
            StringBuilder input = new StringBuilder();
            Stopwatch sw = Stopwatch.StartNew();
            while (true)
            {
                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        return input.ToString();
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length > 0)
                        {
                            input.Length -= 1;
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        input.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                }

                if (sw.Elapsed.TotalSeconds >= secondsLeft)
                {
                    Console.WriteLine();
                    return null;
                }
                Thread.Sleep(50);
            }
        }
    }

    /// <summary>
    /// A small extra: Gratitude activity - similar to listing but prompts gratitude sentences.
    /// </summary>
    public class GratitudeActivity : Activity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Name three things you are grateful for today.",
            "Who brightened your day recently?",
            "What small comforts are you thankful for?"
        };

        private readonly SessionSelector _promptSelector;

        public GratitudeActivity() : base(
            "Gratitude Activity",
            "A short activity to identify things you are grateful for — helps shift perspective.",
            "Short gratitude listing")
        {
            _promptSelector = new SessionSelector(_prompts);
        }

        public override void Run(int durationSeconds)
        {
            StartRoutine(durationSeconds);
            Console.WriteLine(_promptSelector.Next());
            Console.WriteLine("You will have a few seconds to think, then list your items. Press Enter after each item.");
            Countdown(4);

            List<string> items = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < durationSeconds)
            {
                Console.Write($"[{(int)(durationSeconds - sw.Elapsed.TotalSeconds)}s left] > ");
                string line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line)) items.Add(line.Trim());
                if (sw.Elapsed.TotalSeconds >= durationSeconds) break;
            }

            Console.WriteLine($"\nYou recorded {items.Count} item(s).");
            EndRoutine(durationSeconds);
        }
    }

    /// <summary>
    /// Utility class to select items in a session without repetition until exhausted.
    /// </summary>
    public class SessionSelector
    {
        private readonly List<string> _items;
        private readonly List<string> _pool;
        private readonly Random _rng = new Random();

        public SessionSelector(IEnumerable<string> items)
        {
            _items = new List<string>(items);
            _pool = new List<string>(_items);
        }

        public string Next()
        {
            if (_pool.Count == 0)
            {
                // refill pool
                _pool.AddRange(_items);
            }

            int idx = _rng.Next(_pool.Count);
            string selected = _pool[idx];
            _pool.RemoveAt(idx);
            return selected;
        }
    }
}
