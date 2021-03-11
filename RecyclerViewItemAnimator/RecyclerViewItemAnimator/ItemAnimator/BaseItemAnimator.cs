using System.Collections.Generic;
using Android.Animation;
using Android.Views;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using Java.Lang;
using RecyclerViewItemAnimator.Utils;
using Math = System.Math;

namespace RecyclerViewItemAnimator.ItemAnimator
{
    public abstract class BaseItemAnimator : SimpleItemAnimator
    {
        private readonly List<List<RecyclerView.ViewHolder>> _additionsList = new List<List<RecyclerView.ViewHolder>>();
        private readonly List<List<ChangeInfo>> _changesList = new List<List<ChangeInfo>>();
        private readonly List<List<MoveInfo>> _movesList = new List<List<MoveInfo>>();
        private readonly List<RecyclerView.ViewHolder> _pendingAdditions = new List<RecyclerView.ViewHolder>();
        private readonly List<ChangeInfo> _pendingChanges = new List<ChangeInfo>();
        private readonly List<MoveInfo> _pendingMoves = new List<MoveInfo>();
        private readonly List<RecyclerView.ViewHolder> _pendingRemovals = new List<RecyclerView.ViewHolder>();

        protected readonly List<RecyclerView.ViewHolder> AddAnimations = new List<RecyclerView.ViewHolder>();
        protected readonly List<RecyclerView.ViewHolder> ChangeAnimations = new List<RecyclerView.ViewHolder>();
        protected readonly List<RecyclerView.ViewHolder> MoveAnimations = new List<RecyclerView.ViewHolder>();

        /// <summary>
        ///     RecyclerView
        /// </summary>
        protected readonly RecyclerView RecyclerView;

        protected readonly List<RecyclerView.ViewHolder> RemoveAnimations = new List<RecyclerView.ViewHolder>();

        protected BaseItemAnimator(RecyclerView recyclerView)
        {
            RecyclerView = recyclerView;
        }

        public override bool IsRunning => !_pendingAdditions.IsEmpty() ||
                                          !_pendingChanges.IsEmpty() ||
                                          !_pendingMoves.IsEmpty() ||
                                          !_pendingRemovals.IsEmpty() ||
                                          !MoveAnimations.IsEmpty() ||
                                          !RemoveAnimations.IsEmpty() ||
                                          !AddAnimations.IsEmpty() ||
                                          !ChangeAnimations.IsEmpty() ||
                                          !_movesList.IsEmpty() ||
                                          !_additionsList.IsEmpty() ||
                                          !_changesList.IsEmpty();

