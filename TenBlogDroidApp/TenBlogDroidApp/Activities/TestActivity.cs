using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Graphics;
using AndroidX.RecyclerView.Widget;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using TenBlogDroidApp.Adapters;
using TenBlogDroidApp.Extensions;
using TenBlogDroidApp.RssSubscriber.ImageGetter;
using TenBlogDroidApp.Utils;

namespace TenBlogDroidApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class TestActivity : Activity
    {
        private RecyclerView _recyclerView;
        private LinearLayoutManager _layoutManager;
        private StandardRecyclerViewAdapter<string> _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            RequestPermissionAsync();

            SetContentView(Resource.Layout.activity_main);

            InitRecyclerView();
        }

        private void ShowToast(string message, ToastLength duration)
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
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        ShowToast("应用程序需要授予存储权限", ToastLength.Short);
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                }

                if (status == PermissionStatus.Granted)
                {
                    ShowToast("成功授予存储权限", ToastLength.Short);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    ShowToast("您已拒绝授予存储权限", ToastLength.Short);
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
            var items = new List<string>
            {
                "<h1>望岳</h1><p><a>杜甫</a></p><p>岱宗夫如何？齐鲁青未了。</p><p>造化钟神秀，阴阳割昏晓。</p><p>会当凌绝顶，一览众山小。</p><p>荡胸生层云，决眦入归鸟。</p>",
                "<h1>出塞</h1><p>王昌龄</p><p>秦时明月汉时关，万里长征人未还。</p><p>但使龙城飞将在，不教胡马度阴山。</p>",
                "<h1>早发白帝城</h1><p>李白</p><p>朝辞白帝彩云间，千里江陵一日还。</p><p>两岸猿声啼不住，轻舟已过万重山。</p><p><img src=\"http://i1.fuimg.com/734144/4c6a8dbf96ada12d.png\" alt=\"未找到图片\"/></p>",
                "<h1>枫桥夜泊</h1><p>张继</p><p>月落乌啼霜满天，江枫渔火对愁眠。</p><p>姑苏城外寒山寺，夜半钟声到客船。</p><p><img src=\"http://i1.fuimg.com/734144/9dedad1db49ccc09.png\" alt=\"未找到图片\"/></p>",
                "<h1>卜算子・咏梅</h1><p>毛泽东 〔近现代〕</p><p>风雨送春归，飞雪迎春到。</p><p>已是悬崖百丈冰，犹有花枝俏。</p><p>俏也不争春，只把春来报。</p><p>待到山花烂漫时，她在丛中笑。</p>"
            };
            _adapter = new StandardRecyclerViewAdapter<string>(
                Resource.Layout.item_test, items);
            _adapter.OnGetView += Adapter_OnGetView;
            _recyclerView.SetAdapter(_adapter);
        }

        private View Adapter_OnGetView(int position, View convertView, ViewGroup parent, string item, StandardRecyclerViewHolder viewHolder)
        {
            var tvBlogContent = viewHolder.GetView<TextView>(Resource.Id.tv_item_content);
            tvBlogContent.SetHtml(item, new HtmlImageGetter<string>(this, _adapter));

            FontManager.MarkAsIconContainer(convertView, FontManager.GetTypeface(this, FontManager.FontAwesome), TypefaceStyle.Normal);

            return convertView;
        }
    }
}