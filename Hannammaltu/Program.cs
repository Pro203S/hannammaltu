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
        REPL();
        return;
    }

    static void REPL()
    {
        var pendingCommands = new List<HNMParser.Command>();
        int repeatDepth = 0;

        while (true)
        {
            Console.Write(repeatDepth > 0 ? "\n| " : "\n> ");
            string? line = Console.ReadLine();
            if (line == null)
            {
                if (pendingCommands.Count > 0)
                    instance.ExecuteCommands(pendingCommands);
                return;
            }

            var commands = HNMParser.ParseLine(line);
            pendingCommands.AddRange(commands);

            foreach (var command in commands)
            {
                if (command == HNMParser.Command.RepeatStart)
                    repeatDepth++;
                else if (command == HNMParser.Command.RepeatEnd)
                {
                    repeatDepth--;
                    if (repeatDepth < 0)
                        break;
                }
            }

            if (repeatDepth > 0)
                continue;

            instance.ExecuteCommands(pendingCommands);
            pendingCommands.Clear();
            repeatDepth = 0;
        }
    }
}