        public override void RunPendingAnimations()
        {
            var removalsPending = !_pendingRemovals.IsEmpty();
            var movesPending = !_pendingMoves.IsEmpty();
            var changesPending = !_pendingChanges.IsEmpty();
            var additionsPending = !_pendingAdditions.IsEmpty();
            if (!removalsPending && !movesPending && !additionsPending && !changesPending)
                // nothing to animate
                return;
            // First, remove stuff
            foreach (var holder in _pendingRemovals) AnimateRemoveImpl(holder);
            _pendingRemovals.Clear();
            // Next, move stuff
            if (movesPending)
            {
                var moves = new List<MoveInfo>();
                moves.AddRange(_pendingMoves);
                _movesList.Add(moves);
                _pendingMoves.Clear();

                var mover = new Runnable(() =>
                {
                    foreach (var moveInfo in moves)
                        AnimateMoveImpl(moveInfo.Holder, moveInfo.FromX, moveInfo.FromY,
                            moveInfo.ToX, moveInfo.ToY);
                    moves.Clear();
                    _movesList.Remove(moves);
                });

                if (removalsPending)
                {
                    var view = moves[0].Holder.ItemView;
                    ViewCompat.PostOnAnimationDelayed(view, mover, RemoveDuration);
                }
                else
                {
                    mover.Run();
                }
            }

            // Next, change stuff, to run in parallel with move animations
            if (changesPending)
            {
                var changes = new List<ChangeInfo>();
                changes.AddRange(_pendingChanges);
                _changesList.Add(changes);
                _pendingChanges.Clear();

                var changer = new Runnable(() =>
                {
                    foreach (var change in changes) AnimateChangeImpl(change);
                });

                if (removalsPending)
                {
                    var holder = changes[0].OldHolder;
                    ViewCompat.PostOnAnimationDelayed(holder.ItemView, changer, RemoveDuration);
                }
                else
                {
                    changer.Run();
                }
            }

            // Next, add stuff
            if (!additionsPending) return;
            {
                var additions = new List<RecyclerView.ViewHolder>();
                additions.AddRange(_pendingAdditions);
                _additionsList.Add(additions);
                _pendingAdditions.Clear();

                var adder = new Runnable(() =>
                {
                    foreach (var viewHolder in additions) AnimateAddImpl(viewHolder);

                    additions.Clear();
                    _additionsList.Remove(additions);
                });

                if (removalsPending || movesPending || changesPending)
                {
                    var removeDuration = removalsPending ? RemoveDuration : 0;
                    var moveDuration = movesPending ? MoveDuration : 0;
                    var changeDuration = changesPending ? ChangeDuration : 0;
                    var totalDelay = removeDuration + Math.Max(moveDuration, changeDuration);
                    var view = additions[0].ItemView;
                    ViewCompat.PostOnAnimationDelayed(view, adder, totalDelay);
                }
                else
                {
                    adder.Run();
                }
            }
        }

        public override bool AnimateAdd(RecyclerView.ViewHolder holder)
        {
            ResetAnimation(holder);
            PrepareAnimateAdd(holder);
            holder.ItemView.Alpha = 0;
            _pendingAdditions.Add(holder);
            return true;
        }

        public override bool AnimateRemove(RecyclerView.ViewHolder holder)
        {
            ResetAnimation(holder);
            _pendingRemovals.Add(holder);
            return true;
        }

        public override bool AnimateMove(RecyclerView.ViewHolder holder, int fromX, int fromY, int toX, int toY)
        {
            var view = holder.ItemView;
            fromX += (int)holder.ItemView.TranslationX;
            fromY += (int)holder.ItemView.TranslationY;
            ResetAnimation(holder);
            var deltaX = toX - fromX;
            var deltaY = toY - fromY;
            if (deltaX == 0 && deltaY == 0)
            {
                DispatchMoveFinished(holder);
                return false;
            }

            if (deltaX != 0) view.TranslationX = -deltaX;
            if (deltaY != 0) view.TranslationY = -deltaY;
            _pendingMoves.Add(new MoveInfo(holder, fromX, fromY, toX, toY));
            return true;
        }

        public override bool AnimateChange(RecyclerView.ViewHolder oldHolder, RecyclerView.ViewHolder newHolder,
            int fromLeft, int fromTop, int toLeft, int toTop)
        {
            if (oldHolder == newHolder)
                // Don't know how to run change animations when the same view holder is re-used.
                // run a move animation to handle position changes.
                return AnimateMove(oldHolder, fromLeft, fromTop, toLeft, toTop);

            var prevTranslationX = oldHolder.ItemView.TranslationX;
            var prevTranslationY = oldHolder.ItemView.TranslationY;
            var prevAlpha = oldHolder.ItemView.Alpha;
            ResetAnimation(oldHolder);
            var deltaX = (int)(toLeft - fromLeft - prevTranslationX);
            var deltaY = (int)(toTop - fromTop - prevTranslationY);
            // recover prev translation state after ending animation
            oldHolder.ItemView.TranslationX = prevTranslationX;
            oldHolder.ItemView.TranslationY = prevTranslationY;
            oldHolder.ItemView.Alpha = prevAlpha;
            if (newHolder != null)
            {
                // carry over translation values
                ResetAnimation(newHolder);
                newHolder.ItemView.TranslationX = -deltaX;
                newHolder.ItemView.TranslationY = -deltaY;
                newHolder.ItemView.Alpha = 0;
            }

            _pendingChanges.Add(new ChangeInfo(oldHolder, newHolder, fromLeft, fromTop, toLeft, toTop));
            return true;
        }

