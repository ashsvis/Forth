using System.Collections.ObjectModel;

namespace ForthEditor
{
    public class ForthModel : ObservableObject
    {
        private string commandLine = string.Empty;


        public ForthModel() { }

        public ObservableCollection<CommandLine> CommandLines = [];

        public string CommandLine 
        { 
            get => commandLine;
            set
            {
                commandLine = value;
                NotifyPropertyChanged();
            }
        }
    }

    public class CommandLine : ObservableObject
    {
        private string line = string.Empty;

        public CommandLine() { }
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
