using Android.Widget;

namespace com.sendtion.xrichtext
{
    public class XRichText
    {
        private static XRichText _instance;
        private IImageLoader _imageLoader;
        private static readonly object SyncObject = new object();

        public static XRichText GetInstance()
        {
            if (_instance != null) return _instance;
            lock (SyncObject)
            {
                _instance ??= new XRichText();
            }
            return _instance;
        }

        public void SetImageLoader(IImageLoader imageLoader)
        {
            this._imageLoader = imageLoader;
        }

        public void LoadImage(string imagePath, ImageView imageView, int imageHeight)
        {
            _imageLoader?.LoadImage(imagePath, imageView, imageHeight);
        }
    }
}