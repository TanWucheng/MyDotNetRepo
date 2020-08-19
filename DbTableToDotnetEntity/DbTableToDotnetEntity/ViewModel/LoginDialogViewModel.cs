using System;
using System.ComponentModel;
using System.Windows;
using DbTableToDotnetEntity.Domain;

namespace DbTableToDotnetEntity.ViewModel
{
    internal class LoginDialogViewModel : INotifyPropertyChanged, IValidationExceptionHandler
    {
        private string _name;

        private string _password;

        private string _warningMsg;

        private Visibility _visibility;

        public string Name
        {
            get => _name;
            set => this.MutateVerbose(ref _name, value, RaisePropertyChanged());
        }

        public string Password
        {
            get => _password;
            set => this.MutateVerbose(ref _password, value, RaisePropertyChanged());
        }

        public string WarningMsg
        {
            get => _warningMsg;
            set => this.MutateVerbose(ref _warningMsg, value, RaisePropertyChanged());
        }

        public Visibility Visibility
        {
            get => _visibility;
            set => this.MutateVerbose(ref _visibility, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        public bool IsValid { get; set; }
    }
}
