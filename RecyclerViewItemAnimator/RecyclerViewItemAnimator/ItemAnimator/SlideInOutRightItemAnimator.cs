using Android.Views;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.ItemAnimator
{
    public class SlideInOutRightItemAnimator : BaseItemAnimator
    {
        public SlideInOutRightItemAnimator(RecyclerView recyclerView) : base(recyclerView)
        {
        }

        protected override void AnimateRemoveImpl(RecyclerView.ViewHolder holder)
        {
            var view = holder.ItemView;
            var animation = ViewCompat.Animate(view);
            RemoveAnimations.Add(holder);
            animation.SetDuration(RemoveDuration)
                    .Alpha(0)
                    .TranslationX(+RecyclerView.GetLayoutManager().Width)
                    .SetListener(new AnimateRemoveVpaListener(this, holder, animation, +RecyclerView.GetLayoutManager().Width))
                    .Start();
        }

        protected override void AnimateAddImpl(RecyclerView.ViewHolder holder)
        {
            var view = holder.ItemView;
            var animation = ViewCompat.Animate(view);
            AddAnimations.Add(holder);
            animation.TranslationX(0)
                    .Alpha(1)
                    .SetDuration(AddDuration)
                    .SetListener(new AnimateAddVpaListener(this, holder, animation))
                    .Start();
        }

        protected override void PrepareAnimateAdd(RecyclerView.ViewHolder holder)
        {
            holder.ItemView.TranslationX = (+RecyclerView.GetLayoutManager().Width);
        }

        private class AnimateRemoveVpaListener : Java.Lang.Object, IViewPropertyAnimatorListener
        {
            private readonly SlideInOutRightItemAnimator _itemAnimator;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly ViewPropertyAnimatorCompat _animation;
            private readonly float _width;

            public AnimateRemoveVpaListener(SlideInOutRightItemAnimator itemAnimator
                , RecyclerView.ViewHolder holder
                , ViewPropertyAnimatorCompat animation
                , float width)
            {
                _itemAnimator = itemAnimator;
                _holder = holder;
                _animation = animation;
                _width = width;
            }

            public void OnAnimationCancel(View view)
            {

            }

            public void OnAnimationEnd(View view)
            {
                _animation.SetListener(null);
                view.Alpha = 1;
                view.TranslationX = _width;
                _itemAnimator.DispatchRemoveFinished(_holder);
                _itemAnimator.RemoveAnimations.Remove(_holder);
                _itemAnimator.DispatchFinishedWhenDone();
            }

            public void OnAnimationStart(View view)
            {
                _itemAnimator.DispatchRemoveStarting(_holder);
            }
        }

        private class AnimateAddVpaListener : Java.Lang.Object, IViewPropertyAnimatorListener
        {
            private readonly SlideInOutRightItemAnimator _itemAnimator;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly ViewPropertyAnimatorCompat _animation;

            public AnimateAddVpaListener(SlideInOutRightItemAnimator itemAnimator
                , RecyclerView.ViewHolder holder
                , ViewPropertyAnimatorCompat animation)
            {
                _itemAnimator = itemAnimator;
                _holder = holder;
                _animation = animation;
            }

            public void OnAnimationCancel(View view)
            {
                view.TranslationX = 0;
                view.Alpha = 1;
            }

            public void OnAnimationEnd(View view)
            {
                _animation.SetListener(null);
                view.TranslationX = 0;
                view.Alpha = 1;
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