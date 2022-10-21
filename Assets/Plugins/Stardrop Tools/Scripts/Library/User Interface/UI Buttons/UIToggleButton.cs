
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(Toggle))]
    public class UIToggleButton : UIButton
    {
        [Header("Toggle Button")]
        [SerializeField] protected bool initialToggle;
        [SerializeField] protected Toggle toggle;
        [SerializeField] protected Transform parentComponents;
        [SerializeField] protected UIToggleButtonComponent[] toggleComponents;

        public bool Value => toggle.Value;

        public GameEvent OnToggle => toggle.OnToggle;
        public GameEvent<bool> OnToggleValue => toggle.OnToggleValue;
        
        public GameEvent OnToggleTrue => toggle.OnToggleTrue;
        public GameEvent OnToggleFalse => toggle.OnToggleFalse;

        public readonly GameEvent<UIToggleButton> OnToggleClass = new GameEvent<UIToggleButton>();
        public readonly GameEvent<int> OnToggleIndex = new GameEvent<int>();

        public readonly GameEvent<int> OnToggleTrueIndex = new GameEvent<int>();
        public readonly GameEvent<int> OnToggleFalseIndex = new GameEvent<int>();



        public override void Initialize()
        {
            base.Initialize();

            Toggle(initialToggle);
            
            OnClick.AddListener(Toggle);
            OnToggle.AddListener(() => OnToggleClass?.Invoke(this));
            OnToggle.AddListener(() => OnToggleIndex?.Invoke(ButtonID));

            OnToggleTrue.AddListener(() => OnToggleTrueIndex?.Invoke(ButtonID));
            OnToggleFalse.AddListener(() => OnToggleFalseIndex?.Invoke(ButtonID));

            if (toggleComponents.Exists())
                for (int i = 0; i < toggleComponents.Length; i++)
                    toggleComponents[i].Initialize();
        }


        public void Toggle()
        {
            toggle.ToggleValue();
            RefreshToggleComponents();
        }

        public void Toggle(bool value)
        {
            if (value == Value)
                return;

            toggle.ToggleValue(value);
            RefreshToggleComponents();
        }

        public void Toggle(bool value, bool invokeEvents)
        {
            toggle.SetToggle(value, invokeEvents);
            RefreshToggleComponents();
        }

        protected void RefreshToggleComponents()
        {
            for (int i = 0; i < toggleComponents.Length; i++)
                toggleComponents[i].Toggle(Value);
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (toggle == null)
                toggle = GetComponent<Toggle>();

            if (parentComponents != null)
                toggleComponents = Utilities.GetItems<UIToggleButtonComponent>(parentComponents).ToArray();
        }
    }
}