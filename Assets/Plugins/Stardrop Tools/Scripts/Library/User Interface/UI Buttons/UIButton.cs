
using UnityEngine;

namespace StardropTools.UI
{
    public class UIButton : BaseUIObject
    {
        public int ButtonID;
        [SerializeField] UnityEngine.UI.Button button;
        [SerializeField] UnityEngine.UI.Selectable selectable;

        public UnityEngine.Events.UnityEvent OnClick => button.onClick;
        public readonly GameEvent<int> OnClickID = new GameEvent<int>();

        public override void Initialize()
        {
            base.Initialize();

            OnClick.AddListener(() => OnClickID?.Invoke(ButtonID));
        }
    }
}