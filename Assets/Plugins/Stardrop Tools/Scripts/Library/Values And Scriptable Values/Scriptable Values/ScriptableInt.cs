
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Int")]
    public class ScriptableInt : ScriptableValue
    {
        [SerializeField] int defaultInt;
        [SerializeField] int value;

        public readonly GameEvent<int> OnIntChanged = new GameEvent<int>();

        public int Int { get => value; set => SetInt(value); }
        public int DefaultInt => defaultInt;

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
            OnIntChanged?.Invoke(value);
        }

        public override void Default(bool invokeEvents = true)
        {
            if (invokeEvents == false)
                return;

            value = defaultInt;
            InvokeEvents(invokeEvents);
        }

        public void SetInt(int value, bool invokeEvents = true)
        {
            this.value = value;
            InvokeEvents(invokeEvents);
        }

        public void SetInt(int value) => SetInt(value, true);

        public void SetDefaultInt(int defaultInt, bool setValueEqualsToDefault, bool invokeEvents = true)
        {
            this.defaultInt = defaultInt;

            if (setValueEqualsToDefault)
                value = defaultInt;

            InvokeEvents(invokeEvents);
        }

        public int AddAmount(int amountToAdd, bool invokeEvents = true)
        {
            value += amountToAdd;
            InvokeEvents(invokeEvents);

            return value;
        }

        public int SubtractAmount(int amountToSubtract, bool invokeEvents = true)
        {
            value -= amountToSubtract;
            InvokeEvents(invokeEvents);

            return value;
        }

        public int AddOne(bool invokeEvents = true)
        {
            value++;
            InvokeEvents(invokeEvents);

            return value;
        }

        public int SubtractOne(bool invokeEvents = true)
        {
            value--;
            InvokeEvents(invokeEvents);

            return value;
        }
    }
}