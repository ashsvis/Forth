using System.Collections.ObjectModel;

namespace ForthEditor
{
    public class ForthModel : ObservableObject
    {
        private string commandLine = string.Empty;

        private readonly Dictionary<string, Action<ObservableCollection<StackLine>>> words = new() { 
            { "+", (stack) => 
                { 
                    var value2 = stack[0].Value;
                    stack.RemoveAt(0);
                    var value1 = stack[0].Value;
                    stack.RemoveAt(0);
                    stack.Insert(0, new StackLine(value1 + value2));
                } 
            },
            { "-", (stack) =>
                {
                    var value2 = stack[0].Value;
                    stack.RemoveAt(0);
                    var value1 = stack[0].Value;
                    stack.RemoveAt(0);
                    stack.Insert(0, new StackLine(value1 - value2));
                }
            },
            { "*", (stack) =>
                {
                    var value2 = stack[0].Value;
                    stack.RemoveAt(0);
                    var value1 = stack[0].Value;
                    stack.RemoveAt(0);
                    stack.Insert(0, new StackLine(value1 * value2));
                }
            },
            { "/", (stack) =>
                {
                    var value2 = stack[0].Value;
                    stack.RemoveAt(0);
                    var value1 = stack[0].Value;
                    stack.RemoveAt(0);
                    stack.Insert(0, new StackLine(value1 / value2));
                }
            },
            { "dup", (stack) =>
                {
                    // дублирует верхний элемент стека
                    var value = stack[0].Value;
                    stack.Insert(0, new StackLine(value));
                }
            },
            { "drop", (stack) =>
                {
                    // удаляет верхний элемент стека
                    stack.RemoveAt(0);
                }
            },
            { "swap", (stack) =>
                {
                    // меняет местами два верхних элемента стека
                    var value = stack[1].Value;
                    stack.RemoveAt(1);
                    stack.Insert(0, new StackLine(value));
                }
            },
            { "over", (stack) =>
                {
                    // берет второй элемент с вершины стека и дублирует его на вершину стека
                    var value = stack[1].Value;
                    stack.Insert(0, new StackLine(value));
                }
            },
            { "rot", (stack) =>
                {
                    // третий элемент с вершины стека перемещается на вершину стека, вытесняя два других элемента вниз
                    var value = stack[2].Value;
                    stack.RemoveAt(2);
                    stack.Insert(0, new StackLine(value));
                }
            },
        };

        public ForthModel() { }

        public ObservableCollection<CommandLine> CommandLines = [];
        public ObservableCollection<StackLine> StackLines = [];

        public string CommandLine 
        { 
            get => commandLine;
            set
            {
                commandLine = value;
                NotifyPropertyChanged();
            }
        }

        public void EnterCommand(string text)
        {
            var enteredWords = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in enteredWords)
            {
                if (double.TryParse(word, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.GetCultureInfo("en-US"), out double number))
                {
                    StackLines.Insert(0, new StackLine(number));
                    CommandLines.Add(new CommandLine(word + " ok"));
                }
                else if (words.TryGetValue(word, out Action<ObservableCollection<StackLine>>? action))
                {
                    try
                    {
                        action(StackLines);
                    }
                    catch (Exception ex)
                    {
                        StackLines.Clear();
                        CommandLines.Add(new CommandLine(ex.Message));
                    }
                }
                else
                {
                    CommandLines.Add(new CommandLine(text + "?"));
                    break;
                }
            }
            CommandLine = string.Empty;
        }
    }

    public class StackLine : ObservableObject
    {
        private double value = 0.0;

        public StackLine(double value)
        {
            Value = value;
        }

        public double Value
        {
            get => value;
            set
            {
                this.value = value;
                NotifyPropertyChanged();
            }
        }
    }

    public class CommandLine : ObservableObject
    {
        private string line = string.Empty;

        public CommandLine(string line) 
        { 
            Line = line; 
        }

        public string Line 
        { 
            get => line;
            set
            {
                line = value;
                NotifyPropertyChanged();
            }
        }
    }

}
