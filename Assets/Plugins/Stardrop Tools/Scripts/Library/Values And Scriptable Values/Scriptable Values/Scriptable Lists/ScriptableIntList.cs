
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable List / Scriptable List Int")]
    public class ScriptableIntList : ScriptableValue
    {
        [SerializeField] List<int> defaultList;
        [SerializeField] List<int> list;

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

            list = new List<int>();
            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);

            InvokeEvents(invokeEvents);
        }

        public int GetInt(int index) => list[index];

        public int GetRandom() => list.GetRandom();

        public List<int> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(int value, bool invokeEvents = true)
        {
            list.Add(value);
            InvokeEvents(invokeEvents);
        }

        public void AddSafe(int value, bool invokeEvents = true)
        {
            if (list.Contains(value) == false)
                list.Add(value);

            InvokeEvents(invokeEvents);
        }

        public void Remove(int value, bool invokeEvents = true)
        {
            list.Remove(value);
            InvokeEvents(invokeEvents);
        }

        public void RemoveSafe(int value, bool invokeEvents = true)
        {
            if (list.Contains(value))
                list.Remove(value);

            InvokeEvents(invokeEvents);
        }

        public int[] ToArray() => list.ToArray();

        public void Clear(bool invokeEvents = true)
        {
            list.Clear();
            InvokeEvents(invokeEvents);
        }
    }
}