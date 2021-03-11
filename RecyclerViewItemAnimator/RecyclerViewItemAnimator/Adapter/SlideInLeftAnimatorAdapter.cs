using Android.Animation;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.Adapter
{
    /// <summary>
    /// An implementation of the AnimatorAdapter class which applies a
    /// swing-in-from-the-left-animation to views.
    /// </summary>
    public class SlideInLeftAnimatorAdapter : AnimatorAdapter
    {
        private const string TranslationX = "translationX";

        public SlideInLeftAnimatorAdapter(RecyclerView.Adapter adapter, RecyclerView recyclerView) : base(adapter, recyclerView)
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
                ObjectAnimator.OfFloat(view, TranslationX, 0 - RecyclerView.GetLayoutManager().Width, 0)
            };
        }
    }
}