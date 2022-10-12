using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class TimerManager : Singleton<TimerManager>
    {
        [SerializeField] List<Timer> timers;
        [SerializeField] bool isUpdating;

        protected override void Awake()
        {
            base.Awake();

            timers = new List<Timer>();
        }

        public void AddTimer(Timer timer)
        {
            timers.Add(timer);
            CheckIfCanUpdate();
        }

        public void RemoveTimer(Timer timer)
        {
            if (timers.Contains(timer))
            {
                timers.Remove(timer);
                CheckIfCanUpdate();
            }
        }

        void UpdateTimers()
        {
            if (timers.Count > 0)
                for (int i = 0; i < timers.Count; i++)
                    timers[i].Tick();
        }

        void CheckIfCanUpdate()
        {
            if (timers.Count > 0 && isUpdating == false)
            {
                LoopManager.OnUpdate.AddListener(UpdateTimers);
                isUpdating = true;
            }

            if (timers.Count == 0 && isUpdating)
            {
                LoopManager.OnUpdate.RemoveListener(UpdateTimers);
                isUpdating = false;
            }
        }
    }
}