
using UnityEngine;

namespace StardropTools.UI
{
    public abstract class UIToggleButtonComponent : MonoBehaviour
    {
        [SerializeField] protected UIToggleButton button;

        public void Initialize()
        {
            button.OnToggleValue.AddListener(Toggle);
        }

        public abstract void Toggle(bool value);
    }
}