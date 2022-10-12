
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenColorOpacity : TweenFloat
    {
        public Color color;

        protected override void SetEssentials()
        {
            //tweenID = color.GetHashCode();
            tweenType = TweenType.ColorOpacity;
        }

        public TweenColorOpacity(Color color, float start, float end)
        {
            this.color = color;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenColorOpacity(Color color, float end)
        {
            this.color = color;
            start = color.a;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            color.a = lerped;
        }
    }
}