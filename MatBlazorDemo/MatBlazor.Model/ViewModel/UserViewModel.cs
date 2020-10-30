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

        /// <summary>
        /// 对话框标题
        /// </summary>
        public string DialogTitle { get; set; }

        /// <summary>
        /// 批量删除按钮不可用性
        /// </summary>
        public bool BatchDelDisabled { get; set; }

        /// <summary>
        /// 编辑对话框是否打开
        /// </summary>
        public bool EditDialogOpened { get; set; }

        /// <summary>
        /// 批量删除确认对话框是否打开
        /// </summary>
        public bool BatchDelConfirmDialogOpened { get; set; }

        /// <summary>
        /// 单行删除确认对话框是否打开
        /// </summary>
        public bool DelConfirmDialogOpened { get; set; }

        /// <summary>
        /// 进度条是否关闭
        /// </summary>
        public bool ProgressClosed { get; set; }

        /// <summary>
        /// 对话框取消按钮不可用性
        /// </summary>
        public bool DialogCancelDisabled { get; set; }

        /// <summary>
        /// 对话框确认按钮不可用性
        /// </summary>
        public bool DialogConfirmDisabled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
