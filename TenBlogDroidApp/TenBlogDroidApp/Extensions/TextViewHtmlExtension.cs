using Android.Text;
using Android.Widget;

namespace TenBlogDroidApp.Extensions
{
    public static class TextViewHtmlExtension
    {
        public static void SetHtml(this TextView textView, string html, Html.IImageGetter imageGetter = null, Html.ITagHandler tagHandler = null)
        {
            textView.SetText(Html.FromHtml(html, FromHtmlOptions.ModeCompact, imageGetter, tagHandler), TextView.BufferType.Spannable);
        }
    }
}
