using Android.Animation;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.Adapter
{
    /// <summary>
    /// An implementation of the AnimationAdapter class which applies a
    /// swing-in-from-bottom-animation to views.
    /// </summary>
    public class SwingBottomInAnimationAdapter : AnimatorAdapter
    {
        private const string TranslationY = "translationY";

        public SwingBottomInAnimationAdapter(RecyclerView.Adapter adapter, RecyclerView recyclerView) : base(adapter, recyclerView)
        {
        }

        /// <summary>
        /// Returns the Animators to apply to the views.
        /// </summary>
        /// <param name="view">The view that will be animated, as retrieved by OnBindViewHolder().</param>
        /// <returns></returns>
        protected override Animator[] GetAnimators(View view)
        {
            float mOriginalY = RecyclerView.GetLayoutManager().GetDecoratedTop(view);
            var mDeltaY = RecyclerView.Height - mOriginalY;
            return new Animator[]
            {
                ObjectAnimator.OfFloat(view, TranslationY, mDeltaY, 0)
            };
        }
    }
}