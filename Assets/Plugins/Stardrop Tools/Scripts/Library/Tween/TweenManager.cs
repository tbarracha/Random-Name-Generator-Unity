using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenManager : Singleton<TweenManager>
    {
        [SerializeField] int tweenCount;
        List<Tween> tweens;
        bool isUpdating;

        protected override void Awake()
        {
            base.Awake();

            tweens = new List<Tween>();
        }

        public bool ProcessTween(Tween tween)
        {
            if (Application.isPlaying == false)
            {
                Debug.Log("Can't tween on Edit Mode!");
                return false;
            }

            if (tween == null)
            {
                Debug.Log("Tween is null!");
                return false;
            }

            if (tween.Duration <= 0)
            {
                Debug.Log("Tween has zero duration!");
                return false;
            }

            FilterTween(tween.TweenID, tween.TweenType);
            AddTween(tween);

            return true;
        }

        /// <summary>
        /// Check if there is already a tween of this type
        /// </summary>
        void FilterTween(int tweenID, TweenType tweenType)
        {
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                if (tweens[i].TweenID == tweenID && tweens[i].TweenType == tweenType)
                    tweens[i].Stop();
            }
        }

        void AddTween(Tween tween)
        {
            if (tweens.Contains(tween) == false)
            {
                tweens.Add(tween);

                if (isUpdating == false)
                {
                    LoopManager.OnUpdate.AddListener(UpdateTweens);
                    isUpdating = true;
                }
            }
        }


        /// <summary>
        /// Remove input tween from update list
        /// </summary>
        public void RemoveTween(Tween tween)
        {
            if (tweens.Contains(tween))
                tweens.Remove(tween);
        }

        void UpdateTweens()
        {
            if (tweens.Exists())
                for (int i = 0; i < tweens.Count; i++)
                {
                    if (tweens.Exists() == false)
                        break;

                    tweens[i].UpdateTweenState();
                }

            tweenCount = tweens.Count;

            if (tweenCount == 0)
            {
                LoopManager.OnUpdate.RemoveListener(UpdateTweens);
                isUpdating = false;
            }
        }


        /// <summary>
        /// Search inside Tween List for the tween that meets input criteria and Stop()'s it
        /// </summary>
        public void StopTween(int tweenID, TweenType tweenType)
        {
            foreach (Tween tween in tweens)
            {
                if (tween.TweenID == tweenID && tween.TweenType == tweenType)
                    tween.Stop();
            }
        }
    }
}