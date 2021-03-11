__supportLib 26.0.0+以上AnimatorCompatHelper类被移除，替代方案__


```java
protected void resetAnimation(RecyclerView.ViewHolder holder) {
    if (sDefaultInterpolator == null) {
        sDefaultInterpolator = new ValueAnimator().getInterpolator();
    }
    holder.itemView.animate().setInterpolator(sDefaultInterpolator);
    endAnimation(holder);
}
```

```csharp
private void ResetAnimation(RecyclerView.ViewHolder holder)
{
    // AnimatorCompatHelper.ClearInterpolator(holder.ItemView);
    var mDefaultInterpolator = new ValueAnimator().Interpolator;
    holder.ItemView.Animate()?.SetInterpolator(mDefaultInterpolator);
    EndAnimation(holder);
}
```