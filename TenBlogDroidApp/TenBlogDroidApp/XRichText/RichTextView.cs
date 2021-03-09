using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Orientation = Android.Widget.Orientation;

namespace TenBlogDroidApp.XRichText
{
    public sealed class RichTextView : ScrollView
    {
        /// <summary>
        /// 插入的图片显示高度
        /// </summary>
        private int _rtImageHeight;
        /// <summary>
        /// 两张相邻图片间距
        /// </summary>
        private int _rtImageBottom = 10;
        /// <summary>
        /// 相当于16sp
        /// </summary>
        private int _rtTextSize = 16;
        /// <summary>
        /// 相当于8dp
        /// </summary>
        private int _rtTextLineSpace = 8;
        private Color _rtTextColor = Color.ParseColor("#757575");
        /// <summary>
        /// 文字相关属性，初始提示信息，文字大小和颜色
        /// </summary>
        private string _rtTextInitHint = "没有内容";
        ///// <summary>
        ///// 图片地址集合
        ///// </summary>
        //private readonly List<string> _imagePaths;
        private LayoutInflater _inflater;
        /// <summary>
        /// 这个是所有子view的容器，scrollView内部的唯一一个ViewGroup
        /// </summary>
        private LinearLayout _allLayout;
        /// <summary>
        /// 新生的view都会打一个tag，对每个view来说，这个tag是唯一的。
        /// </summary>
        private int _viewTagIndex = 1;

        private const int EditNormalPadding = 0;

        /// <summary>
        /// EditText常规padding是10dp
        /// </summary>
        private const int EditPadding = 10;

        /// <summary>
        /// 关键词高亮
        /// </summary>
        private string _keywords;

        /// <summary>
        /// 图片点击事件
        /// </summary>
        private IOnClickListener _btnListener;
        private IOnRtImageClickListener _onRtImageClickListener;

        public RichTextView(Context context) : base(context)
        {
            Initialize(context);
        }

