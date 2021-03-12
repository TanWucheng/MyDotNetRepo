using TenBlogDroidApp.RssSubscriber.Models;

namespace TenBlogDroidApp.ViewModels
{
    public class BlogEntryViewModel
    {
        public Entry Entry { get; set; }

        /// <summary>
        /// 博文简略信息是否处于折叠状态
        /// </summary>
        public bool AbstractExpanded { get; set; }
    }
}