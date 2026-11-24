using System.Reflection;

namespace Hannammaltu;

public class Program
{
    public static string Version { get; } = typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;

    static HNMExecutor instance = new();

    public static void Main(string[] args)
    {

        if (args.Length > 0)
        {
            if (File.Exists(args[0]))
            {
                instance.Execute(File.ReadAllLines(args[0]));
                return;
            }
        }

        Console.WriteLine("한남말투랭 (https://github.com/Pro203S/hannammaltu) v" + Version);
        Input();
        return;
    }

    static void Input()
    {
        Console.Write("\n> ");
        string? line = Console.ReadLine();

        if (line == null)
        {
            Input();
            return;
        }

        //instance.ExecuteLine(line);

        Input();
        return;
    }
}
