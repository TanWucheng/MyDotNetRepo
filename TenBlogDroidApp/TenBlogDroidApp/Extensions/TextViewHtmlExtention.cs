using Android.Text;
using Android.Widget;

namespace TenBlogDroidApp.Extensions
{
    public static class TextViewHtmlExtention
    {
        public static void SetHtml(this TextView textView, string html)
        {
            textView.SetText(Html.FromHtml(html, FromHtmlOptions.ModeCompact), TextView.BufferType.Spannable);
        }
    }
}
