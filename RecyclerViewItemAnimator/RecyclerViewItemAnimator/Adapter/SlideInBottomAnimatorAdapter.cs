using Android.Animation;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.Adapter
{
    /// <summary>
    /// An implementation of the AnimatorAdapter class which applies a
    /// swing-in-from-bottom-animation to views.
    /// </summary>
    public class SlideInBottomAnimatorAdapter : AnimatorAdapter
    {
        private const string TranslationY = "translationY";

        public SlideInBottomAnimatorAdapter(RecyclerView.Adapter adapter, RecyclerView recyclerView) : base(adapter, recyclerView)
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
                ObjectAnimator.OfFloat(view,TranslationY,RecyclerView.MeasuredHeight>>1,0)
            };
        }
    }
}