using System;
using System.ComponentModel;
using DbTableToDotnetEntity.Domain;

namespace DbTableToDotnetEntity.Models
{
    /// <summary>
    /// 数据库连接字符串模型
    /// </summary>
    public class DbServerInfo : INotifyPropertyChanged
    {
        private string _ip;

        private string _database;

        private string _userId;

        private string _password;

        private string _encoding;

        private string _tcpIpPort;

        private string _databaseType;

        public string Ip
        {
            get => _ip;
            set => this.MutateVerbose(ref _ip, value, RaisePropertyChanged());
        }

        public string Database
        {
            get => _database;
            set => this.MutateVerbose(ref _database, value, RaisePropertyChanged());
        }

        public string UserId
        {
            get => _userId;
            set => this.MutateVerbose(ref _userId, value, RaisePropertyChanged());
        }

        public string Password
        {
            get => _password;
            set => this.MutateVerbose(ref _password, value, RaisePropertyChanged());
        }

        public string Encoding
        {
            get => _encoding;
            set => this.MutateVerbose(ref _encoding, value, RaisePropertyChanged());
        }

        public string TcpIpPort
        {
            get => _tcpIpPort;
            set => this.MutateVerbose(ref _tcpIpPort, value, RaisePropertyChanged());
        }

        public string DatabaseType
        {
            get => _databaseType;
            set => this.MutateVerbose(ref _databaseType, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
