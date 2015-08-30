using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Editable.Host.Mvvm
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void UpdateProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (backingField?.Equals(value) == true)
            {
                return;
            }
            backingField = value;
            OnPropertyChanged(propertyName);
        }
    }
}
