using System.Text;

namespace Hannammaltu;

public class HNMExecutor
{
    public readonly Dictionary<int, double> _pointerValue = new() { { 0, 0 } };
    public int _currentPointer = 0;

    public void Execute(string[] lines)
    {
        ExecuteCommands(lines.SelectMany(HNMParser.ParseLine).ToArray());
    }

    internal void ExecuteCommands(IReadOnlyList<HNMParser.Command> commands)
    {
        var repeatStarts = new Stack<int>();
        var repeatJumps = new Dictionary<int, int>();

        for (int i = 0; i < commands.Count; i++)
        {
            switch (commands[i])
            {
                case HNMParser.Command.RepeatStart:
                    repeatStarts.Push(i);
                    break;
                case HNMParser.Command.RepeatEnd:
                    if (!repeatStarts.TryPop(out int start))
                    {
                        Console.Error.WriteLine("아 예? 엄 줴줴이야");
                        return;
                    }

                    repeatJumps.Add(start, i);
                    repeatJumps.Add(i, start);
                    break;
            }
        }

        if (repeatStarts.Count > 0)
        {
            Console.Error.WriteLine("아 예? 엄 넌 나가라");
            return;
        }

        for (int i = 0; i < commands.Count; i++)
        {
            switch (commands[i])
            {
                case HNMParser.Command.PointerIncrease:
                    _currentPointer++;
                    if (!_pointerValue.ContainsKey(_currentPointer)) _pointerValue[_currentPointer] = 0;
                    break;
                case HNMParser.Command.PointerDecrease:
                    _currentPointer--;
                    if (!_pointerValue.ContainsKey(_currentPointer)) _pointerValue[_currentPointer] = 0;
                    break;
                case HNMParser.Command.ValueAdd:
                    _pointerValue[_currentPointer] += 1;
                    break;
                case HNMParser.Command.ValueSubtract:
                    _pointerValue[_currentPointer] -= 1;
                    break;
                case HNMParser.Command.ValueMultiply:
                    _pointerValue[_currentPointer] *= _pointerValue[_currentPointer];
                    break;
                case HNMParser.Command.InputValue:
                    _pointerValue[_currentPointer] = (double)Console.ReadKey(true).Key;
                    break;
                case HNMParser.Command.OutputValue:
                    if (byte.TryParse(_pointerValue[_currentPointer].ToString(), out byte b))
                        Console.Write(Encoding.UTF8.GetString([b]));
                    else
                        Console.WriteLine("아뇨아뇨아뇨 이건 아스키 코드가 아니잖아요");
                    break;
                case HNMParser.Command.RepeatStart:
                    if (_pointerValue[_currentPointer] == 0)
                        i = repeatJumps[i];
                    break;
                case HNMParser.Command.RepeatEnd:
                    if (_pointerValue[_currentPointer] != 0)
                        i = repeatJumps[i];
                    break;
            }
        }
    }
}
