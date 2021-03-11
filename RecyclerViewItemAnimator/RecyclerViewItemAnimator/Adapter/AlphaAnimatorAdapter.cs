using Android.Animation;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.Adapter
{
    public class AlphaAnimatorAdapter : AnimatorAdapter
    {
        public AlphaAnimatorAdapter(RecyclerView.Adapter adapter, RecyclerView recyclerView) : base(adapter, recyclerView)
        {
        }

        /// <summary>
        /// Returns the Animators to apply to the views.
        /// </summary>
        /// <param name="view">The view that will be animated, as retrieved by OnBindViewHolder().</param>
        /// <returns></returns>
        protected override Animator[] GetAnimators(View view)
        {
            return new Animator[0];
        }
    }
}