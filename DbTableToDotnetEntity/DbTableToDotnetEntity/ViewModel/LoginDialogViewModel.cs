using System;
using System.ComponentModel;
using DbTableToDotnetEntity.Domain;

namespace DbTableToDotnetEntity.ViewModel
{
    internal class LoginDialogViewModel : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => this.MutateVerbose(ref _name, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
