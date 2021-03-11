using Android.Animation;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.Adapter
{
    public class ScaleInAnimatorAdapter : AnimatorAdapter
    {
        private const float DefaultScaleFrom = 0.6f;
        private const string ScaleX = "scaleX";
        private const string ScaleY = "scaleY";
        private readonly float _scaleFrom;

        public ScaleInAnimatorAdapter(RecyclerView.Adapter adapter, RecyclerView recyclerView, float scaleFrom = DefaultScaleFrom) : base(adapter, recyclerView)
        {
            _scaleFrom = scaleFrom;
        }

        /// <summary>
        /// Returns the Animators to apply to the views.
        /// </summary>
        /// <param name="view">The view that will be animated, as retrieved by OnBindViewHolder().</param>
        /// <returns></returns>
        protected override Animator[] GetAnimators(View view)
        {
            var scaleX = ObjectAnimator.OfFloat(view, ScaleX, _scaleFrom, 1f);
            var scaleY = ObjectAnimator.OfFloat(view, ScaleY, _scaleFrom, 1f);
            return new Animator[] { scaleX, scaleY };
        }
    }
}