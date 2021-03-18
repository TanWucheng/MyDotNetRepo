using System;
using Android.App;
using Android.Runtime;
using Com.Umeng.Socialize;

namespace TenBlogDroidApp
{
    [Application]
    public class MyApplication : Application
    {
        public MyApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            PlatformConfig.SetWeixin("wxcca84fd96c2e43bc", "9397a67632149fd48cef3587c61d00a0");
            //PlatformConfig.SetSinaWeibo();
        }
    }
}