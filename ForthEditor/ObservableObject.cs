using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ForthEditor
{
    /// <summary>
    /// Класс поддержки привязки данных
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
