using Android.Widget;

namespace TenBlogDroidApp.XRichText
{
    public interface IImageLoader
    {
        void LoadImage(string imagePath, ImageView imageView, int imageHeight);
    }
}