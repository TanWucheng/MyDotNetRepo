using Android.Widget;

namespace com.sendtion.xrichtext
{
    public interface IImageLoader
    {
        void LoadImage(string imagePath, ImageView imageView, int imageHeight);
    }
}