        public RichTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context, attrs);
        }

        public RichTextView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(context, attrs);
        }

        public RichTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(context, attrs);
        }

        private void Initialize(Context context, IAttributeSet attrs = null)
        {
            if (attrs == null) return;
            //获取自定义属性
            var ta = context.ObtainStyledAttributes(attrs, Resource.Styleable.RichTextView);
            _rtImageHeight = ta.GetInteger(Resource.Styleable.RichTextView_rt_view_image_height, 0);
            _rtImageBottom = ta.GetInteger(Resource.Styleable.RichTextView_rt_view_image_bottom, 10);
            _rtTextSize = ta.GetDimensionPixelSize(Resource.Styleable.RichTextView_rt_view_text_size, 16);
            _rtTextLineSpace = ta.GetDimensionPixelSize(Resource.Styleable.RichTextView_rt_view_text_line_space, 8);
            _rtTextColor = ta.GetColor(Resource.Styleable.RichTextView_rt_view_text_color, Color.ParseColor("#757575"));
            _rtTextInitHint = ta.GetString(Resource.Styleable.RichTextView_rt_view_text_init_hint);

            ta.Recycle();

            // _imagePaths = new List<string>();

            _inflater = LayoutInflater.From(context);

            // 1. 初始化allLayout
            _allLayout = new LinearLayout(context) { Orientation = Orientation.Vertical };
            //allLayout.setBackgroundColor(Color.WHITE);//去掉背景
            //setupLayoutTransitions();//禁止载入动画
            var layoutParams = new LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent);
            _allLayout.SetPadding(50, 15, 50, 15); //设置间距，防止生成图片时文字太靠边
            AddView(_allLayout, layoutParams);

            _btnListener = new MyBtnClickListener(_onRtImageClickListener);

            var firstEditParam = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

            //editNormalPadding = dip2px(EDIT_PADDING);
            var firstText = CreateTextView(_rtTextInitHint, Dip2Px(context, EditPadding));
            _allLayout.AddView(firstText, firstEditParam);
        }

        public interface IOnRtImageClickListener
        {
            void OnRtImageClick(View view, string imagePath);
        }

        public void SetOnRtImageClickListener(IOnRtImageClickListener onRtImageClickListener)
        {
            _onRtImageClickListener = onRtImageClickListener;
        }

        private static int Dip2Px(Context context, float dipValue)
        {
            if (context.Resources?.DisplayMetrics == null) return 1;
            var m = context.Resources.DisplayMetrics.Density;
            return (int)(dipValue * m + 0.5f);

        }

        /// <summary>
        /// 生成文本输入框
        /// </summary>
        /// <param name="hint">提示</param>
        /// <param name="paddingTop">Padding Top</param>
        /// <returns></returns>
        public TextView CreateTextView(string hint, int paddingTop)
        {
            var textView = (TextView)_inflater.Inflate(Resource.Layout.rich_textview, null);
            if (textView == null) return null;
            textView.Tag = _viewTagIndex++;
            textView.SetPadding(EditNormalPadding, paddingTop, EditNormalPadding, paddingTop);
            textView.Hint = hint;
            //textView.setTextSize(getResources().getDimensionPixelSize(R.dimen.text_size_16));
            textView.SetTextSize(ComplexUnitType.Px, _rtTextSize);
            textView.SetLineSpacing(_rtTextLineSpace, 1.0f);
            textView.SetTextColor(_rtTextColor);
            return textView;
        }

        /// <summary>
        /// 清除所有的view
        /// </summary>
        public void ClearAllLayout()
        {
            _allLayout.RemoveAllViews();
        }

        /// <summary>
        /// 获得最后一个子view的位置
        /// </summary>
        /// <returns></returns>
        public int GetLastIndex()
        {
            var lastEditIndex = _allLayout.ChildCount;
            return lastEditIndex;
        }

        /// <summary>
        /// 生成图片View
        /// </summary>
        /// <returns></returns>
        private RelativeLayout CreateImageLayout()
        {
            var layout = (RelativeLayout)_inflater.Inflate(Resource.Layout.edit_imageview, null);
            if (layout != null)
            {
                layout.Tag = _viewTagIndex++;
                var closeView = layout.FindViewById(Resource.Id.image_close);
                if (closeView != null) closeView.Visibility = ViewStates.Gone;
                var imageView = layout.FindViewById<DataImageView>(Resource.Id.edit_imageView);
                //imageView.setTag(layout.getTag());
                imageView?.SetOnClickListener(_btnListener);
                return layout;
            }

            return null;
        }

        /// <summary>
        /// 关键字高亮显示
        /// </summary>
        /// <param name="text">需要显示的文字</param>
        /// <param name="target">需要高亮的关键字</param>
        /// <returns>处理完后的结果，记得不要toString()，否则没有效果</returns>
        public static SpannableStringBuilder Highlight(ICharSequence text, string target)
        {
            var builder = new SpannableStringBuilder(text);
            try
            {
                var p = Java.Util.Regex.Pattern.Compile(target);
                var m = p.Matcher(text);
                while (m.Find())
                {
                    CharacterStyle span = new ForegroundColorSpan(Color.ParseColor("#EE5C42"));
                    builder.SetSpan(span, m.Start(), m.End(), SpanTypes.ExclusiveExclusive);
                }
            }
            catch (Java.Lang.Exception e)
            {
                e.PrintStackTrace();
            }
            return builder;
        }

        public void SetKeywords(string keywords)
        {
            _keywords = keywords;
        }

        /// <summary>
        /// 在特定位置插入EditText
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="editStr">EditText显示的文字</param>
        public void AddTextViewAtIndex(int index, ICharSequence editStr)
        {
            try
            {
                var textView = CreateTextView("", EditPadding);
                if (!TextUtils.IsEmpty(_keywords))//搜索关键词高亮
                {
                    var textStr = Highlight(editStr, _keywords);
                    // textView.Text = textStr.ToString();
                    textView.SetText(textStr, TextView.BufferType.Spannable);
                }
                else
                {
                    textView.SetText(editStr, TextView.BufferType.Spannable);
                }

                // 请注意此处，EditText添加、或删除不触动Transition动画
                //allLayout.setLayoutTransition(null);
                _allLayout.AddView(textView, index);
                //allLayout.setLayoutTransition(mTransitioner); // remove之后恢复transition动画
            }
            catch (Java.Lang.Exception e)
            {
                e.PrintStackTrace();
            }
        }

        /// <summary>
        /// 在特定位置添加ImageView
        /// </summary>
        /// <param name="index"></param>
        /// <param name="imagePath"></param>
        public void AddImageViewAtIndex(int index, string imagePath)
        {
            if (TextUtils.IsEmpty(imagePath))
            {
                return;
            }
            // _imagePaths.Add(imagePath);
            var imageLayout = CreateImageLayout();
            if (imageLayout == null)
            {
                return;
            }
            var imageView = imageLayout.FindViewById<DataImageView>(Resource.Id.edit_imageView);
            if (imageView != null)
            {
                imageView.SetAbsolutePath(imagePath);

                XRichText.GetInstance().LoadImage(imagePath, imageView, _rtImageHeight);
            }

            // onActivityResult无法触发动画，此处post处理
            _allLayout.AddView(imageLayout, index);
        }

        /// <summary>
        /// 根据view的宽度，动态缩放bitmap尺寸
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="width">view的宽度</param>
        /// <returns></returns>
        public Bitmap GetScaledBitmap(string filePath, int width)
        {
            if (TextUtils.IsEmpty(filePath))
            {
                return null;
            }
            BitmapFactory.Options options = null;
            try
            {
                options = new BitmapFactory.Options { InJustDecodeBounds = true };
                BitmapFactory.DecodeFile(filePath, options);
                var sampleSize = options.OutWidth > width ? options.OutWidth / width
                                                            + 1 : 1;
                options.InJustDecodeBounds = false;
                options.InSampleSize = sampleSize;
            }
            catch (Java.Lang.Exception e)
            {
                e.PrintStackTrace();
            }
            return BitmapFactory.DecodeFile(filePath, options);
        }

        ///// <summary>
        ///// 初始化transition动画
        ///// </summary>
        //private void SetupLayoutTransitions()
        //{
        //    _mTransitioner = new LayoutTransition();
        //    _allLayout.LayoutTransition = _mTransitioner;
        //    _mTransitioner.AddTransitionListener(new MyTransitionListener());
        //    _mTransitioner.SetDuration(300);
        //}

        public int GetRtImageHeight()
        {
            return _rtImageHeight;
        }

        public void SetRtImageHeight(int rtImageHeight)
        {
            _rtImageHeight = rtImageHeight;
        }

        public int GetRtImageBottom()
        {
            return _rtImageBottom;
        }

        public void SetRtImageBottom(int rtImageBottom)
        {
            _rtImageBottom = rtImageBottom;
        }

        public string GetRtTextInitHint()
        {
            return _rtTextInitHint;
        }

        public void SetRtTextInitHint(string rtTextInitHint)
        {
            _rtTextInitHint = rtTextInitHint;
        }

        public int GetRtTextSize()
        {
            return _rtTextSize;
        }

        public void SetRtTextSize(int rtTextSize)
        {
            _rtTextSize = rtTextSize;
        }

        public int GetRtTextColor()
        {
            return _rtTextColor;
        }

        public void SetRtTextColor(Color rtTextColor)
        {
            _rtTextColor = rtTextColor;
        }

        public int GetRtTextLineSpace()
        {
            return _rtTextLineSpace;
        }

        public void SetRtTextLineSpace(int rtTextLineSpace)
        {
            _rtTextLineSpace = rtTextLineSpace;
        }

        //private class MyTransitionListener : Java.Lang.Object, LayoutTransition.ITransitionListener
        //{
        //    public void EndTransition(LayoutTransition transition, ViewGroup container, View view, LayoutTransitionType transitionType)
        //    {
        //        if (!transition.IsRunning
        //            && transitionType == LayoutTransitionType.ChangeDisappearing)
        //        {
        //            // transition动画结束，合并EditText
        //            // mergeEditText();
        //        }
        //    }

        //    public void StartTransition(LayoutTransition transition, ViewGroup container, View view,
        //        LayoutTransitionType transitionType)
        //    {

        //    }
        //}

        private class MyBtnClickListener : Java.Lang.Object, IOnClickListener
        {
            private readonly IOnRtImageClickListener _onRtImageClickListener;

            public MyBtnClickListener(IOnRtImageClickListener onRtImageClickListener)
            {
                _onRtImageClickListener = onRtImageClickListener;
            }

            public void OnClick(View v)
            {
                if (v is DataImageView imageView)
                {
                    //int currentItem = imagePaths.indexOf(imageView.getAbsolutePath());
                    //Toast.makeText(getContext(),"点击图片："+currentItem+"："+imageView.getAbsolutePath(), Toast.LENGTH_SHORT).show();
                    // 开放图片点击接口
                    _onRtImageClickListener?.OnRtImageClick(imageView, imageView.GetAbsolutePath());
                }
            }
        }
    }
}