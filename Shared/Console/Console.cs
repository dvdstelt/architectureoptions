using System.Drawing;
using System.Reflection;
using AnyConsole;

namespace Shared.Console;

public static class Console
{
    static readonly ExtendedConsole console = new ExtendedConsole();
    
    public static ExtendedConsole InitializeConsole(string title)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var location = assembly.Location;
        var demoVersion = location.Substring(assembly.Location.IndexOf("Demo", StringComparison.Ordinal), 6);

        var displayTitle = $"{demoVersion} - {title}";
        System.Console.Title = title;

        console.Configure(config =>
        {
            config.SetStaticRow("Header", RowLocation.Top, Color.White, Color.DarkRed);
            config.SetMaxHistoryLines(1000);
            config.SetLogHistoryContainer(RowLocation.Top, 1, Color.Silver, Color.Black);

            config.SetUpdateInterval(TimeSpan.FromMilliseconds(100));
        });

        console.WriteRow("Header", displayTitle, ColumnLocation.Left, Color.Yellow);
        console.WriteRow("Header", Component.Time, ColumnLocation.Right);
        
        console.Write("Mwahahaha");
        
        console.Start();
        
        return console;
        // var backgroundColor = Console.BackgroundColor;
        // var foregroundColor = Console.ForegroundColor;
        // var windowWith = Console.WindowWidth;
        //
        // Console.BackgroundColor = ConsoleColor.DarkRed;
        // Console.ForegroundColor = ConsoleColor.Yellow;
        // Console.WriteLine($"{demoVersion} - {title}".PadRight(windowWith - 1));
        //
        // Console.BackgroundColor = ConsoleColor.Gray;
        // Console.ForegroundColor = ConsoleColor.DarkRed;
        // Console.WriteLine("  [1] Send a random customer message".PadRight(windowWith - 1));
        // // Console.WriteLine($" [2] Send {BatchSize} random customer messages".PadRight(windowWith - 1));
        // Console.WriteLine("  [q] To quit".PadRight(windowWith - 1));
        //
        // Console.BackgroundColor = backgroundColor;
        // Console.ForegroundColor = foregroundColor;        
    }
}