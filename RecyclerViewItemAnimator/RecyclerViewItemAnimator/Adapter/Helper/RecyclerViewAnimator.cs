using System;
using System.Diagnostics.CodeAnalysis;
using Android.Animation;
using Android.OS;
using Android.Util;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace RecyclerViewItemAnimator.Adapter.Helper
{
    /// <summary>
    /// A class which decides whether given Views should be animated based on their position: each View should only be animated once.
    /// It also calculates proper animation delays for the views.
    /// </summary>
    public class RecyclerViewAnimator
    {
        /// <summary>
        /// The default delay in millis before the first animation starts.
        /// </summary>
        private const int InitialDelayMillis = 150;

        /// <summary>
        /// The default delay in millis between view animations.
        /// </summary>
        private const int DefaultAnimationDelayMillis = 100;

        /// <summary>
        /// The default duration in millis of the animations.
        /// </summary>
        private const int DefaultAnimationDurationMillis = 300;

        /* Fields */

        /// <summary>
        /// The ListViewWrapper containing the ListView implementation.
        /// </summary>
        private readonly RecyclerView _recyclerView;

        /// <summary>
        /// The active Animators. Keys are hashCodes of the Views that are animated.
        /// </summary>
        private readonly SparseArray<Animator> _animators = new SparseArray<Animator>();

        /// <summary>
        /// The delay in millis before the first animation starts.
        /// </summary>
        private int _initialDelayMillis = InitialDelayMillis;

        /// <summary>
        /// The delay in millis between view animations.
        /// </summary>
        private int _animationDelayMillis = DefaultAnimationDelayMillis;

        /// <summary>
        /// The duration in millis of the animations.
        /// </summary>
        private int _animationDurationMillis = DefaultAnimationDurationMillis;

        /// <summary>
        /// The start timestamp of the first animation, as returned by {@link SystemClock#uptimeMillis()}.
        /// </summary>
        private long _animationStartMillis;

        /// <summary>
        /// The position of the item that is the first that was animated.
        /// </summary>
        private int _firstAnimatedPosition;

        /// <summary>
        /// The position of the last item that was animated.
        /// </summary>
        private int _lastAnimatedPosition;

        /// <summary>
        /// Whether animation is enabled. When this is set to false, no animation is applied to the views.
        /// </summary>
        private bool _shouldAnimate = true;

        /// <summary>
        /// Creates a new ViewAnimator, using the given RecyclerView.
        /// </summary>
        /// <param name="recyclerView">the ListViewWrapper which wraps the implementation of the ListView used.</param>
        public RecyclerViewAnimator(RecyclerView recyclerView)
        {
            _recyclerView = recyclerView;
            _animationStartMillis = -1;
            _firstAnimatedPosition = -1;
            _lastAnimatedPosition = -1;

        }

        /// <summary>
        /// Call this method to reset animation status on all views.
        /// </summary>
        public void Reset()
        {
            for (var i = 0; i < _animators.Size(); i++)
            {
                _animators.Get(_animators.KeyAt(i)).Cancel();
            }
            _animators.Clear();
            _firstAnimatedPosition = -1;
            _lastAnimatedPosition = -1;
            _animationStartMillis = -1;
            _shouldAnimate = true;
        }

        /// <summary>
        /// Set the starting position for which items should animate. Given position will animate as well.
        /// </summary>
        /// <param name="position"></param>
        public void SetShouldAnimateFromPosition(int position)
        {
            EnableAnimations();
            _firstAnimatedPosition = position - 1;
            _lastAnimatedPosition = position - 1;
        }

        /// <summary>
        /// Set the starting position for which items should animate as the first position which isn't currently visible on screen.
        /// This call is also valid when the Views
        /// </summary>
        public void SetShouldAnimateNotVisible()
        {
            EnableAnimations();
            _firstAnimatedPosition = ((LinearLayoutManager)_recyclerView.GetLayoutManager()).FindLastVisibleItemPosition();
            _lastAnimatedPosition = ((LinearLayoutManager)_recyclerView.GetLayoutManager()).FindLastVisibleItemPosition();
        }

        /// <summary>
        /// Sets the value of the last animated position. Views with positions smaller than or equal to given value will not be animated.
        /// </summary>
        /// <param name="lastAnimatedPosition"></param>
        public void SetLastAnimatedPosition(int lastAnimatedPosition)
        {
            _lastAnimatedPosition = lastAnimatedPosition;
        }

        /// <summary>
        /// Sets the delay in milliseconds before the first animation should start. Defaults to INITIAL_DELAY_MILLIS.
        /// </summary>
        /// <param name="delayMillis">he time in milliseconds.</param>
        public void SetInitialDelayMillis(int delayMillis)
        {
            _initialDelayMillis = delayMillis;
        }

        /// <summary>
        /// Sets the delay in milliseconds before an animation of a view should start. Defaults to DEFAULT_ANIMATION_DELAY_MILLIS.
        /// </summary>
        /// <param name="delayMillis">he time in milliseconds.</param>
        public void SetAnimationDelayMillis(int delayMillis)
        {
            _animationDelayMillis = delayMillis;
        }

        /// <summary>
        /// Sets the duration of the animation in milliseconds. Defaults to DEFAULT_ANIMATION_DURATION_MILLIS.
        /// </summary>
        /// <param name="durationMillis">he time in milliseconds.</param>
        public void SetAnimationDurationMillis(int durationMillis)
        {
            _animationDurationMillis = durationMillis;
        }

        /// <summary>
        /// Enables animating the Views. This is the default.
        /// </summary>
        public void EnableAnimations()
        {
            _shouldAnimate = true;
        }

        /// <summary>
        /// Disables animating the Views. Enable them again using EnableAnimations().
        /// </summary>
        public void DisableAnimations()
        {
            _shouldAnimate = false;
        }

        /// <summary>
        /// Cancels any existing animations for given View.
        /// </summary>
        /// <param name="view"></param>
        public void CancelExistingAnimation([NotNull] View view)
        {
            var hashCode = view.GetHashCode();
            var animator = _animators.Get(hashCode);
            if (animator == null) return;
            animator.End();
            _animators.Remove(hashCode);
        }

        /// <summary>
        /// Animates given View if necessary.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="view"></param>
        /// <param name="animators"></param>
        public void AnimateViewIfNecessary(int position, [NotNull] View view, [NotNull] Animator[] animators)
        {
            if (!_shouldAnimate || position <= _lastAnimatedPosition) return;
            if (_firstAnimatedPosition == -1)
            {
                _firstAnimatedPosition = position;
            }

            AnimateView(position, view, animators);
            _lastAnimatedPosition = position;
        }

        /// <summary>
        /// Animates given View.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="view">the View that should be animated.</param>
        /// <param name="animators"></param>
        private void AnimateView(int position, [NotNull] View view, [NotNull] Animator[] animators)
        {
            if (_animationStartMillis == -1)
            {
                _animationStartMillis = SystemClock.UptimeMillis();
            }
            
            view.Alpha = 0;

            var set = new AnimatorSet();
            set.PlayTogether(animators);
            set.StartDelay = CalculateAnimationDelay(position);
            set.SetDuration(_animationDurationMillis);
            set.Start();

            _animators.Put(view.GetHashCode(), set);
        }

        /// <summary>
        /// Returns the delay in milliseconds after which animation for View with position mLastAnimatedPosition + 1 should start.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private int CalculateAnimationDelay(int position)
        {
            int delay;

            var lastVisiblePosition = ((LinearLayoutManager)_recyclerView.GetLayoutManager()).FindLastCompletelyVisibleItemPosition();
            var firstVisiblePosition = ((LinearLayoutManager)_recyclerView.GetLayoutManager()).FindFirstCompletelyVisibleItemPosition();

            if (_lastAnimatedPosition > lastVisiblePosition) lastVisiblePosition = _lastAnimatedPosition;

            var numberOfItemsOnScreen = lastVisiblePosition - firstVisiblePosition;
            var numberOfAnimatedItems = position - 1 - _firstAnimatedPosition;

            if (numberOfItemsOnScreen + 1 < numberOfAnimatedItems)
            {
                delay = _animationDelayMillis;

                if (_recyclerView.GetLayoutManager() is GridLayoutManager manager)
                {
                    var numColumns = manager.SpanCount;
                    delay += _animationDelayMillis * (position % numColumns);
                    Log.Debug("GAB", "Delay[" + position + "]=*" + lastVisiblePosition + "|" + firstVisiblePosition + "|");
                }
            }
            else
            {
                var delaySinceStart = (position - _firstAnimatedPosition) * _animationDelayMillis;
                delay = Math.Max(0, (int)(-SystemClock.UptimeMillis() + _animationStartMillis + _initialDelayMillis + delaySinceStart));
            }
            Log.Debug("GAB", "Delay[" + position + "]=" + delay + "|" + lastVisiblePosition + "|" + firstVisiblePosition + "|");
            return delay;
        }

        /// <summary>
        /// Returns a Parcelable object containing the AnimationAdapter's current dynamic state.
        /// </summary>
        /// <returns></returns>
        public IParcelable OnSaveInstanceState()
        {
            var parcelable = new AnimateStateParcelable
            {
                FirstAnimatedPosition = _firstAnimatedPosition,
                LastAnimatedPosition = _lastAnimatedPosition,
                ShouldAnimate = _shouldAnimate
            };

            return parcelable;
        }

        /// <summary>
        /// Restores this AnimationAdapter's state.
        /// </summary>
        /// <param name="parcelable">the Parcelable object previously returned by OnSaveInstanceState().</param>
        public void OnRestoreInstanceState([AllowNull] IParcelable parcelable)
        {
            if (!(parcelable is AnimateStateParcelable animateState)) return;
            _firstAnimatedPosition = animateState.FirstAnimatedPosition;
            _lastAnimatedPosition = animateState.LastAnimatedPosition;
            _shouldAnimate = animateState.ShouldAnimate;
        }

        public class AnimateStateParcelable : Java.Lang.Object, IParcelable
        {
            public int FirstAnimatedPosition { get; set; }

            public int LastAnimatedPosition { get; set; }

            public bool ShouldAnimate { get; set; }



            public int DescribeContents()
            {
                return 0;
            }

            public void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
            {
                dest.WriteInt(FirstAnimatedPosition);
                dest.WriteInt(LastAnimatedPosition);
                dest.WriteBooleanArray(new[] { ShouldAnimate });
            }
        }
    }
}