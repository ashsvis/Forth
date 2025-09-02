using System.Collections.ObjectModel;

namespace ForthEditor
{
    public class ForthModel : ObservableObject
    {
        private string commandLine = string.Empty;


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
            if (double.TryParse(text, System.Globalization.NumberStyles.Float, 
                System.Globalization.CultureInfo.GetCultureInfo("en-US"),  out double number))
            {
                StackLines.Insert(0, new StackLine(number));
                CommandLines.Add(new CommandLine(text));
                CommandLines.Add(new CommandLine("ok"));
            }
            else
            {
                CommandLines.Add(new CommandLine(text + "?"));
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
