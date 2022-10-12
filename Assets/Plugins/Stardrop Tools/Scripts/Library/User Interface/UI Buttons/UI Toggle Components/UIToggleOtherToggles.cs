
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleOtherToggles : UIToggleButtonComponent
    {
        [SerializeField] UIToggleButton[] toggles;

        public override void Toggle(bool value)
        {
            if (value == true)
                for (int i = 0; i < toggles.Length; i++)
                    toggles[i].Toggle(false);
        }
    }
}