        private void ResetAnimation(RecyclerView.ViewHolder holder)
        {
            // AnimatorCompatHelper.ClearInterpolator(holder.ItemView);
            var mDefaultInterpolator = new ValueAnimator().Interpolator;
            holder.ItemView.Animate()?.SetInterpolator(mDefaultInterpolator);
            EndAnimation(holder);
        }

        private bool EndChangeAnimationIfNecessary(ChangeInfo changeInfo, RecyclerView.ViewHolder item)
        {
            var oldItem = false;
            if (changeInfo.NewHolder == item)
            {
                changeInfo.NewHolder = null;
            }
            else if (changeInfo.OldHolder == item)
            {
                changeInfo.OldHolder = null;
                oldItem = true;
            }
            else
            {
                return false;
            }

            //ViewCompat.SetAlpha(item.ItemView, 1);
            //ViewCompat.SetTranslationX(item.ItemView, 0);
            //ViewCompat.SetTranslationY(item.ItemView, 0);
            item.ItemView.Alpha = 1;
            item.ItemView.TranslationX = 0;
            item.ItemView.TranslationY = 0;
            DispatchChangeFinished(item, oldItem);
            return true;
        }

        private void EndChangeAnimationIfNecessary(ChangeInfo changeInfo)
        {
            if (changeInfo.OldHolder != null) EndChangeAnimationIfNecessary(changeInfo, changeInfo.OldHolder);
            if (changeInfo.NewHolder != null) EndChangeAnimationIfNecessary(changeInfo, changeInfo.OldHolder);
        }

        private void EndChangeAnimation(IList<ChangeInfo> infoList, RecyclerView.ViewHolder item)
        {
            for (var i = infoList.Count - 1; i >= 0; i--)
            {
                var changeInfo = infoList[i];
                if (!EndChangeAnimationIfNecessary(changeInfo, item)) continue;
                if (changeInfo.OldHolder == null && changeInfo.NewHolder == null)
                    infoList.Remove(changeInfo);
            }
        }

        public override void EndAnimation(RecyclerView.ViewHolder item)
        {
            var view = item.ItemView;
            // this will trigger end callback which should set properties to their target values.
            ViewCompat.Animate(view).Cancel();
            // if some other animations are chained to end, how do we cancel them as well?
            for (var i = _pendingMoves.Count - 1; i >= 0; i--)
            {
                var moveInfo = _pendingMoves[i];
                if (moveInfo.Holder != item) continue;
                //ViewCompat.SetTranslationY(view, 0);
                //ViewCompat.SetTranslationX(view, 0);
                view.TranslationY = 0;
                view.TranslationX = 0;
                DispatchMoveFinished(item);
                _pendingMoves.RemoveAt(i);
            }

            EndChangeAnimation(_pendingChanges, item);
            if (_pendingRemovals.Remove(item))
            {
                //ViewCompat.SetAlpha(view, 1);
                view.Alpha = 1;
                DispatchRemoveFinished(item);
            }

            if (_pendingAdditions.Remove(item))
            {
                //ViewCompat.SetAlpha(view, 1);
                view.Alpha = 1;
                DispatchAddFinished(item);
            }

            for (var i = _changesList.Count - 1; i >= 0; i--)
            {
                var changes = _changesList[i];
                EndChangeAnimation(changes, item);
                if (changes.IsEmpty()) _changesList.RemoveAt(i);
            }

            for (var i = _movesList.Count - 1; i >= 0; i--)
            {
                var moves = _movesList[i];
                for (var j = moves.Count - 1; j >= 0; j--)
                {
                    var moveInfo = moves[j];
                    if (moveInfo.Holder != item) continue;
                    //ViewCompat.SetTranslationY(view, 0);
                    //ViewCompat.SetTranslationX(view, 0);
                    view.TranslationY = 0;
                    view.TranslationX = 0;
                    DispatchMoveFinished(item);
                    moves.RemoveAt(j);
                    if (moves.IsEmpty()) _movesList.RemoveAt(i);
                    break;
                }
            }

            for (var i = _additionsList.Count - 1; i >= 0; i--)
            {
                var additions = _additionsList[i];
                if (!additions.Remove(item)) continue;
                //ViewCompat.SetAlpha(view, 1);
                view.Alpha = 1;
                DispatchAddFinished(item);
                if (additions.IsEmpty()) _additionsList.RemoveAt(i);
            }

            // animations should be ended by the cancel above.
            DispatchFinishedWhenDone();
        }

