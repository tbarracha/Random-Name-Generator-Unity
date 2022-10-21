
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StardropTools.Tween;
using StardropTools.Pool;

public class ListItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPoolable<ListItem>
{
    [NaughtyAttributes.Expandable][SerializeField] protected ListItemSettingsSO settings;
    [SerializeField] protected RectTransform selfRect;
    [SerializeField] protected Button button;
    [SerializeField] protected TMPro.TextMeshProUGUI textMesh;
    [SerializeField] protected StardropTools.UI.UIToggleButton likeToggle;
    [SerializeField] protected StardropTools.UI.UIToggleButtonComponent toggleComponent;
    public string text;

    [Header("Components to animate")]
    [SerializeField] protected Image[] opacityImages;

    protected Tween tweenPos, tweenTextColor;
    protected PoolItem<ListItem> poolItem;

    public virtual void Initialize()
    {
        likeToggle.Initialize();
        button.onClick.AddListener(() => Generator.Instance.SetGeneratedText(textMesh.text));
        
        likeToggle.OnToggleTrue.AddListener(() => Generator.Instance.AddToSaved(this));
        likeToggle.OnToggleFalse.AddListener(() => Generator.Instance.RemoveFromSaved(this));
    }

    public void SetParent(Transform parent) => transform.SetParent(parent);

    public void OnPointerEnter(PointerEventData eventData)
    {
        tweenPos?.Stop();
        tweenPos = new TweenSizeDelta(selfRect, settings.sizeSelected)
            .SetDuration(settings.moveDuration)
            .SetEaseType(EaseType.EaseOutSine)
            .SetID(selfRect.GetHashCode())
            .Initialize();

        tweenTextColor?.Stop();
        tweenTextColor = new TweenTextMeshColor(textMesh, settings.textColors[1])
            .SetDuration(settings.moveDuration)
            .SetEaseType(EaseType.EaseOutSine)
            .SetID(selfRect.GetHashCode())
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

        tweenPos = new TweenSizeDelta(selfRect, settings.sizeIdle)
            .SetDuration(settings.moveDuration)
            .SetEaseType(EaseType.EaseOutSine)
            .SetID(selfRect.GetHashCode())
            .Initialize();

        tweenTextColor?.Stop();
        tweenTextColor = new TweenTextMeshColor(textMesh, settings.textColors[0])
            .SetDuration(settings.moveDuration)
            .SetEaseType(EaseType.EaseOutSine)
            .SetID(selfRect.GetHashCode())
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

    public void ToggleSaved(bool saved) => likeToggle.Toggle(saved, false);

    public void SetText(string text)
    {
        this.text = text;
        textMesh.text = text;
    }

    public void SetOptionButtonInteractibility(bool value) => likeToggle.SetInteractible(value);


    public void OnSpawn()
    {

    }

    public void OnDespawn()
    {

    }


    public void SetPoolItem(PoolItem<ListItem> poolItem) => this.poolItem = poolItem;

    public void Despawn() => poolItem.Despawn();

    public void SetAsFirstSibling() => selfRect.SetAsFirstSibling();
}
