using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ForthEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ForthModel model;

        public MainWindow()
        {
            InitializeComponent();
            model = (ForthModel)DataContext;
            lbxCommands.ItemsSource = model.CommandLines;
            lbxStack.ItemsSource = model.StackLines;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void cmdLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                model.EnterCommand(cmdLine.Text);
                ScrollIntoView();
                cmdLine.Focus();
            }
        }

        private void ScrollIntoView()
        {
            if (lbxCommands.Items.Count == 0) return;
            var index = lbxCommands.Items.Count - 1;
            var item = lbxCommands.Items.GetItemAt(index);
            lbxCommands.ScrollIntoView(item);
        }
    }
}