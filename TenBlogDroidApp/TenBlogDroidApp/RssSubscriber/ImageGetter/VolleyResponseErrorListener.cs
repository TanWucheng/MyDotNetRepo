using System;
using Volley;

namespace TenBlogDroidApp.RssSubscriber.ImageGetter
{
    internal class VolleyResponseErrorListener : Java.Lang.Object, Response.IErrorListener
    {
        public void OnErrorResponse(VolleyError p0)
        {
            Console.WriteLine(p0.Message);
        }
    }
}