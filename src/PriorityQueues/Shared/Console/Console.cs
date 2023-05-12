using System.Reflection;

namespace Shared;

public static class ConsoleEx
{
    private static string title = string.Empty;
    private static int batchSize = 250;
    
    public static void Initialize(string title = "", int batchSize = 250)
    {
        var assembly = Assembly.GetEntryAssembly();
        var name = assembly!.FullName!.Split(',')[0];
        
        ConsoleEx.batchSize = batchSize; 
        title = $" {name.Split('.')[0]} - {name.Split('.')[1]}";
        System.Console.Title = title;

        var backgroundColor = Console.BackgroundColor;
        var foregroundColor = Console.ForegroundColor;
        var windowWith = Console.WindowWidth;
        
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(title.PadRight(windowWith - 1));
        
        Console.BackgroundColor = backgroundColor;
        Console.ForegroundColor = foregroundColor;        
    }

    public static void DisplayMenuOptions()
    {
        var backgroundColor = Console.BackgroundColor;
        var foregroundColor = Console.ForegroundColor;
        var windowWith = Console.WindowWidth;

        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("".PadRight(windowWith - 1));
        Console.WriteLine($"  [1] Send a random customer message".PadRight(windowWith - 1));
        Console.WriteLine($"  [2] Send {batchSize} random customer messages".PadRight(windowWith - 1));
        Console.WriteLine($"  [q] To quit".PadRight(windowWith - 1));
        Console.WriteLine("".PadRight(windowWith - 1));
        Console.WriteLine();
        
        Console.BackgroundColor = backgroundColor;
        Console.ForegroundColor = foregroundColor;        

    }
}