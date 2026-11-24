using System.Text;

namespace Hannammaltu;

public class HNMExecutor
{
    public readonly Dictionary<int, double> _pointerValue = new() { { 0, 0 } };
    public int _currentPointer = 0;

    public void Execute(string[] lines)
    {
        int repeatStart = 0;
        int repeatPointer = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            var commands = HNMParser.ParseLine(line);

            for (int j = 0; j < commands.Length; j++)
            {
                var command = commands[i];
                Console.WriteLine($"i: {i}, j: {j}, rS: {repeatStart}, rP: {repeatPointer}");

                switch (command)
                {
                    case HNMParser.Command.PointerIncrease:
                        _currentPointer++;
                        if (!_pointerValue.ContainsKey(_currentPointer)) _pointerValue[_currentPointer] = 0;
                        continue;
                    case HNMParser.Command.PointerDecrease:
                        _currentPointer--;
                        if (!_pointerValue.ContainsKey(_currentPointer)) _pointerValue[_currentPointer] = 0;
                        continue;
                    case HNMParser.Command.ValueAdd:
                        _pointerValue[_currentPointer] += 1;
                        continue;
                    case HNMParser.Command.ValueSubtract:
                        _pointerValue[_currentPointer] -= 1;
                        continue;
                    case HNMParser.Command.ValueMultiply:
                        _pointerValue[_currentPointer] *= _pointerValue[_currentPointer];
                        continue;
                    case HNMParser.Command.InputValue:
                        _pointerValue[_currentPointer] = (double)Console.ReadKey(true).Key;
                        continue;
                    case HNMParser.Command.OutputValue:
                        if (byte.TryParse(_pointerValue[_currentPointer].ToString(), out byte b))
                            Console.Write(Encoding.UTF8.GetString([b]));
                        else
                            Console.WriteLine("아뇨아뇨아뇨 이건 아스키 코드가 아니잖아요");
                        continue;
                }

                if (command == HNMParser.Command.RepeatStart)
                {
                    repeatStart = i;
                    repeatPointer = _currentPointer;
                    continue;
                }

                if (command == HNMParser.Command.RepeatEnd)
                {
                    if (_pointerValue[repeatPointer] != 0)
                        i = repeatStart;
                    continue;
                }
            }
        }
    }
}