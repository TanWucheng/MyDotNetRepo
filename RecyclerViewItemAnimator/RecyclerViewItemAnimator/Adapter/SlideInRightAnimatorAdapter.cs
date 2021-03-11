using Android.Animation;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.Adapter
{
    /// <summary>
    /// An implementation of the AnimatorAdapter class which applies a
    /// swing-in-from-the-right-animation to views.
    /// </summary>
    public class SlideInRightAnimatorAdapter : AnimatorAdapter
    {
        private const string TranslationX = "translationX";

        public SlideInRightAnimatorAdapter(RecyclerView.Adapter adapter, RecyclerView recyclerView) : base(adapter, recyclerView)
        {
        }

        /// <summary>
        /// Returns the Animators to apply to the views.
        /// </summary>
        /// <param name="view">The view that will be animated, as retrieved by OnBindViewHolder().</param>
        /// <returns></returns>
        protected override Animator[] GetAnimators(View view)
        {
            return new Animator[]
            {
                ObjectAnimator.OfFloat(view, TranslationX, RecyclerView.GetLayoutManager().Width, 0)
            };
        }
    }
}