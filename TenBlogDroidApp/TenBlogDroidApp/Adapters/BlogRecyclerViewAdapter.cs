using Android.Content;
using Android.Views;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using Android.Text;
using AndroidX.RecyclerView.Widget;
using TenBlogDroidApp.Utils;
using TenBlogDroidApp.ViewModels;

namespace TenBlogDroidApp.Adapters
{
    public class BlogRecyclerViewAdapter : RecyclerView.Adapter
    {
        private const int AbstractLines = 2;

        private readonly Context _context;
        private List<BlogEntryViewModel> _entryViewModels;
        private readonly int _itemViewId;

        public BlogRecyclerViewAdapter(Context context, List<BlogEntryViewModel> entryViewModels, int itemViewId)
        {
            _context = context;
            _entryViewModels = entryViewModels;
            _itemViewId = itemViewId;
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is not BlogViewHolder blogViewHolder) return;
            var item = _entryViewModels[position];
            var categories = string.Join(", ", from category in item.Entry.Categories select category.Term);

            blogViewHolder.IvBlogPicture.SetImageResource(categories.Contains("笔记")
                ? Resource.Drawable.ic_event_note_black_48dp
                : Resource.Drawable.ic_code_black_48dp);

            blogViewHolder.TvBlogAbstract.Text = item.Entry.Summary.Content;
            blogViewHolder.TvBlogAbstractExpand.Tag = item.Entry.Id;
            blogViewHolder.TvBlogCategory.Text = categories;
            blogViewHolder.TvBlogPublished.Text = $"{item.Entry.Published:yyyy-MM-dd}";
            blogViewHolder.TvBlogTitle.Text = item.Entry.Title;

            blogViewHolder.TvBlogAbstractExpand.Click += delegate
            {
                // 未展开
                if (!item.AbstractExpanded)
                {
                    blogViewHolder.TvBlogAbstract.Ellipsize = null;
                    blogViewHolder.TvBlogAbstract.SetSingleLine(false);
                    blogViewHolder.TvBlogAbstractExpand.SetText(Resource.String.fa_chevron_up);
                }
                else
                {
                    blogViewHolder.TvBlogAbstract.Ellipsize = TextUtils.TruncateAt.End;
                    blogViewHolder.TvBlogAbstract.SetLines(AbstractLines);
                    blogViewHolder.TvBlogAbstractExpand.SetText(Resource.String.fa_chevron_down);
                }

                item.AbstractExpanded = !item.AbstractExpanded;
            };

            FontManager.MarkAsIconContainer(holder.ItemView, FontManager.GetTypeface(_context, FontManager.FontAwesome), TypefaceStyle.Normal);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(_context)?.Inflate(_itemViewId, parent, false);
            return new BlogViewHolder(view);
        }

        public override int ItemCount => _entryViewModels.Count;

        /// <summary>
        /// 刷新Adapter里的绑定数据集
        /// </summary>
        /// <param name="items">新的数据集</param>
        /// <param name="recyclerView">RecyclerView</param>
        public void RefreshItems(List<BlogEntryViewModel> items, RecyclerView recyclerView)
        {
            _entryViewModels = items;
            NotifyDataSetChanged();
            recyclerView.SetItemViewCacheSize(items.Count - 4);
        }
    }
}