        public override void EndAnimations()
        {
            var count = _pendingMoves.Count;
            for (var i = count - 1; i >= 0; i--)
            {
                var item = _pendingMoves[i];
                var view = item.Holder.ItemView;
                view.TranslationY = 0;
                view.TranslationX = 0;
                DispatchMoveFinished(item.Holder);
                _pendingMoves.RemoveAt(i);
            }

            count = _pendingRemovals.Count;
            for (var i = count - 1; i >= 0; i--)
            {
                var item = _pendingRemovals[i];
                DispatchRemoveFinished(item);
                _pendingRemovals.RemoveAt(i);
            }

            count = _pendingAdditions.Count;
            for (var i = count - 1; i >= 0; i--)
            {
                var item = _pendingAdditions[i];
                var view = item.ItemView;
                view.Alpha = 1;
                DispatchAddFinished(item);
                _pendingAdditions.RemoveAt(i);
            }

            count = _pendingChanges.Count;
            for (var i = count - 1; i >= 0; i--) EndChangeAnimationIfNecessary(_pendingChanges[i]);
            _pendingChanges.Clear();
            if (!IsRunning) return;

            var listCount = _movesList.Count;
            for (var i = listCount - 1; i >= 0; i--)
            {
                var moves = _movesList[i];
                count = moves.Count;
                for (var j = count - 1; j >= 0; j--)
                {
                    var moveInfo = moves[j];
                    var item = moveInfo.Holder;
                    var view = item.ItemView;
                    view.TranslationY = 0;
                    view.TranslationX = 0;
                    DispatchMoveFinished(moveInfo.Holder);
                    moves.RemoveAt(j);
                    if (moves.IsEmpty()) _movesList.Remove(moves);
                }
            }

            listCount = _additionsList.Count;
            for (var i = listCount - 1; i >= 0; i--)
            {
                var additions = _additionsList[i];
                count = additions.Count;
                for (var j = count - 1; j >= 0; j--)
                {
                    var item = additions[j];
                    var view = item.ItemView;
                    view.Alpha = 1;
                    DispatchAddFinished(item);
                    additions.RemoveAt(j);
                    if (additions.IsEmpty()) _additionsList.Remove(additions);
                }
            }

            listCount = _changesList.Count;
            for (var i = listCount - 1; i >= 0; i--)
            {
                var changes = _changesList[i];
                count = changes.Count;
                for (var j = count - 1; j >= 0; j--)
                {
                    EndChangeAnimationIfNecessary(changes[j]);
                    if (changes.IsEmpty()) _changesList.Remove(changes);
                }
            }

            CancelAll(RemoveAnimations);
            CancelAll(MoveAnimations);
            CancelAll(AddAnimations);
            CancelAll(ChangeAnimations);

            DispatchAnimationsFinished();
        }

        private void CancelAll(IReadOnlyList<RecyclerView.ViewHolder> viewHolders)
        {
            for (var i = viewHolders.Count - 1; i >= 0; i--) ViewCompat.Animate(viewHolders[i].ItemView).Cancel();
        }

