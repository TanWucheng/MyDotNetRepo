using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using DE.Hdodenhof.CircleImageViewLib;

namespace TenBlogDroidApp.Adapters
{
    public class BlogViewHolder : RecyclerView.ViewHolder
    {
        public CircleImageView IvBlogPicture { get; }
        public TextView TvBlogTitle { get; }
        public TextView TvBlogAbstract { get; }
        public TextView TvBlogAbstractExpand { get; }
        public TextView TvBlogPublished { get; }
        public TextView TvBlogCategory { get; }

        public BlogViewHolder(View itemView) : base(itemView)
        {
            IvBlogPicture = itemView.FindViewById<CircleImageView>(Resource.Id.iv_blog_picture);
            TvBlogTitle = itemView.FindViewById<TextView>(Resource.Id.tv_blog_title);
            TvBlogAbstract = itemView.FindViewById<TextView>(Resource.Id.tv_blog_abstract);
            TvBlogAbstractExpand = itemView.FindViewById<TextView>(Resource.Id.tv_blog_abstract_expand);
            TvBlogPublished = itemView.FindViewById<TextView>(Resource.Id.tv_blog_published);
            TvBlogCategory = itemView.FindViewById<TextView>(Resource.Id.tv_blog_category);
        }
    }
}