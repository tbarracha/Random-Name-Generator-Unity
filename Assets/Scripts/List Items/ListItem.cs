
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StardropTools.Tween;

public class ListItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [NaughtyAttributes.Expandable][SerializeField] protected ListItemSettingsSO settings;
    [SerializeField] protected Button button;
    [SerializeField] protected TMPro.TextMeshProUGUI textMesh;

    [Header("Components to animate")]
    [SerializeField] protected RectTransform optionsRect;
    [SerializeField] protected Button[] optionButtons;
    [SerializeField] protected Image[] opacityImages;

    protected Tween tweenPos;

    private void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        button.onClick.AddListener(() => Generator.Instance.SetGeneratedText(textMesh.text));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tweenPos?.Stop();

        tweenPos = new TweenAnchoredPosition(optionsRect, new Vector2(80, 0))
            .SetDuration(settings.moveDuration)
            .SetEaseType(EaseType.EaseOutSine)
            .SetID(optionsRect.GetHashCode())
            .Initialize();

        tweenPos.OnTweenComplete.AddListener(() => SetOptionButtonInteractibility(true));

        for (int i = 0; i < opacityImages.Length; i++)
        {
            Tween tweenColor = new TweenImageOpacity(opacityImages[i], 1)
                .SetDuration(settings.fadeDuration)
                .SetEaseType(EaseType.EaseOutSine)
                .SetID(opacityImages[i].GetHashCode())
                .Initialize();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tweenPos?.Stop();

        tweenPos = new TweenAnchoredPosition(optionsRect, new Vector2(0, 0))
            .SetDuration(settings.moveDuration)
            .SetEaseType(EaseType.EaseOutSine)
            .SetID(optionsRect.GetHashCode())
            .Initialize();

        tweenPos.OnTweenComplete.AddListener(() => SetOptionButtonInteractibility(false));

        for (int i = 0; i < opacityImages.Length; i++)
        {
            Tween tweenColor = new TweenImageOpacity(opacityImages[i], 0)
                .SetDuration(settings.fadeDuration)
                .SetEaseType(EaseType.EaseOutSine)
                .SetID(opacityImages[i].GetHashCode())
                .Initialize();
        }
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }

    public void SetOptionButtonInteractibility(bool value)
    {
        for (int i = 0; i < optionButtons.Length; i++)
            optionButtons[i].interactable = value;
    }
}