        private void AnimateMoveImpl(RecyclerView.ViewHolder holder, int fromX, int fromY, int toX, int toY)
        {
            var view = holder.ItemView;
            var deltaX = toX - fromX;
            var deltaY = toY - fromY;
            if (deltaX != 0) ViewCompat.Animate(view).TranslationX(0);
            if (deltaY != 0) ViewCompat.Animate(view).TranslationY(0);
            // make EndActions end listeners instead, since end actions aren't called when
            // vpas are canceled (and can't end them. why?)
            // need listener functionality in VPACompat for this. Ick.
            var animation = ViewCompat.Animate(view);
            MoveAnimations.Add(holder);
            animation.SetDuration(MoveDuration)
                .SetListener(new AnimateMoveVpaListener(this, holder, deltaX, deltaY, animation, MoveAnimations))
                .Start();
        }

        private void AnimateChangeImpl(ChangeInfo changeInfo)
        {
            var holder = changeInfo.OldHolder;
            var view = holder?.ItemView;
            var newHolder = changeInfo.NewHolder;
            var newView = newHolder?.ItemView;
            if (view != null)
            {
                var oldViewAnim = ViewCompat.Animate(view).SetDuration(ChangeDuration);
                ChangeAnimations.Add(changeInfo.OldHolder);
                oldViewAnim.TranslationX(changeInfo.ToX - changeInfo.FromX);
                oldViewAnim.TranslationY(changeInfo.ToY - changeInfo.FromY);
                oldViewAnim.Alpha(0)
                    .SetListener(new AnimateChangeVpaListener(this, changeInfo, oldViewAnim, ChangeAnimations, false))
                    .Start();
            }

            if (newView == null) return;
            var newViewAnimation = ViewCompat.Animate(newView);
            ChangeAnimations.Add(changeInfo.NewHolder);
            newViewAnimation.TranslationX(0).TranslationY(0).SetDuration(ChangeDuration).Alpha(1)
                .SetListener(new AnimateChangeVpaListener(this, changeInfo, newViewAnimation, ChangeAnimations, true))
                .Start();
        }

        /// <summary>
        ///     Check the state of currently pending and running animations. If there are none
        ///     pending/running, call {@link #dispatchAnimationsFinished()} to notify any listeners
        /// </summary>
        protected void DispatchFinishedWhenDone()
        {
            if (!IsRunning) DispatchAnimationsFinished();
        }

        protected abstract void AnimateRemoveImpl(RecyclerView.ViewHolder holder);

        protected abstract void AnimateAddImpl(RecyclerView.ViewHolder holder);

        protected abstract void PrepareAnimateAdd(RecyclerView.ViewHolder holder);

        private class MoveInfo
        {
            public readonly int FromX;
            public readonly int FromY;
            public readonly RecyclerView.ViewHolder Holder;
            public readonly int ToX;
            public readonly int ToY;

            public MoveInfo(RecyclerView.ViewHolder holder, int fromX, int fromY, int toX, int toY)
            {
                Holder = holder;
                FromX = fromX;
                FromY = fromY;
                ToX = toX;
                ToY = toY;
            }
        }

        private class ChangeInfo
        {
            public readonly int FromX;
            public readonly int FromY;
            public readonly int ToX;
            public readonly int ToY;
            public RecyclerView.ViewHolder NewHolder;
            public RecyclerView.ViewHolder OldHolder;

            private ChangeInfo(RecyclerView.ViewHolder oldHolder, RecyclerView.ViewHolder newHolder)
            {
                OldHolder = oldHolder;
                NewHolder = newHolder;
            }

            public ChangeInfo(RecyclerView.ViewHolder oldHolder, RecyclerView.ViewHolder newHolder,
                int fromX, int fromY, int toX, int toY) : this(oldHolder, newHolder)
            {
                FromX = fromX;
                FromY = fromY;
                ToX = toX;
                ToY = toY;
            }

