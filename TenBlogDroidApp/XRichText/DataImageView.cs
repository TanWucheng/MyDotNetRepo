using Android.Content;
using Android.Graphics;
using AndroidX.AppCompat.Widget;
using Android.Util;

namespace com.sendtion.xrichtext
{
    /// <summary>
    /// 自定义ImageView，可以存放Bitmap和Path等信息
    /// </summary>
    public class DataImageView : AppCompatImageView
    {
        /// <summary>
        /// 是否显示边框
        /// </summary>
        private bool _showBorder;
        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color _borderColor = Color.Gray;
        /// <summary>
        /// 边框大小
        /// </summary>
        private int _borderWidth = 5;

        private string _absolutePath;
        private Bitmap _bitmap;
        private Paint _paint;

        public DataImageView(Context context) : base(context, null)
        {
        }

        public DataImageView(Context context, IAttributeSet attrs) : base(context, attrs, 0)
        {
        }

        public DataImageView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            InitData();
        }

        private void InitData()
        {
            //画笔
            _paint = new Paint { Color = _borderColor, StrokeWidth = _borderWidth };
            _paint.SetStyle(Paint.Style.Stroke);//设置画笔的风格-不能设成填充FILL否则看不到图片了
        }

        public string GetAbsolutePath()
        {
            return _absolutePath;
        }

        public void SetAbsolutePath(string absolutePath)
        {
            _absolutePath = absolutePath;
        }

        public bool IsShowBorder()
        {
            return _showBorder;
        }

        public void SetShowBorder(bool showBorder)
        {
            _showBorder = showBorder;
        }

        public Bitmap GetBitmap()
        {
            return _bitmap;
        }

        public void SetBitmap(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public int GetBorderColor()
        {
            return _borderColor;
        }

        public void SetBorderColor(Color borderColor)
        {
            _borderColor = borderColor;
        }

        public int GetBorderWidth()
        {
            return _borderWidth;
        }

        public void SetBorderWidth(int borderWidth)
        {
            _borderWidth = borderWidth;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            if (!_showBorder) return;
            //画边框
            var rec = new Rect();
            if (canvas == null) return;
            canvas.GetClipBounds(rec);
            // 这两句可以使底部和右侧边框更大
            //rec.bottom -= 2;
            //rec.right -= 2;
            canvas.DrawRect(rec, _paint);
        }
    }
}