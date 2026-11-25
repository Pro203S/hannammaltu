using System.Text;

namespace Hannammaltu;

public class HNMParser
{
    public enum Command
    {
        PointerIncrease,
        PointerDecrease,
        ValueAdd,
        ValueSubtract,
        ValueMultiply,
        InputValue,
        OutputValue,
        RepeatStart,
        RepeatEnd
    }

    public static Command[] ParseLine(string line)
    {
        string[] commands = line.Split("+");
        List<Command> enumCommands = [];

        foreach (string command in commands)
        {
            switch (command)
            {
                case "아 예?":
                    enumCommands.Add(Command.PointerDecrease);
                    break;
                case "예?":
                    enumCommands.Add(Command.PointerIncrease);
                    break;
                case "티~원":
                    enumCommands.Add(Command.ValueAdd);
                    break;
                case "아뇨아뇨아뇨":
                    enumCommands.Add(Command.ValueSubtract);
                    break;
                case "엄":
                    enumCommands.Add(Command.ValueMultiply);
                    break;
                case "요이":
                    enumCommands.Add(Command.InputValue);
                    break;
                case "쥐~랄":
                    enumCommands.Add(Command.OutputValue);
                    break;
                case "줴줴이야":
                    enumCommands.Add(Command.RepeatStart);
                    break;
                case "넌 나가라":
                    enumCommands.Add(Command.RepeatEnd);
                    break;
                default: break;
            }
        }

        return [.. enumCommands];
    }

    public enum REPLCommand
    {
        Invalid = -1,
        Exit,
        Debug
    }

    public static REPLCommand ParseREPLCommand(string line)
    {
        switch (line)
        {
            case ".exit":
                return REPLCommand.Exit;
            case ".debug":
                return REPLCommand.Debug;
            default: return REPLCommand.Invalid;
        }
    }
}