            public override string ToString()
            {
                return "ChangeInfo{" +
                       "oldHolder=" + OldHolder +
                       ", newHolder=" + NewHolder +
                       ", fromX=" + FromX +
                       ", fromY=" + FromY +
                       ", toX=" + ToX +
                       ", toY=" + ToY +
                       '}';
            }
        }

        private class AnimateChangeVpaListener : Object, IViewPropertyAnimatorListener
        {
            private readonly ViewPropertyAnimatorCompat _animatorCompat;
            private readonly BaseItemAnimator _baseItemAnimator;
            private readonly ChangeInfo _changeInfo;
            private readonly bool _isNewView;
            private readonly List<RecyclerView.ViewHolder> _mChangeAnimations;

            public AnimateChangeVpaListener(BaseItemAnimator baseItemAnimator
                , ChangeInfo changeInfo
                , ViewPropertyAnimatorCompat animatorCompat
                , List<RecyclerView.ViewHolder> mChangeAnimations
                , bool isNewView)
            {
                _baseItemAnimator = baseItemAnimator;
                _changeInfo = changeInfo;
                _animatorCompat = animatorCompat;
                _mChangeAnimations = mChangeAnimations;
                _isNewView = isNewView;
            }

            public void OnAnimationCancel(View view)
            {
            }

            public void OnAnimationEnd(View view)
            {
                _animatorCompat.SetListener(null);
                //ViewCompat.SetAlpha(view, 1);
                //ViewCompat.SetTranslationX(view, 0);
                //ViewCompat.SetTranslationY(view, 0);
                view.Alpha = 1;
                view.TranslationX = 0;
                view.TranslationY = 0;
                if (_isNewView)
                {
                    _baseItemAnimator.DispatchChangeFinished(_changeInfo.NewHolder, false);
                    _mChangeAnimations.Remove(_changeInfo.NewHolder);
                }
                else
                {
                    _baseItemAnimator.DispatchChangeFinished(_changeInfo.OldHolder, true);
                    _mChangeAnimations.Remove(_changeInfo.OldHolder);
                }

                _baseItemAnimator.DispatchFinishedWhenDone();
            }

            public void OnAnimationStart(View view)
            {
                if (_isNewView)
                    _baseItemAnimator.DispatchChangeStarting(_changeInfo.NewHolder, false);
                else
                    _baseItemAnimator.DispatchChangeStarting(_changeInfo.OldHolder, true);
            }
        }

        private class AnimateMoveVpaListener : Object, IViewPropertyAnimatorListener
        {
            private readonly ViewPropertyAnimatorCompat _animation;
            private readonly BaseItemAnimator _baseItemAnimator;
            private readonly int _deltaX;
            private readonly int _deltaY;
            private readonly RecyclerView.ViewHolder _holder;
            private readonly List<RecyclerView.ViewHolder> _mMoveAnimations;

            public AnimateMoveVpaListener(BaseItemAnimator baseItemAnimator
                , RecyclerView.ViewHolder holder
                , int deltaX
                , int deltaY
                , ViewPropertyAnimatorCompat animation
                , List<RecyclerView.ViewHolder> mMoveAnimations)
            {
                _baseItemAnimator = baseItemAnimator;
                _holder = holder;
                _deltaX = deltaX;
                _deltaY = deltaY;
                _animation = animation;
                _mMoveAnimations = mMoveAnimations;
            }

            public void OnAnimationCancel(View view)
            {
                if (_deltaX != 0)
                    //ViewCompat.SetTranslationX(view, 0);
                    view.TranslationX = 0;
                if (_deltaY != 0)
                    //ViewCompat.SetTranslationY(view, 0);
                    view.TranslationY = 0;
            }

            public void OnAnimationEnd(View view)
            {
                _animation.SetListener(null);
                _baseItemAnimator.DispatchMoveFinished(_holder);
                _mMoveAnimations.Remove(_holder);
                _baseItemAnimator.DispatchFinishedWhenDone();
            }

            public void OnAnimationStart(View view)
            {
                _baseItemAnimator.DispatchMoveStarting(_holder);
            }
        }
    }
}