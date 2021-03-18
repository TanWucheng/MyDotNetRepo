using Android.Content;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomSheet;
using Ten.Droid.Library.Utils;
using Uri = Android.Net.Uri;

namespace TenBlogDroidApp.Widgets
{
    public class SocialShareBottomSheetDialog : BottomSheetDialog
    {
        public SocialShareBottomSheetDialog(Context context) : base(context)
        {
            Init(context);
        }

        /// <summary>
        ///     展示Toast
        /// </summary>
        /// <param name="context">Activity上下文</param>
        /// <param name="message">消息</param>
        /// <param name="duration">时长</param>
        private void ShowToast(Context context, string message, ToastLength duration = ToastLength.Short)
        {
            Toast.MakeText(context, message, duration)?.Show();
        }

        private void Init(Context context)
        {
            var contentView = LayoutInflater.From(context)
                                ?.Inflate(Resource.Layout.content_social_shara_sheet, null);
            InitBottomShareMenu(context, contentView);

            Window?.AddFlags(WindowManagerFlags.TranslucentStatus); //←重点在这里
            SetContentView(contentView!);
            SetCancelable(true);
            SetCanceledOnTouchOutside(true);
        }

        private void InitBottomShareMenu(Context context, View contentView)
        {
            var sysShareMenu = contentView.FindViewById(Resource.Id.linear_sys_share);
            if (sysShareMenu != null)
            {
                sysShareMenu.Click += delegate
                {
                    var intent = new Intent(Intent.ActionSend);
                    intent.SetType("text/plain");
                    intent.PutExtra(Intent.ExtraText, "内容");
                    context.StartActivity(Intent.CreateChooser(intent, "分享到"));
                    Dismiss();
                };
            }

            var copyUrlMenu = contentView.FindViewById(Resource.Id.linear_copy_url);
            if (copyUrlMenu != null)
            {
                copyUrlMenu.Click += delegate
                {
                    if (context.GetSystemService(Context.ClipboardService) is not ClipboardManager manager) return;
                    var data = ClipData.NewPlainText("shareUrl",
                        "https://tanwucheng.github.io");
                    manager.PrimaryClip = data;
                    ShowToast(context, "链接已复制到剪贴板");
                    Dismiss();
                };
            }

            var smsShareMenu = contentView.FindViewById(Resource.Id.linear_sms_share);
            if (smsShareMenu != null)
            {
                smsShareMenu.Click += delegate
                {
                    var uri = Uri.Parse("smsto:");
                    var intent = new Intent(Intent.ActionSendto, uri);
                    intent.PutExtra("sms_body", "来自Ten's Blog的分享短信，欢迎访问的我的博客网站：https://tanwucheng.github.io");
                    context.StartActivity(intent);
                    Dismiss();
                };
            }

            var emailShareMenu = contentView.FindViewById(Resource.Id.linear_email_share);
            if (emailShareMenu != null)
            {
                emailShareMenu.Click += delegate
                {
                    var intent = new Intent(Intent.ActionSend);
                    intent.SetData(Uri.Parse("mailto:example@example.com"));
                    intent.PutExtra(Intent.ExtraSubject, "欢迎访问我的博客网站");
                    intent.PutExtra(Intent.ExtraText, "<h5>来自Ten's Blog的分享邮件</h5><p><a href='https://tanwucheng.github.io'>点此</a>访问博客网站</p>");
                    context.StartActivity(Intent.CreateChooser(intent, "选择邮箱客户端"));
                    Dismiss();
                };
            }

            var weChatMenu = contentView.FindViewById(Resource.Id.linear_wechat_share);
            if (weChatMenu != null)
            {
                weChatMenu.Click += delegate
                {
                    if (PlatformUtils.IsInstallApp(context, PlatformUtils.WeChatPackage))
                    {
                        Intent intent = new();
                        ComponentName cop = new(PlatformUtils.WeChatPackage, "com.tencent.mm.ui.tools.ShareImgUI");
                        intent.SetComponent(cop);
                        intent.SetAction(Intent.ActionSend);
                        intent.PutExtra("android.intent.extra.TEXT", "https://tanwucheng.github.io");
                        intent.PutExtra("Kdescription", "Ten's Blog网站地址");
                        intent.SetFlags(ActivityFlags.NewTask);
                        context.StartActivity(intent);
                    }
                    else
                    {
                        ShowToast(context, "您需要安装微信客户端");
                    }

                    Dismiss();
                };
            }
        }
    }
}
