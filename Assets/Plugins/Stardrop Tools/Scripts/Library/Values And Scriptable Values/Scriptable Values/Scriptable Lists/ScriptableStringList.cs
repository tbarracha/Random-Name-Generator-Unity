﻿
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable List / Scriptable List String")]
    public class ScriptableStringList : ScriptableValue
    {
        [SerializeField] List<string> defaultList;
        [SerializeField] List<string> list;

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
        }

        public override void Default(bool invokeEvents = true)
        {
            if (invokeEvents == false)
                return;

            list = new List<string>();
            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);

            InvokeEvents(invokeEvents);
        }

        public string GetString(int index) => list[index];

        public string GetRandom() => list.GetRandom();

        public List<string> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(string value, bool invokeEvents = true)
        {
            list.Add(value);
            InvokeEvents(invokeEvents);
        }

        public void AddSafe(string value, bool invokeEvents = true)
        {
            if (list.Contains(value) == false)
                list.Add(value);

            InvokeEvents(invokeEvents);
        }

        public void Remove(string value, bool invokeEvents = true)
        {
            list.Remove(value);
            InvokeEvents(invokeEvents);
        }

        public void RemoveSafe(string value, bool invokeEvents = true)
        {
            if (list.Contains(value))
                list.Remove(value);

            InvokeEvents(invokeEvents);
        }

        public string[] ToArray() => list.ToArray();

        public void Clear(bool invokeEvents = true)
        {
            list.Clear();
            InvokeEvents(invokeEvents);
        }
    }
}