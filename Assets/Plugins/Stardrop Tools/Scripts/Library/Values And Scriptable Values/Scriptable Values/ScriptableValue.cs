
using UnityEngine;

namespace StardropTools
{
    public abstract class ScriptableValue : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea][SerializeField] protected string description;
#endif

        public readonly GameEvent OnValueChanged = new GameEvent();

        public abstract void Default(bool invoke);
        protected abstract void InvokeEvents(bool invoke);
    }
}