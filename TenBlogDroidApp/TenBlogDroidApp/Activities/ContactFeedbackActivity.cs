using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Button;
using Java.IO;
using TenBlogDroidApp.Utils;
using Xamarin.Essentials;
using Uri = Android.Net.Uri;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using Ten.Droid.Library.Extensions;
using Android.Views;
using Android.Text;

namespace TenBlogDroidApp.Activities
{
    [Activity(Label = "ContactFeedback", Theme = "@style/AppTheme.NoActionBar")]
    public class ContactFeedbackActivity : AppCompatActivity
    {
        private Toolbar _toolbar;
        private MaterialButton _contactButton;
        private MaterialButton _feedbackButton;
        private TextView _statmentTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_contact_feedback);

            InitToolbar();
            InitStatment();
            InitContactButton();
            InitFeedbackButton();
        }

        private void InitToolbar()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_contact_feedback);
            if (_toolbar == null) return;
            _toolbar.Title = Resources.GetString(Resource.String.contact_feedback);
            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        private void InitStatment()
        {
            _statmentTextView = FindViewById<TextView>(Resource.Id.tv_feedback_statement);
            if (_statmentTextView == null) return;
            _statmentTextView.SetHtml(Constants.AppStatement);
        }

        private void InitContactButton()
        {
            _contactButton = FindViewById<MaterialButton>(Resource.Id.button_contact);
            if (_contactButton == null) return;
            _contactButton.Click += delegate
            {
                SnackbarUtil.Show(this, _contactButton, "海内存知己，天涯若比邻。");
            };
        }

        private void InitFeedbackButton()
        {
            _feedbackButton = FindViewById<MaterialButton>(Resource.Id.button_feedback);
            if (_feedbackButton == null) return;
            _feedbackButton.Click += delegate
            {
                Intent intent = new(Intent.ActionSend);
                //intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.SetData(Uri.Parse("mailto:tanwucheng@outlook.com"));
                intent.PutExtra(Intent.ExtraSubject, "示例标题:运行Bug日志反馈");
                var appDocPath = FilesDir?.AbsolutePath;
                var absFilePath = System.IO.Path.Combine(appDocPath ?? string.Empty, $"applog_{DateTime.Now:yyyyMMdd}.log");
                var logUri = AndroidX.Core.Content.FileProvider.GetUriForFile(this, PackageName + ".fileprovider", new File(absFilePath));
                //GrantUriPermission("com.microsoft.office.outlook", logUri, ActivityFlags.GrantReadUriPermission);
                intent.PutExtra(Intent.ExtraText,
                    $"<h2 style='color:#ff5722;'>App运行错误日志反馈</h2><p style='color:#00bcd4;'>注：邮件附件默认添加的最新一份App运行错误日志，如果您有其他疑问或者建议，请在正文里补充。</p><p style='color:#2196F3;'>{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}</p>");
                intent.PutExtra(Intent.ExtraStream, logUri);
                StartActivityForResult(intent, RequestCodes.SendEmail);
            };
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    {
                        base.OnBackPressed();
                        return true;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
