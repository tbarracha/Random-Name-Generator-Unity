
using UnityEngine;

namespace StardropTools
{
    public class Toggle : MonoBehaviour
    {
        [SerializeField] bool toggle;

        public bool Value => toggle;

        public readonly GameEvent OnToggle = new GameEvent();
        public readonly GameEvent<bool> OnToggleValue = new GameEvent<bool>();

        public readonly GameEvent OnToggleTrue = new GameEvent();
        public readonly GameEvent OnToggleFalse = new GameEvent();

        public void SetToggle(bool value, bool invokeEvents)
        {
            toggle = value;

            if (invokeEvents)
                ToggleEvents();
        }

        public bool ToggleValue()
        {
            toggle = !toggle;
            ToggleEvents();

            return toggle;
        }

        public void ToggleValue(bool value)
        {
            if (value == Value)
                return;

            toggle = value;
            ToggleEvents();
        }

        void ToggleEvents()
        {
            OnToggle?.Invoke();
            OnToggleValue?.Invoke(toggle);

            if (toggle == true)
                OnToggleTrue?.Invoke();

            if (toggle == false)
                OnToggleFalse?.Invoke();
        }
    }
}