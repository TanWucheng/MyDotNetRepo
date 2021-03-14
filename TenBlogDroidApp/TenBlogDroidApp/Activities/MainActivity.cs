using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Infideap.DrawerBehavior;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RecyclerViewItemAnimator.Adapter;
using TenBlogDroidApp.Adapters;
using TenBlogDroidApp.Fragments;
using TenBlogDroidApp.Listeners;
using TenBlogDroidApp.Services;
using TenBlogDroidApp.ViewModels;
using Permission = Android.Content.PM.Permission;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace TenBlogDroidApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener,
        IDialogFragmentCallBack, IFabDisplayListener
    {
        private const int AbstractLines = 2;

        private Advance3DDrawerLayout _drawer;
        private Toolbar _toolbar;
        private SwipeRefreshLayout _swipeRefreshLayout;
        private RecyclerView _recyclerView;
        private FloatingActionButton _fab;
        private LinearLayoutManager _layoutManager;
        private SimpleProgressDialogFragment _dialogFragment;
        private AnimatorAdapter _animatorAdapter;
        private BlogRecyclerViewAdapter _blogAdapter;
        private List<BlogEntryViewModel> _entryViewModels;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            RequestPermissionAsync();

            SetContentView(Resource.Layout.activity_main);

            InitToolbar();
            InitDrawer();
            InitNavigationView();
            InitRefreshLayout();
            InitRecyclerView();
            InitFab();
            RssSubscribeAsync();
        }

        public void Show()
        {
            _dialogFragment?.Show(SupportFragmentManager, "ProgressDialogFragment");
        }

        public void Dismiss()
        {
            _dialogFragment?.Dismiss();
        }

        private void ShowToast(string message, ToastLength duration = ToastLength.Short)
        {
            Toast.MakeText(this, message, duration)?.Show();
        }

        private async void RequestPermissionAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Location))
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

        private void InitRecyclerView()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.rv_main);
            _layoutManager = new LinearLayoutManager(this);
            if (_recyclerView == null) return;
            _recyclerView.SetLayoutManager(_layoutManager);

            _entryViewModels = new List<BlogEntryViewModel>();

            _blogAdapter = new BlogRecyclerViewAdapter(this, _entryViewModels, Resource.Layout.item_blog);
            _animatorAdapter = new ScaleInAnimatorAdapter(_blogAdapter, _recyclerView);
            _recyclerView.SetAdapter(_animatorAdapter);

            _recyclerView.AddOnScrollListener(new FabScrollListener(this));
        }

        private async void RssSubscribeAsync()
        {
            _dialogFragment = SimpleProgressDialogFragment.NewInstance("博文拼命加载中...");
            Show();
            _entryViewModels = await RssSubscribeService.GetBlogEntries(this);
            _blogAdapter.RefreshItems(_entryViewModels);
            Dismiss();
        }

        private void InitRefreshLayout()
        {
            _swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.srl_main);
            if (_swipeRefreshLayout != null) _swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
        }

        private async void SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            _swipeRefreshLayout.Refreshing = false;
        }

        private void InitNavigationView()
        {
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView?.SetNavigationItemSelectedListener(this);
        }

        private void InitDrawer()
        {
            _drawer = FindViewById<Advance3DDrawerLayout>(Resource.Id.drawer_layout);
            var toggle = new ActionBarDrawerToggle(this, _drawer, _toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            if (_drawer == null) return;
            _drawer.AddDrawerListener(toggle);
            toggle.SyncState();
            _drawer.SetViewScale(GravityCompat.Start, 0.96f);
            _drawer.SetViewElevation(GravityCompat.Start, 8f);
            _drawer.SetViewRotation(GravityCompat.Start, 15f);
        }

        private void InitToolbar()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_main);
            SetSupportActionBar(_toolbar);
        }

        private void InitFab()
        {
            _fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            if (_fab != null) _fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            _recyclerView.SmoothScrollToPosition(0);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (_drawer != null)
            {
                if (_drawer.IsDrawerOpen(GravityCompat.Start))
                {
                    _drawer.CloseDrawer(GravityCompat.Start);
                }
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            _drawer?.CloseDrawer(GravityCompat.Start);

            return true;
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
    }
}