using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DbTableToDotnetEntity.Domain
{
    public class DbServerHistoryItem : INotifyPropertyChanged
    {
        private string _ip;
        private string _databaseType;
        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement;
        private Thickness _marginRequirement = new Thickness(16);

        public DbServerHistoryItem(string ip, string databaseType)
        {
            _ip = ip;
            _databaseType = databaseType;
        }

        public string Ip
        {
            get => _ip;
            set => this.MutateVerbose(ref _ip, value, RaisePropertyChanged());
        }

        public string DatabaseType
        {
            get => _databaseType;
            set => this.MutateVerbose(ref _databaseType, value, RaisePropertyChanged());
        }

        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        {
            get => _horizontalScrollBarVisibilityRequirement;
            set => this.MutateVerbose(ref _horizontalScrollBarVisibilityRequirement, value, RaisePropertyChanged());
        }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get => _verticalScrollBarVisibilityRequirement;
            set => this.MutateVerbose(ref _verticalScrollBarVisibilityRequirement, value, RaisePropertyChanged());
        }

        public Thickness MarginRequirement
        {
            get => _marginRequirement;
            set => this.MutateVerbose(ref _marginRequirement, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
