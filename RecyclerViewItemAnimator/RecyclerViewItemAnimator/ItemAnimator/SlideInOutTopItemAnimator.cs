using Android.Views;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.ItemAnimator
{
    public class SlideInOutTopItemAnimator : BaseItemAnimator
    {
        private float _originalY;

        public SlideInOutTopItemAnimator(RecyclerView recyclerView) : base(recyclerView)
        {
        }

        protected override void AnimateRemoveImpl(RecyclerView.ViewHolder holder)
        {
            var view = holder.ItemView;
            RetrieveItemHeight(holder);
            var animation = ViewCompat.Animate(view);
            RemoveAnimations.Add(holder);
            animation.SetDuration(RemoveDuration)
                    .Alpha(0)
                    .TranslationY(-_originalY)
                    .SetListener(new AnimateRemoveVpaListener(this, holder, animation, _originalY))
                    .Start();
        }

        protected override void AnimateAddImpl(RecyclerView.ViewHolder holder)
        {
            var view = holder.ItemView;
            var animation = ViewCompat.Animate(view);
            AddAnimations.Add(holder);
            animation.TranslationY(0)
                    .Alpha(1)
                    .SetDuration(AddDuration)
                    .SetListener(new AnimateAddVpaListener(this, holder, animation))
                    .Start();
        }

        protected override void PrepareAnimateAdd(RecyclerView.ViewHolder holder)
        {
            RetrieveItemHeight(holder);
            holder.ItemView.TranslationY = (-_originalY);
        }

        private void RetrieveItemHeight(RecyclerView.ViewHolder holder)
        {
            _originalY = RecyclerView.GetLayoutManager().GetDecoratedBottom(holder.ItemView);
        }

        private class AnimateRemoveVpaListener : Java.Lang.Object, IViewPropertyAnimatorListener
        {
            private readonly SlideInOutTopItemAnimator _itemAnimator;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly ViewPropertyAnimatorCompat _animation;
            private readonly float _originalY;

            public AnimateRemoveVpaListener(SlideInOutTopItemAnimator itemAnimator
                , RecyclerView.ViewHolder holder
                , ViewPropertyAnimatorCompat animation,
                float originalY)
            {
                _itemAnimator = itemAnimator;
                _holder = holder;
                _animation = animation;
                _originalY = originalY;
            }

            public void OnAnimationCancel(View view)
            {

            }

            public void OnAnimationEnd(View view)
            {
                _animation.SetListener(null);
                view.Alpha = 1;
                view.TranslationY = (-_originalY);
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
            private readonly SlideInOutTopItemAnimator _itemAnimator;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly ViewPropertyAnimatorCompat _animation;

            public AnimateAddVpaListener(SlideInOutTopItemAnimator itemAnimator
                , RecyclerView.ViewHolder holder
                , ViewPropertyAnimatorCompat animation)
            {
                _itemAnimator = itemAnimator;
                _holder = holder;
                _animation = animation;
            }

            public void OnAnimationCancel(View view)
            {
                view.Alpha = 1;
                view.TranslationY = 0;
            }

            public void OnAnimationEnd(View view)
            {
                _animation.SetListener(null);
                view.TranslationY = 0;
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