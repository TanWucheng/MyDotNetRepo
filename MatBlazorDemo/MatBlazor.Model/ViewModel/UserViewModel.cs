using System;
using System.ComponentModel;
using MatBlazor.Model.Model;

namespace MatBlazor.Model.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private bool? _isCheckAll;

        private UserModel[] _userModels;

        public bool? IsCheckAll
        {
            get => _isCheckAll;
            set
            {
                _isCheckAll = value;
                //foreach (var userModel in UserModels)
                //{
                //    userModel.Checked = value ?? false;
                //}

                //if (value.HasValue)
                //{
                //    BatchDelDisabled = !value.Value;
                //}
                //else
                //{
                //    BatchDelDisabled = true;
                //}
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheckAll)));
            }
        }

        public UserModel[] UserModels
        {
            get => _userModels;
            set
            {
                _userModels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserModels)));
            }
        }

        public bool BatchDelDisabled { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
