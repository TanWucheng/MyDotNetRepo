using System;
using System.Collections.Generic;
using Android.Util;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace TenBlogDroidApp.Adapters
{
    /// <summary>
    /// RecyclerView视图支架
    /// </summary>
    public class StandardRecyclerViewHolder : RecyclerView.ViewHolder
    {
        private readonly Dictionary<int, View> _views;
        private readonly View _convertView;

        /// <summary>
        /// New Instance
        /// </summary>
        /// <param name="itemView">RecyclerView的Item View</param>
        /// <param name="parent">父级视图</param>
        /// <param name="clickListener">点击响应事件</param>
        /// <param name="longClickListener">长按响应时间</param>
        public StandardRecyclerViewHolder(View itemView, ViewGroup parent, Action<RecyclerItemClickEventArgs> clickListener, Action<RecyclerItemClickEventArgs> longClickListener) : base(itemView)
        {
            _views = new Dictionary<int, View>();
            _convertView = itemView;
            itemView.Click += (sender, e) => clickListener?.Invoke(new RecyclerItemClickEventArgs
            {
                View = itemView,
                Position = AdapterPosition
            });
            itemView.LongClick += (sender, e) => longClickListener?.Invoke(new RecyclerItemClickEventArgs
            {
                View = itemView,
                Position = AdapterPosition
            });
            _convertView.Tag = this;
        }

        /// <summary>
        /// 单例模式获取对象实例
        /// </summary>
        /// <param name="itemView">RecyclerView子级View</param>
        /// <param name="parent">父级视图</param>
        /// <param name="clickListener">子级view点击事件</param>
        /// <param name="longClickListener">子级view的长按点击事件</param>
        /// <returns></returns>
        public static StandardRecyclerViewHolder Get(View itemView, ViewGroup parent, Action<RecyclerItemClickEventArgs> clickListener, Action<RecyclerItemClickEventArgs> longClickListener)
        {
            if (itemView == null)
            {
                return new StandardRecyclerViewHolder(itemView, parent, clickListener, longClickListener);
            }
            else
            {
                return new StandardRecyclerViewHolder(itemView, parent, clickListener, longClickListener);
            }
        }

        /// <summary>
        /// 获取填装好后的View
        /// </summary>
        /// <returns></returns>
        public View GetConvertView() => _convertView;

        /// <summary>  
        /// 通过ViewID获取控件  
        /// </summary>  
        /// <typeparam name="T">view类型</typeparam>  
        /// <param name="viewId">view的id</param>  
        /// <returns></returns>  
        public T GetView<T>(int viewId) where T : View
        {
            _views.TryGetValue(viewId, out var view);
            if (view == null)
            {
                view = _convertView.FindViewById<T>(viewId);
                _views.Add(viewId, view);
            }
            return (T)view;
        }

        /// <summary>
        /// 通过ViewId给控件添加点击监听事件
        /// </summary>
        /// <typeparam name="T">view类型</typeparam>
        /// <param name="viewId">view的id</param>
        /// <param name="widget">小装置</param>
        /// <param name="listener">监听事件</param>
        /// <returns></returns>
        public StandardRecyclerViewHolder SetOnClickListener<T>(int viewId, T widget, View.IOnClickListener listener)
            where T : View
        {
            View view = GetView<T>(viewId);
            view.SetOnClickListener(listener);
            return this;
        }
    }
}
