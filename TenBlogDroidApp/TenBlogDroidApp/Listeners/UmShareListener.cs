using Android.Content;
using Android.Widget;
using Com.Umeng.Socialize;
using Com.Umeng.Socialize.Bean;
using Java.Lang;
using Ten.Droid.Lib.Utils;

namespace TenBlogDroidApp.Listeners
{
    internal class UmShareListener : Java.Lang.Object, IUMShareListener
    {
        private readonly Context _context;

        public UmShareListener(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// 分享取消的回调
        /// </summary>
        /// <param name="p0">平台类型</param>
        public void OnCancel(SHARE_MEDIA p0)
        {
            Toast.MakeText(_context, "已取消分享", ToastLength.Short)?.Show();
        }

        /// <summary>
        /// 分享失败的回调
        /// </summary>
        /// <param name="p0">平台类型</param>
        /// <param name="p1">错误原因</param>
        public void OnError(SHARE_MEDIA p0, Throwable p1)
        {
            LogFileUtil.NewInstance(_context)
                .SaveLogToFile("TenBlogDroidApp.Listeners.UmShareListener.OnError分享发生错误:" + p1.Message);
        }

        /// <summary>
        /// 分享成功的回调
        /// </summary>
        /// <param name="p0">平台类型</param>
        public void OnResult(SHARE_MEDIA p0)
        {
            Toast.MakeText(_context, "分享成功", ToastLength.Short)?.Show();
        }

        /// <summary>
        /// 分享开始的回调
        /// </summary>
        /// <param name="p0">平台类型</param>
        public void OnStart(SHARE_MEDIA p0)
        {
        }
    }
}