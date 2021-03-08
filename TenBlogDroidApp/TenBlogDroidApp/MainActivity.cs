using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using Infideap.DrawerBehavior;
using TenBlogDroidApp.Adapters;
using TenBlogDroidApp.Extensions;
using TenBlogDroidApp.Utils;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace TenBlogDroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private Advance3DDrawerLayout _drawer;
        private Toolbar _toolbar;
        private SwipeRefreshLayout _swipeRefreshLayout;
        private RecyclerView _recyclerView;
        private LinearLayoutManager _layoutManager;
        private StandardRecyclerViewAdapter<string> _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitToolbar();
            InitDrawer();
            InitNavigationView();
            InitRefreshLayout();
            InitRecyclerView();
        }

        private void InitRecyclerView()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.rv_main);
            _layoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(_layoutManager);
            var items = new List<string> {
                "<h1>望岳</h1><p><a>杜甫</a></p><p>岱宗夫如何？齐鲁青未了。</p><p>造化钟神秀，阴阳割昏晓。</p><p>会当凌绝顶，一览众山小。</p><p>荡胸生层云，决眦入归鸟。</p>",
                "<h1>出塞</h1><p>王昌龄</p><p>秦时明月汉时关，万里长征人未还。</p><p>但使龙城飞将在，不教胡马度阴山。</p>",
                "<h1>早发白帝城</h1><p>李白</p><p>朝辞白帝彩云间，千里江陵一日还。</p><p>两岸猿声啼不住，轻舟已过万重山。</p>",
                "<h1>枫桥夜泊</h1><p>张继</p><p>月落乌啼霜满天，江枫渔火对愁眠。</p><p>姑苏城外寒山寺，夜半钟声到客船。</p>",
                "<h1>卜算子・咏梅</h1><p>毛泽东 〔近现代〕</p><p>风雨送春归，飞雪迎春到。</p><p>已是悬崖百丈冰，犹有花枝俏。</p><p>俏也不争春，只把春来报。</p><p>待到山花烂漫时，她在丛中笑。</p>"
            };
            _adapter = new StandardRecyclerViewAdapter<string>(
                Resource.Layout.item_blog, items);
            _adapter.OnGetView += Adapter_OnGetView;
            _recyclerView.SetAdapter(_adapter);
        }

        private View Adapter_OnGetView(int position, View convertView, ViewGroup parent, string item, StandardRecyclerViewHolder viewHolder)
        {
            //var textView = viewHolder.GetView<TextView>(Resource.Id.tv_donation_desc);
            //textView.SetHtml(item);

            var tvBlogContent = viewHolder.GetView<TextView>(Resource.Id.tv_blog_content);
            tvBlogContent.SetHtml(item);

            FontManager.MarkAsIconContainer(convertView, FontManager.GetTypeface(this, FontManager.FontAwesome), TypefaceStyle.Normal);

            return convertView;
        }

        private void InitRefreshLayout()
        {
            _swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.srl_main);
            _swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
        }

        private async void SwipeRefreshLayout_Refresh(object sender, System.EventArgs e)
        {
            await Task.Delay(1000);
            _swipeRefreshLayout.Refreshing = false;
        }

        private void InitNavigationView()
        {
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        private void InitDrawer()
        {
            _drawer = FindViewById<Advance3DDrawerLayout>(Resource.Id.drawer_layout);
            var toggle = new ActionBarDrawerToggle(this, _drawer, _toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            if (_drawer != null)
            {
                _drawer.AddDrawerListener(toggle);
                toggle.SyncState();
                _drawer.SetViewScale(GravityCompat.Start, 0.96f);
                _drawer.SetViewElevation(GravityCompat.Start, 8f);
                _drawer.SetViewRotation(GravityCompat.Start, 15f);
            }
        }

        private void InitToolbar()
        {
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_main);
            SetSupportActionBar(_toolbar);
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            Snackbar.Make((View)sender, "FloatingActionButton oncliked", BaseTransientBottomBar.LengthLong).SetAction("Action", v =>
            {
                Toast.MakeText(this, "Snackbar Action", ToastLength.Long).Show();
            }).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
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
            if (_drawer != null)
            {
                _drawer.CloseDrawer(GravityCompat.Start);
            }

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

        private class MyWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                view.LoadUrl(request.Url.ToString());
                return false;
            }
        }
    }
}