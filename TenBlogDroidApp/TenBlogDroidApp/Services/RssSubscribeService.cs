using System.Collections.Generic;
using System.Threading.Tasks;
using TenBlogDroidApp.RssSubscriber;
using TenBlogDroidApp.RssSubscriber.Models;

namespace TenBlogDroidApp.Services
{
    internal static class RssSubscribeService
    {
        public static Task<List<Entry>> GetBlogEntries()
        {
            return Task.Run(() =>
           {
               var feed = Subscriber.Subscribe("https://tanwucheng.github.io/atom.xml");
               return feed.Entries;
           });
        }
    }
}