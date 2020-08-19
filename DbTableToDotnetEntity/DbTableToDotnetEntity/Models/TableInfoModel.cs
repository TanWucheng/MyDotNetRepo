using System;
using System.ComponentModel;
using DbTableToDotnetEntity.Domain;

namespace DbTableToDotnetEntity.Models
{
    internal class TableInfoModel : INotifyPropertyChanged
    {
        private bool _isSelected;

        private TableInfo _tableInfo;

        public bool IsSelected
        {
            get => _isSelected;
            set => this.MutateVerbose(ref _isSelected, value, RaisePropertyChanged());
        }
        public TableInfo TableInfo
        {
            get => _tableInfo;
            set => this.MutateVerbose(ref _tableInfo, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
