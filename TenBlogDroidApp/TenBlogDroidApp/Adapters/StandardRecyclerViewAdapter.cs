using System;
using System.Collections.Generic;
using Android.Util;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace TenBlogDroidApp.Adapters
{
    /// <summary>
    /// RecyclerView适配器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StandardRecyclerViewAdapter<T> : RecyclerView.Adapter
    {
        /// <summary>
        /// 用以保存checkbox选中状态的数组
        /// </summary>
        public SparseBooleanArray CheckStates { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        private List<T> _items;
        /// <summary>
        /// Item布局Id
        /// </summary>
        private readonly int _layoutId;

        /// <summary>
        /// New instance
        /// </summary>
        /// <param name="layoutId">Item view的layout id</param>
        /// <param name="items">数据集合</param>
        public StandardRecyclerViewAdapter(int layoutId, List<T> items)
        {
            CheckStates = new SparseBooleanArray();
            _layoutId = layoutId;
            _items = items;
        }
        /// <summary>
        /// 声明OnBindViewHolder实现委托事件
        /// </summary>
        /// <param name="position"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <param name="item"></param>
        /// <param name="viewHolder"></param>
        /// <returns></returns>
        public delegate View GetViewEvent(int position, View convertView, ViewGroup parent, T item, StandardRecyclerViewHolder viewHolder);

        /// <summary>
        /// Item View点击委托事件
        /// </summary>
        public event EventHandler<RecyclerItemClickEventArgs> ItemClick;
        /// <summary>
        /// Item View长按委托事件
        /// </summary>
        public event EventHandler<RecyclerItemClickEventArgs> ItemLongClick;
        /// <summary>
        /// 新建OnBindViewHolder实现委托事件
        /// </summary>
        public event GetViewEvent OnGetView;

        /// <summary>
        /// 重写ItemCount
        /// </summary>
        public override int ItemCount => _items.Count;

        /// <summary>
        /// 重写GetItemId
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override long GetItemId(int position) => position;

        /// <summary>
        /// 重写OnBindViewHolder
        /// </summary>
        /// <param name="holder"></param>
        /// <param name="position"></param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _items[position];
            var viewHolder = holder as StandardRecyclerViewHolder;
            OnGetView?.Invoke(position, holder.ItemView, null, item, viewHolder);
        }

        /// <summary>
        /// 重写RecyclerView.ViewHolder
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)?.Inflate(_layoutId, parent, false);
            return StandardRecyclerViewHolder.Get(itemView, parent, OnClick, OnLongClick);
        }

        /// <summary>
        /// 刷新Adapter里的绑定数据集
        /// </summary>
        /// <param name="items">新的数据集</param>
        /// <param name="recyclerView">RecyclerView</param>
        public void RefreshItems(List<T> items, RecyclerView recyclerView)
        {
            _items = items;
            NotifyDataSetChanged();
            // recyclerView.SetItemViewCacheSize(items.Count - 4);
        }

        /// <summary>
        /// Item view点击事件
        /// </summary>
        /// <param name="args"></param>
        protected void OnClick(RecyclerItemClickEventArgs args) => ItemClick?.Invoke(this, args);
        /// <summary>
        /// Item view长按点击事件
        /// </summary>
        /// <param name="args"></param>
        protected void OnLongClick(RecyclerItemClickEventArgs args) => ItemLongClick?.Invoke(this, args);
    }
}
