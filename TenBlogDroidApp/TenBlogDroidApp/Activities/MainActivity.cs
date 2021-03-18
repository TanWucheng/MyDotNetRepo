using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Com.Umeng.Socialize;
using Com.Umeng.Socialize.Bean;
using Com.Umeng.Socialize.Media;
using Google.Android.Material.BottomSheet;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Infideap.DrawerBehavior;
using Plugin.Permissions;
using Ten.Droid.Lib.Extensions;
using Ten.Droid.Lib.RecyclerView.Adapters;
using TenBlogDroidApp.Adapters;
using TenBlogDroidApp.Fragments;
using TenBlogDroidApp.Listeners;
using TenBlogDroidApp.RssSubscriber.Models;
using TenBlogDroidApp.Services;
using TenBlogDroidApp.Widgets;
using Xamarin.Essentials;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using Uri = Android.Net.Uri;

namespace TenBlogDroidApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener,
        IDialogFragmentCallBack, IFabDisplayListener
    {
        private AnimatorAdapter _animatorAdapter;
        private BlogRecyclerViewAdapter _blogAdapter;
        private RecyclerView _blogRecyclerView;
        private SimpleProgressDialogFragment _dialogFragment;
        private Advance3DDrawerLayout _drawer;
        private List<Entry> _entries;
        private FloatingActionButton _fab;
        private string _keywords = string.Empty;
        private LinearLayoutManager _layoutManager;
        private RemovableEditText _searchEditText;
        private StandardRecyclerViewAdapter<string> _searchResultAdapter;
        private RecyclerView _searchResultRecyclerView;
        private SwipeRefreshLayout _swipeRefreshLayout;
        private Toolbar _toolbar;
        private BottomSheetDialog _bottomSheetDialog;

        public void DialogShow()
        {
            _dialogFragment?.Show(SupportFragmentManager, "ProgressDialogFragment");
        }

        public void DialogDismiss()
        {
            _dialogFragment?.Dismiss();
        }

        public void FabShow()
        {
            _fab.Animate()?.TranslationY(0)?.SetInterpolator(new DecelerateInterpolator(3));
        }

        public void FabHide()
        {
            var marinParams = new ViewGroup.MarginLayoutParams(_fab.LayoutParameters);
            _fab.Animate()
                ?.TranslationY(_fab.Height * 2 + marinParams.BottomMargin)
                ?.SetInterpolator(new AccelerateInterpolator(3));
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            switch (menuItem.ItemId)
            {
                case Resource.Id.nav_share:
                    {
                        if (_bottomSheetDialog == null)
                        {
                            _bottomSheetDialog = new BottomSheetDialog(this);
                            var contentView = LayoutInflater.From(this)
                                ?.Inflate(Resource.Layout.social_share_bottom_dialog_content, null);

                            InitBottomShareMenu(contentView);

                            _bottomSheetDialog.Window?.AddFlags(WindowManagerFlags.TranslucentStatus); //←重点在这里
                            _bottomSheetDialog.SetContentView(contentView!);
                            _bottomSheetDialog.SetCancelable(true);
                            _bottomSheetDialog.SetCanceledOnTouchOutside(true);
                        }

                        _bottomSheetDialog.Show();
                        break;
                    }
            }
            menuItem.SetChecked(false);
            _drawer?.CloseDrawer(GravityCompat.Start);

            return true;
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            RequestPermissionAsync();

            SetContentView(Resource.Layout.activity_main);

            InitToolbar();
            InitDrawer();
            InitNavigationView();
            InitRefreshLayout();
            InitRecyclerView();
            InitFab();
            InitSearchResultRecyclerView();
            InitSearchEditText();
            await RssSubscribeAsync();
        }

        private void InitBottomShareMenu(View contentView)
        {
            var sysShareMenu = contentView.FindViewById(Resource.Id.linear_sys_share);
            if (sysShareMenu != null)
                sysShareMenu.Click += delegate
                {
                    var intent = new Intent(Intent.ActionSend);
                    intent.SetType("text/plain");
                    intent.PutExtra(Intent.ExtraText, "内容");
                    StartActivity(Intent.CreateChooser(intent, "分享到"));
                    _bottomSheetDialog.Dismiss();
                };

            var copyUrlMenu = contentView.FindViewById(Resource.Id.linear_copy_url);
            if (copyUrlMenu != null)
                copyUrlMenu.Click += delegate
                {
                    if (GetSystemService(Context.ClipboardService) is not ClipboardManager manager) return;
                    var data = ClipData.NewPlainText("shareUrl",
                        $"");
                    manager.PrimaryClip = data;
                    ShowToast("链接已复制到剪贴板");
                    _bottomSheetDialog.Dismiss();
                };

            var smsShareMenu = contentView.FindViewById(Resource.Id.linear_sms_share);
            if (smsShareMenu != null)
                smsShareMenu.Click += delegate
                {
                    var uri = Uri.Parse("smsto:");
                    var intent = new Intent(Intent.ActionSendto, uri);
                    intent.PutExtra("sms_body", "来自Ten's Blog的分享短信，欢迎访问的我的博客网站：https://tanwucheng.github.io");
                    StartActivity(intent);
                    _bottomSheetDialog.Dismiss();
                };

            var emailShareMenu = contentView.FindViewById(Resource.Id.linear_email_share);
            if (emailShareMenu != null)
                emailShareMenu.Click += delegate
                {
                    var intent = new Intent(Intent.ActionSend);
                    intent.SetData(Uri.Parse("mailto:example@example.com"));
                    intent.PutExtra(Intent.ExtraSubject, "欢迎访问我的博客网站");
                    intent.PutExtra(Intent.ExtraText, "<h5>来自Ten's Blog的分享邮件</h5><p><a href='https://tanwucheng.github.io'>点此</a>访问博客网站</p>");
                    StartActivity(Intent.CreateChooser(intent, "选择邮箱客户端"));
                    _bottomSheetDialog.Dismiss();
                };

            var weChatMenu = contentView.FindViewById(Resource.Id.linear_wechat_share);
            if (weChatMenu != null)
                weChatMenu.Click += delegate
                {
                    var image = new UMImage(this, Resource.Drawable.welcome_background); //资源文件
                    var thumb = new UMImage(this, Resource.Mipmap.ic_launcher_round); //缩略图
                    image.SetThumb(thumb);
                    new ShareAction(this)
                        .SetPlatform(SHARE_MEDIA.Weixin)
                        .WithText("分享测试")
                        .WithMedia(image)
                        .SetCallback(new UmShareListener(this))
                        .Share();
                };

            var weChatCircleMenu = contentView.FindViewById(Resource.Id.linear_wechat_circle);
            if (weChatCircleMenu != null)
                weChatCircleMenu.Click += delegate
                {
                    var thumb = new UMImage(this, Resource.Mipmap.ic_launcher_round); //缩略图
                    var web = new UMWeb("https://tanwucheng.github.io")
                    {
                        Title = "Ten's Blog",
                        Description = "记录分享一些心得-https://tanwucheng.github.io"
                    };
                    web.SetThumb(thumb);
                    new ShareAction(this)
                        .SetPlatform(SHARE_MEDIA.WeixinCircle)
                        .WithText("来自Ten's Blog的分享")
                        .WithMedia(web)
                        .SetCallback(new UmShareListener(this))
                        .Share();
                };
        }

        /// <summary>
        ///     展示Toast
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="duration">时长</param>
        private void ShowToast(string message, ToastLength duration = ToastLength.Short)
        {
            Toast.MakeText(this, message, duration)?.Show();
        }

        /// <summary>
        ///     应用请求系统权限
        /// </summary>
        private async void RequestPermissionAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions
                        .Abstractions.Permission.Storage))
                    {
                        ShowToast("应用程序需要授予存储权限");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                }

                if (status == PermissionStatus.Granted)
                {
                    // ShowToast("成功授予存储权限", ToastLength.Short);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    ShowToast("您已拒绝授予存储权限");
                }
            }
            catch (Exception ex)
            {
                ShowToast("请求授予存储权限发生错误:" + ex.Message, ToastLength.Long);
            }
        }

        /// <summary>
        ///     初始化搜索输入框
        /// </summary>
        private void InitSearchEditText()
        {
            _searchEditText = FindViewById<RemovableEditText>(Resource.Id.edit_search);
            if (_searchEditText != null)
                _searchEditText.TextChanged += (_, e) =>
                {
                    if (e.Text == null) return;
                    var text = e.Text.ToString();
                    _keywords = text;
                    var titles = new List<string>();
                    if (!string.IsNullOrWhiteSpace(text))
                        titles = (from entry in _entries
                                  where entry.Title.ToLower().Contains(text.ToLower())
                                  select entry.Title).Take(20).ToList();
                    _searchResultAdapter.RefreshItems(titles);
                };
        }

        /// <summary>
        ///     初始化搜索结果RecyclerView
        /// </summary>
        private void InitSearchResultRecyclerView()
        {
            _searchResultRecyclerView = FindViewById<RecyclerView>(Resource.Id.rv_search_result);
            if (_searchResultRecyclerView == null) return;
            _searchResultRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            _searchResultAdapter =
                new StandardRecyclerViewAdapter<string>(Android.Resource.Layout.SimpleListItem1,
                    new List<string>());
            _searchResultAdapter.OnGetConvertView += SearchResultAdapter_OnGetConvertView;
            _searchResultRecyclerView.SetAdapter(_searchResultAdapter);
        }

        /// <summary>
        ///     搜索结果RecyclerView适配器OnGetConvertView实现
        /// </summary>
        /// <param name="position"></param>
        /// <param name="parent"></param>
        /// <param name="item"></param>
        /// <param name="viewHolder"></param>
        /// <returns></returns>
        private View SearchResultAdapter_OnGetConvertView(int position, ViewGroup parent, string item,
            StandardRecyclerViewHolder viewHolder)
        {
            var textView = viewHolder.GetView<TextView>(Android.Resource.Id.Text1);
            textView.SetHighLightText(this, item, _keywords,
                Resources?.GetColor(Resource.Color.colorAccent, null) ?? Color.Black);

            return viewHolder.GetConvertView();
        }

        /// <summary>
        ///     初始化RecyclerView
        /// </summary>
        private void InitRecyclerView()
        {
            _blogRecyclerView = FindViewById<RecyclerView>(Resource.Id.rv_main);
            _layoutManager = new LinearLayoutManager(this);
            if (_blogRecyclerView == null) return;
            _blogRecyclerView.SetLayoutManager(_layoutManager);

            _entries = new List<Entry>();

            _blogAdapter = new BlogRecyclerViewAdapter(this, _entries, Resource.Layout.item_blog);
            _animatorAdapter = new ScaleInAnimatorAdapter(_blogAdapter, _blogRecyclerView);
            _blogRecyclerView.SetAdapter(_animatorAdapter);

            _blogRecyclerView.AddOnScrollListener(new FabScrollListener(this));
        }

        /// <summary>
        ///     查询RSS订阅
        /// </summary>
        private async Task RssSubscribeAsync()
        {
            _dialogFragment = SimpleProgressDialogFragment.NewInstance("博文拼命加载中...");
            DialogShow();

            _entries = await RssSubscribeService.GetBlogEntries(this);
            _blogAdapter.RefreshItems(_entries);

            DialogDismiss();
        }

        /// <summary>
        ///     初始化SwipeRefreshLayout
        /// </summary>
        private void InitRefreshLayout()
        {
            _swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.srl_main);
            if (_swipeRefreshLayout != null)
                _swipeRefreshLayout.Refresh += async (_, _) =>
                {
                    await Task.Delay(1000);
                    await RssSubscribeAsync();
                    _swipeRefreshLayout.Refreshing = false;
                };
        }

        /// <summary>
        ///     初始化抽屉NavigationView
        /// </summary>
        private void InitNavigationView()
        {
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView?.SetNavigationItemSelectedListener(this);
        }

        /// <summary>
        ///     初始化抽屉导航栏
        /// </summary>
        private void InitDrawer()
        {
            _drawer = FindViewById<Advance3DDrawerLayout>(Resource.Id.drawer_layout);
            var toggle = new ActionBarDrawerToggle(this, _drawer, _toolbar, Resource.String.navigation_drawer_open,
                Resource.String.navigation_drawer_close);
            if (_drawer == null) return;
            _drawer.AddDrawerListener(toggle);
            toggle.SyncState();
            _drawer.SetViewScale(GravityCompat.Start, 0.96f);
            _drawer.SetViewElevation(GravityCompat.Start, 8f);
            _drawer.SetViewRotation(GravityCompat.Start, 15f);
        }

        /// <summary>
        ///     初始化工具栏
        /// </summary>
        private void InitToolbar()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_main);
            SetSupportActionBar(_toolbar);
        }

        /// <summary>
        ///     初始化FloatingActionButton
        /// </summary>
        private void InitFab()
        {
            _fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            if (_fab != null) _fab.Click += (_, _) => { _blogRecyclerView.SmoothScrollToPosition(0); };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (_drawer != null)
            {
                if (_drawer.IsDrawerOpen(GravityCompat.Start)) _drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_right_drawer:
                    {
                        if (_drawer != null)
                        {
                            _drawer.OpenDrawer(GravityCompat.End);
                            return true;
                        }

                        break;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}