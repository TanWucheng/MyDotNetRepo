using System;
using Android.Views;

namespace TenBlogDroidApp.Adapters
{
    /// <summary>
    /// RecyclerView Item点击事件参数
    /// </summary>
    public class RecyclerItemClickEventArgs : EventArgs
    {
        /// <summary>
        /// Item条目根试图
        /// </summary>
        public View View { get; set; }
        /// <summary>
        /// Item在适配器中的位置
        /// </summary>
        public int Position { get; set; }
    }
}
