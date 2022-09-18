using System.ComponentModel;

namespace CARDINAL.Persephone.Helpers;

public static class Console
{
    public static void Log(string data)
    {
        System.Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"PersephoneLog: {data}");
        System.Console.ForegroundColor = ConsoleColor.White;
    }

    public static void LogWarning(string data)
    {
        System.Console.ForegroundColor = ConsoleColor.DarkYellow;
        WarningException warning = new WarningException($"PersephoneWarning: {data}");
        System.Console.WriteLine(warning.ToString());
        System.Console.ForegroundColor = ConsoleColor.White;
    }

    public static void LogError(string data)
    {
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.Error.WriteLine($"PersephoneError: {data}");
        System.Console.ForegroundColor = ConsoleColor.White;
    }
}