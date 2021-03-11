using System.Diagnostics.CodeAnalysis;
using Android.Animation;
using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using RecyclerViewItemAnimator.Adapter.Helper;
using Object = Java.Lang.Object;

namespace RecyclerViewItemAnimator.Adapter
{
    public abstract class AnimatorAdapter : RecyclerView.Adapter
    {
        private readonly RecyclerView.Adapter _adapter;

        /// <summary>
        /// Alpha property
        /// </summary>
        private const string Alpha = "alpha";

        /// <summary>
        /// The ViewAnimator responsible for animating the Views.
        /// </summary>
        private readonly RecyclerViewAnimator _viewAnimator;

        /// <summary>
        /// RecyclerView
        /// </summary>
        protected readonly RecyclerView RecyclerView;

        protected AnimatorAdapter(RecyclerView.Adapter adapter, RecyclerView recyclerView)
        {
            _adapter = adapter;
            _viewAnimator = new RecyclerViewAnimator(recyclerView);
            RecyclerView = recyclerView;
        }

        /// <summary>
        /// Returns the Animators to apply to the views.
        /// </summary>
        /// <param name="view">The view that will be animated, as retrieved by OnBindViewHolder().</param>
        /// <returns></returns>
        protected abstract Animator[] GetAnimators([NotNull] View view);

        /// <summary>
        /// Animates given View
        /// </summary>
        /// <param name="view">the View that should be animated.</param>
        /// <param name="position">the position of the item the View represents.</param>
        private void AnimateView(View view, int position)
        {
            var animators = GetAnimators(view);
            Animator alphaAnimator = ObjectAnimator.OfFloat(view, Alpha, 0, 1);
            var concatAnimators = AnimatorUtil.ConcatAnimators(animators, alphaAnimator);
            _viewAnimator.AnimateViewIfNecessary(position, view, concatAnimators);
        }
        public RecyclerViewAnimator GetViewAnimator()
        {
            return _viewAnimator;
        }

        /// <summary>
        /// Returns a Parcelable object containing the AnimationAdapter's current dynamic state.
        /// </summary>
        /// <returns></returns>
        public IParcelable OnSaveInstanceState()
        {
            IParcelable parcelable = new RecyclerViewAnimator.AnimateStateParcelable();
            if (_viewAnimator != null)
            {
                parcelable = _viewAnimator.OnSaveInstanceState();
            }

            return parcelable;
        }

        /// <summary>
        /// Restores this AnimationAdapter's state.
        /// </summary>
        /// <param name="parcelable">the Parcelable object previously returned by OnSaveInstanceState().</param>
        public void OnRestoreInstanceState([AllowNull] IParcelable parcelable)
        {
            if (parcelable is RecyclerViewAnimator.AnimateStateParcelable animateState)
            {
                _viewAnimator?.OnRestoreInstanceState(animateState);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return _adapter.OnCreateViewHolder(parent, viewType);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            _adapter.OnBindViewHolder(holder, position);
            _viewAnimator.CancelExistingAnimation(holder.ItemView);
            AnimateView(holder.ItemView, position);
        }

        public override int ItemCount => _adapter.ItemCount;

        public override int GetItemViewType(int position)
        {
            return _adapter.GetItemViewType(position);
        }

        public override void RegisterAdapterDataObserver(RecyclerView.AdapterDataObserver observer)
        {
            base.RegisterAdapterDataObserver(observer);
            _adapter.RegisterAdapterDataObserver(observer);
        }

        public override void UnregisterAdapterDataObserver(RecyclerView.AdapterDataObserver observer)
        {
            base.UnregisterAdapterDataObserver(observer);
            _adapter.UnregisterAdapterDataObserver(observer);
        }

        public override long GetItemId(int position)
        {
            return _adapter.GetItemId(position);
        }

        public override void OnViewRecycled(Object holder)
        {
            _adapter.OnViewRecycled(holder);
        }

        public override bool OnFailedToRecycleView(Object holder)
        {
            return _adapter.OnFailedToRecycleView(holder);
        }

        public override void OnViewAttachedToWindow(Object holder)
        {
            _adapter.OnViewAttachedToWindow(holder);
        }

        public override void OnViewDetachedFromWindow(Object holder)
        {
            _adapter.OnViewDetachedFromWindow(holder);
        }

        public override void OnAttachedToRecyclerView(RecyclerView recyclerView)
        {
            _adapter.OnAttachedToRecyclerView(recyclerView);
        }

        public override void OnDetachedFromRecyclerView(RecyclerView recyclerView)
        {
            _adapter.OnDetachedFromRecyclerView(recyclerView);
        }

        public void RefreshItems()
        {
            NotifyDataSetChanged();
        }
    }
}