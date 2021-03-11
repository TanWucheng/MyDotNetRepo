using System.Diagnostics.CodeAnalysis;
using Android.Animation;

namespace RecyclerViewItemAnimator.Adapter.Helper
{
    public static class AnimatorUtil
    {
        /// <summary>
        /// Merges given Animators into one array.
        /// </summary>
        /// <param name="animators"></param>
        /// <param name="alphaAnimator"></param>
        /// <returns></returns>
        public static Animator[] ConcatAnimators([NotNull] Animator[] animators, [NotNull] Animator alphaAnimator)
        {
            var allAnimators = new Animator[animators.Length + 1];
            var i = 0;

            foreach (var animator in animators)
            {
                allAnimators[i] = animator;
                ++i;
            }
            allAnimators[^1] = alphaAnimator;
            return allAnimators;
        }
    }
}