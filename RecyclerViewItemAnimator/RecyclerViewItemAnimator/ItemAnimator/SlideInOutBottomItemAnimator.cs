using Android.Views;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.ItemAnimator
{
    public class SlideInOutBottomItemAnimator : BaseItemAnimator
    {
        private float _originalY;
        private float _deltaY;

        public SlideInOutBottomItemAnimator(RecyclerView recyclerView) : base(recyclerView)
        {
        }

        protected override void AnimateRemoveImpl(RecyclerView.ViewHolder holder)
        {
            var view = holder.ItemView;
            var animation = ViewCompat.Animate(view);
            RemoveAnimations.Add(holder);
            animation.SetDuration(RemoveDuration)
                    .Alpha(0)
                    .TranslationY(+_deltaY)
                    .SetListener(new AnimateRemoveVpaListener(this, holder, animation, _deltaY)).Start();
        }

        protected override void AnimateAddImpl(RecyclerView.ViewHolder holder)
        {
            var view = holder.ItemView;
            var animation = ViewCompat.Animate(view);
            AddAnimations.Add(holder);
            animation.TranslationY(0)
                    .Alpha(1)
                    .SetDuration(AddDuration)
                    .SetListener(new AnimateAddVpaListener(this, holder, animation)).Start();
        }

        protected override void PrepareAnimateAdd(RecyclerView.ViewHolder holder)
        {
            RetrieveItemPosition(holder);
            holder.ItemView.TranslationY = (+_deltaY);
        }

        private void RetrieveItemPosition(RecyclerView.ViewHolder holder)
        {
            _originalY = RecyclerView.GetLayoutManager().GetDecoratedTop(holder.ItemView);
            _deltaY = RecyclerView.Height - _originalY;
        }

        private class AnimateRemoveVpaListener : Java.Lang.Object, IViewPropertyAnimatorListener
        {
            private readonly SlideInOutBottomItemAnimator _itemAnimator;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly ViewPropertyAnimatorCompat _animation;
            private readonly float _deltaY;

            public AnimateRemoveVpaListener(SlideInOutBottomItemAnimator itemAnimator
                , RecyclerView.ViewHolder holder
                , ViewPropertyAnimatorCompat animation,
                float deltaY)
            {
                _itemAnimator = itemAnimator;
                _holder = holder;
                _animation = animation;
                _deltaY = deltaY;
            }

            public void OnAnimationCancel(View view)
            {

            }

            public void OnAnimationEnd(View view)
            {
                _animation.SetListener(null);
                view.Alpha = 1;
                view.TranslationY = _deltaY;
                _itemAnimator.DispatchMoveFinished(_holder);
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
            private readonly SlideInOutBottomItemAnimator _itemAnimator;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly ViewPropertyAnimatorCompat _animation;

            public AnimateAddVpaListener(SlideInOutBottomItemAnimator itemAnimator
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
                view.Alpha = 1;
                view.TranslationY = 0;
                _itemAnimator.DispatchAddFinished(_holder);
                _itemAnimator.AddAnimations.Remove(_holder);
                _itemAnimator.DispatchFinishedWhenDone();
            }

            public void OnAnimationStart(View view)
            {

            }
        }
    }
}