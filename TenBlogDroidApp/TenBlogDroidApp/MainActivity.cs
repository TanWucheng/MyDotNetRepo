using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using Infideap.DrawerBehavior;
using Java.IO;
using TenBlogDroidApp.Adapters;
using TenBlogDroidApp.Extensions;
using TenBlogDroidApp.Utils;
using Volley;
using Volley.Toolbox;
using Console = System.Console;
using Environment = Android.OS.Environment;
using File = Java.IO.File;
using FileNotFoundException = Java.IO.FileNotFoundException;
using IOException = Java.IO.IOException;
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
            if (_recyclerView == null) return;
            _recyclerView.SetLayoutManager(_layoutManager);
            var items = new List<string>
            {
                "<h1>望岳</h1><p><a>杜甫</a></p><p>岱宗夫如何？齐鲁青未了。</p><p>造化钟神秀，阴阳割昏晓。</p><p>会当凌绝顶，一览众山小。</p><p>荡胸生层云，决眦入归鸟。</p>",
                "<h1>出塞</h1><p>王昌龄</p><p>秦时明月汉时关，万里长征人未还。</p><p>但使龙城飞将在，不教胡马度阴山。</p>",
                "<h1>早发白帝城</h1><p>李白</p><p>朝辞白帝彩云间，千里江陵一日还。</p><p>两岸猿声啼不住，轻舟已过万重山。</p><p><img src=\"http://i1.fuimg.com/734144/4c6a8dbf96ada12d.png\" alt=\"未找到图片\"/></p>",
                "<h1>枫桥夜泊</h1><p>张继</p><p>月落乌啼霜满天，江枫渔火对愁眠。</p><p>姑苏城外寒山寺，夜半钟声到客船。</p>",
                "<h1>卜算子・咏梅</h1><p>毛泽东 〔近现代〕</p><p>风雨送春归，飞雪迎春到。</p><p>已是悬崖百丈冰，犹有花枝俏。</p><p>俏也不争春，只把春来报。</p><p>待到山花烂漫时，她在丛中笑。</p>"
            };
            _adapter = new StandardRecyclerViewAdapter<string>(
                Resource.Layout.item_main, items);
            _adapter.OnGetView += Adapter_OnGetView;
            _recyclerView.SetAdapter(_adapter);
        }

        private View Adapter_OnGetView(int position, View convertView, ViewGroup parent, string item, StandardRecyclerViewHolder viewHolder)
        {
            //var textView = viewHolder.GetView<RichTextView>(Resource.Id.tv_item_content);
            //textView.ClearAllLayout();
            //textView.AddTextViewAtIndex(textView.GetLastIndex(), new Java.Lang.String(item));

            var tvBlogContent = viewHolder.GetView<TextView>(Resource.Id.tv_item_content);
            tvBlogContent.SetHtml(item, new MyImageGetter(this));

            FontManager.MarkAsIconContainer(convertView, FontManager.GetTypeface(this, FontManager.FontAwesome), TypefaceStyle.Normal);

            return convertView;
        }

        private void InitRefreshLayout()
        {
            _swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.srl_main);
            if (_swipeRefreshLayout != null) _swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
        }

        private async void SwipeRefreshLayout_Refresh(object sender, System.EventArgs e)
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
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            if (fab != null) fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            Snackbar.Make((View)sender, "FloatingActionButton OnClicked", BaseTransientBottomBar.LengthLong).SetAction("Action", _ =>
            {
                Toast.MakeText(this, "Snackbar Action", ToastLength.Long)?.Show();
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

        private class MyResponseListener : Java.Lang.Object, Response.IListener
        {
            private readonly string _fileName;
            private readonly MyImageGetter _imageGetter;

            public MyResponseListener(string fileName, MyImageGetter imageGetter)
            {
                _fileName = fileName;
                _imageGetter = imageGetter;
            }

            public void OnResponse(Java.Lang.Object p0)
            {
                if (p0 is Bitmap bitmap)
                {
                    _imageGetter.SaveBitmap(_fileName, bitmap);
                }
            }
        }

        private class MyResponseErrorListener : Java.Lang.Object, Response.IErrorListener
        {
            public void OnErrorResponse(VolleyError p0)
            {
                Console.WriteLine(p0.Message);
            }
        }

        private class MyImageGetter : Java.Lang.Object, Html.IImageGetter
        {
            private readonly Context _context;

            public MyImageGetter(Context context)
            {
                _context = context;
            }

            public Drawable GetDrawable(string source)
            {
                //Toast.MakeText(_context, source, ToastLength.Long)?.Show();
                //var drawable = _context.Resources?.GetDrawable(Resource.Mipmap.landscape_1, null);
                //if (drawable == null) return null;
                //drawable.SetBounds(0, 0, drawable.IntrinsicWidth,
                //    drawable.IntrinsicHeight);
                //return drawable;
                var fileName = GetFileName(source);
                Drawable drawable = null;
                var absFilePath = $"{_context.GetExternalFilesDir(Environment.DirectoryPictures)}";
                var file = new File(absFilePath, fileName);
                if (file.Exists())
                {
                    drawable = Drawable.CreateFromPath(file.AbsolutePath);
                    drawable?.SetBounds(0, 0, drawable.IntrinsicWidth * 2,
                        drawable.IntrinsicHeight * 2);
                }
                else
                {
                    var hasWriteExternalPermission = ContextCompat.CheckSelfPermission(_context, Manifest.Permission.WriteExternalStorage);
                    if (hasWriteExternalPermission == Permission.Granted)
                    {
                        //TODO 有权限，做自己的后续操作
                        GetNetworkImg(source);
                    }
                    else
                    {
                        //未授权，申请授权(从相册选择图片需要读取存储卡的权限)
                        ActivityCompat.RequestPermissions((AppCompatActivity)_context,
                            new[] { Manifest.Permission.ReadExternalStorage }, 1);
                    }
                }
                return drawable;
            }

            private void GetNetworkImg(string url)
            {

                var requestQueue = Volley.Toolbox.Volley.NewRequestQueue(_context);
                var imageRequest = new ImageRequest(url, new MyResponseListener(GetFileName(url), this)
                    , 0, 0, ImageView.ScaleType.Center, Bitmap.Config.Rgb565, new MyResponseErrorListener());
                requestQueue.Add(imageRequest);
            }

            private static string GetFileName(string path)
            {
                var pos1 = path.LastIndexOf('/');
                var pos2 = path.LastIndexOf('\\');
                var pos = Math.Max(pos1, pos2);
                var str = pos < 0 ? path : path[(pos + 1)..];

                return str;
            }

            public void SaveBitmap(string fileName, Bitmap bitmap)
            {
                var absFileName = $"{_context.GetExternalFilesDir(Environment.DirectoryPictures)}/{fileName}";
                var file = new File(absFileName);
                try
                {
                    file.Mkdir();
                    file.CreateNewFile();
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                    return;
                }

                var fileStream = new FileStream(absFileName, FileMode.Open);
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, fileStream);

                try
                {
                    fileStream.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    fileStream.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}