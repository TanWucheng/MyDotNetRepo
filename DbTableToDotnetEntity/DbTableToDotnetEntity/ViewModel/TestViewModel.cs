using System;
using System.ComponentModel;
using DbTableToDotnetEntity.Domain;

namespace DbTableToDotnetEntity.ViewModel
{
    internal class TestViewModel : INotifyPropertyChanged, IValidationExceptionHandler
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => this.MutateVerbose(ref _name, value, RaisePropertyChanged());
        }

        public bool IsValid { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
