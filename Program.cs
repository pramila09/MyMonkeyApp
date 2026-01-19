namespace MyMonkeyApp;

/// <summary>
/// Main entry point for the Monkey Console Application.
/// Provides an interactive menu-based interface for exploring monkey species data.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DisplayApplicationBanner();
        RunMainMenu();
    }

    /// <summary>
    /// Displays the application banner with ASCII art.
    /// </summary>
    private static void DisplayApplicationBanner()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"
   ___  ___            _               _____ _     _           
  / _ \/ _ \          | |             |  ___| |   (_)          
 / /_\ \ (_) |___   __| | ___   ___  _| |_  | |    _ _ ____  _ 
 |  _  |> _ </ _ \ / _` |/ _ \ / _ \(_)  _| | |   | | '_ \ \/ /
 | | | / <_> \ (_) | (_| | (_) | (_) |_| |   | |___| | | | >  < 
 \_| |_/\___/ \___/ \__,_|\___/ \___/   \_|   \_____/_|_| |_/_/\_\
                                                                    
        🐒 Monkey Collection Manager 🐒
");
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// Main menu loop for the application.
    /// Displays menu options and handles user selections.
    /// </summary>
    private static void RunMainMenu()
    {
        bool continueRunning = true;

        while (continueRunning)
        {
            Console.Clear();
            DisplayMenuOptions();
            string? choice = Console.ReadLine();

            switch (choice?.Trim().ToLower())
            {
                case "1":
                    ListAllMonkeys();
                    break;
                case "2":
                    GetMonkeyByNameMenu();
                    break;
                case "3":
                    GetRandomMonkeyMenu();
                    break;
                case "4":
                    continueRunning = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️  Invalid option. Please enter 1, 2, 3, or 4.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey(intercept: true);
                    break;
            }
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("👋 Thanks for exploring monkeys! Goodbye!");
        Console.ResetColor();
    }

    /// <summary>
    /// Displays the main menu options.
    /// </summary>
    private static void DisplayMenuOptions()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                   🐒 Monkey App Menu 🐒                    ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("1. List all monkeys");
        Console.WriteLine("2. Get details for a specific monkey");
        Console.WriteLine("3. Get a random monkey");
        Console.WriteLine("4. Exit");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Choose an option (1-4): ");
        Console.ResetColor();
    }

    /// <summary>
    /// Lists all available monkeys with their access counts.
    /// </summary>
    private static void ListAllMonkeys()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║               📋 All Available Monkeys 📋                  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        var monkeys = MonkeyHelper.GetAllMonkeys();

        if (monkeys.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ No monkeys found in the collection.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{"#",-3} {"Name",-25} {"Location",-25} {"Population",-12} {"Accessed",-8}");
            Console.WriteLine(new string('-', 75));
            Console.ResetColor();

            for (int i = 0; i < monkeys.Count; i++)
            {
                var monkey = monkeys[i];
                int accessCount = MonkeyHelper.GetAccessCount(monkey.Name);
                string accessText = accessCount == 1 ? "1 time" : $"{accessCount} times";

                Console.WriteLine($"{i + 1,-3} {monkey.Name,-25} {monkey.Location,-25} {monkey.Population,-12} {accessText,-8}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey(intercept: true);
    }

    /// <summary>
    /// Menu for getting details about a specific monkey by name.
    /// </summary>
    private static void GetMonkeyByNameMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          🔍 Search Monkey by Name 🔍                       ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.Write("Enter monkey name: ");
        string? monkeyName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(monkeyName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Please enter a valid monkey name.");
            Console.ResetColor();
        }
        else
        {
            var monkey = MonkeyHelper.GetMonkeyByName(monkeyName);

            if (monkey == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Monkey '{monkeyName}' not found.");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Available monkeys:");
                var allMonkeys = MonkeyHelper.GetAllMonkeys();
                foreach (var m in allMonkeys)
                {
                    Console.WriteLine($"  • {m.Name}");
                }
            }
            else
            {
                DisplayMonkeyDetails(monkey, isRandom: false);
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey(intercept: true);
    }

    /// <summary>
    /// Menu for selecting and displaying a random monkey.
    /// </summary>
    private static void GetRandomMonkeyMenu()
    {
        var monkey = MonkeyHelper.GetRandomMonkey();

        if (monkey == null)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Error: Could not retrieve a monkey from the collection.");
            Console.ResetColor();
        }
        else
        {
            DisplayMonkeyDetails(monkey, isRandom: true);
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey(intercept: true);
    }

    /// <summary>
    /// Displays detailed information about a monkey with ASCII art.
    /// </summary>
    /// <param name="monkey">The monkey object containing information to display.</param>
    /// <param name="isRandom">Whether this monkey was randomly selected.</param>
    private static void DisplayMonkeyDetails(Monkey monkey, bool isRandom = false)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        string header = isRandom ? "🐒 Random Monkey Selected! 🐒" : "🐒 Monkey Details 🐒";
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║{header.PadLeft((66 + header.Length) / 2).PadRight(66)}║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        // Display ASCII art monkey face
        DisplayMonkeyFace();
        Console.WriteLine();

        // Display monkey name
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"📛 Name: {monkey.Name}");
        Console.ResetColor();

        // Display location
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"📍 Location: {monkey.Location}");
        Console.ResetColor();

        // Display population
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"👥 Population: {monkey.Population:N0}");
        Console.ResetColor();

        // Display coordinates
        Console.ForegroundColor = ConsoleColor.Blue;
        string latDirection = monkey.Latitude >= 0 ? "N" : "S";
        string lonDirection = monkey.Longitude >= 0 ? "E" : "W";
        Console.WriteLine($"🗺️  Coordinates: {Math.Abs(monkey.Latitude):F2}°{latDirection}, {Math.Abs(monkey.Longitude):F2}°{lonDirection}");
        Console.ResetColor();

        // Display details
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("ℹ️  Details:");
        Console.ResetColor();
        Console.WriteLine(WrapText(monkey.Details, 60));

        // Display image URL
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"🖼️  Image: {monkey.Image}");
        Console.ResetColor();

        // Display access count
        Console.WriteLine();
        int accessCount = MonkeyHelper.GetAccessCount(monkey.Name);
        string accessText = accessCount == 1 ? "time" : "times";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"👁️  Accessed: {accessCount} {accessText}");
        Console.ResetColor();

        Console.WriteLine();
    }

    /// <summary>
    /// Displays ASCII art of a monkey face.
    /// </summary>
    private static void DisplayMonkeyFace()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(@"
      ▄▀▀▀▄
     █     █
     █ ◯ ◯ █
     █  V  █
      █   █
       ▀█▀
        │
    ▀▀▀▀ ▀▀▀▀");
        Console.ResetColor();
    }

    /// <summary>
    /// Wraps text to a specified line width for better console display.
    /// </summary>
    /// <param name="text">The text to wrap.</param>
    /// <param name="lineWidth">The maximum width of each line.</param>
    /// <returns>The wrapped text with newlines inserted at appropriate positions.</returns>
    private static string WrapText(string text, int lineWidth)
    {
        var words = text.Split(' ');
        var lines = new List<string>();
        var currentLine = string.Empty;

        foreach (var word in words)
        {
            if ((currentLine + word).Length > lineWidth)
            {
                if (!string.IsNullOrEmpty(currentLine))
                {
                    lines.Add(currentLine.Trim());
                    currentLine = string.Empty;
                }
            }

            currentLine += word + " ";
        }

        if (!string.IsNullOrEmpty(currentLine))
        {
            lines.Add(currentLine.Trim());
        }

        return string.Join(Environment.NewLine, lines);
    }
}
