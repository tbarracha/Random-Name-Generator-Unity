﻿
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Bool")]
    public class ScriptableBool : ScriptableValue
    {
        [SerializeField] bool defaultBool;
        [SerializeField] bool value;

        public bool Bool { get => value; set => SetBool(value); }
        public bool DefaultBool => defaultBool;

        public readonly GameEvent<bool> OnBoolChanged = new GameEvent<bool>();

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
            OnBoolChanged?.Invoke(value);
        }

        public override void Default(bool invokeEvents = true)
        {
            if (invokeEvents == false)
                return;

            value = defaultBool;
            InvokeEvents(invokeEvents);
        }

        public void SetBool(bool value, bool invokeEvents = true)
        {
            this.value = value;
            InvokeEvents(invokeEvents);
        }

        public void SetDefaultBool(bool defaultBool, bool setValueEqualsToDefault, bool invokeEvents = true)
        {
            this.defaultBool = defaultBool;

            if (setValueEqualsToDefault)
                value = defaultBool;

            InvokeEvents(invokeEvents);
        }
    }
}