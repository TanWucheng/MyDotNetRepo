using Android.Views;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using Java.Lang;

namespace RecyclerViewItemAnimator.ItemAnimator
{
    public class ScaleInOutItemAnimator : BaseItemAnimator
    {
        public ScaleInOutItemAnimator(RecyclerView recyclerView) : base(recyclerView)
        {
        }

        protected override void AnimateRemoveImpl(RecyclerView.ViewHolder holder)
        {
            var view = holder.ItemView;
            var animation = ViewCompat.Animate(view);
            RemoveAnimations.Add(holder);
            animation.SetDuration(RemoveDuration)
                .Alpha(0)
                .ScaleX(0)
                .ScaleY(0)
                .SetListener(new AnimateRemoveVpaListener(this, holder, animation)).Start();
        }

        protected override void AnimateAddImpl(RecyclerView.ViewHolder holder)
        {
            var view = holder.ItemView;
            var animation = ViewCompat.Animate(view);
            AddAnimations.Add(holder);
            animation.ScaleX(1)
                .ScaleY(1)
                .Alpha(1)
                .SetDuration(AddDuration)
                .SetListener(new AnimateAddVpaListener(this, holder, animation)).Start();
        }

        protected override void PrepareAnimateAdd(RecyclerView.ViewHolder holder)
        {
            holder.ItemView.ScaleX = 0;
            holder.ItemView.ScaleY = 0;
        }

        private class AnimateRemoveVpaListener : Object, IViewPropertyAnimatorListener
        {
            private readonly ViewPropertyAnimatorCompat _animation;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly ScaleInOutItemAnimator _itemAnimator;

            public AnimateRemoveVpaListener(ScaleInOutItemAnimator itemAnimator
                , RecyclerView.ViewHolder holder
                , ViewPropertyAnimatorCompat animation)
            {
                _itemAnimator = itemAnimator;
                _holder = holder;
                _animation = animation;
            }

            public void OnAnimationCancel(View view)
            {
            }

            public void OnAnimationEnd(View view)
            {
                _animation.SetListener(null);
                view.Alpha = 1;
                view.ScaleX = 0;
                view.ScaleY = 0;
                _itemAnimator.DispatchMoveFinished(_holder);
                _itemAnimator.RemoveAnimations.Remove(_holder);
                _itemAnimator.DispatchFinishedWhenDone();
            }

            public void OnAnimationStart(View view)
            {
                _itemAnimator.DispatchRemoveStarting(_holder);
            }
        }

        private class AnimateAddVpaListener : Object, IViewPropertyAnimatorListener
        {
            private readonly ViewPropertyAnimatorCompat _animation;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly ScaleInOutItemAnimator _itemAnimator;

            public AnimateAddVpaListener(ScaleInOutItemAnimator itemAnimator
                , RecyclerView.ViewHolder holder
                , ViewPropertyAnimatorCompat animation)
            {
                _itemAnimator = itemAnimator;
                _holder = holder;
                _animation = animation;
            }

            public void OnAnimationCancel(View view)
            {
                view.ScaleX = 1;
                view.ScaleY = 1;
                view.Alpha = 1;
            }

            public void OnAnimationEnd(View view)
            {
                _animation.SetListener(null);
                view.Alpha = 1;
                view.ScaleX = 1;
                view.ScaleY = 1;
                _itemAnimator.DispatchAddFinished(_holder);
                _itemAnimator.AddAnimations.Remove(_holder);
                _itemAnimator.DispatchFinishedWhenDone();
            }

            public void OnAnimationStart(View view)
            {
                _itemAnimator.DispatchAddStarting(_holder);
            }
        }
    }
}