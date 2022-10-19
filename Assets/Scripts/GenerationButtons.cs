
using UnityEngine;
using UnityEngine.EventSystems;
using StardropTools.Tween;

public class GenerationButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GenButtonSettingsSO settings;
    [SerializeField] UnityEngine.UI.Image buttonImage;
    [SerializeField] TMPro.TextMeshProUGUI textMesh;

    Tween tweenFade, tweenImg;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Animate(1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Animate(0);
    }

    void Animate(int id)
    {
        tweenFade?.Stop();
        tweenFade = new TweenTextMeshColor(textMesh, settings.textColors[id])
            .SetDuration(settings.duration)
            .SetEaseType(EaseType.EaseOutSine)
            .SetID(textMesh.GetHashCode())
            .Initialize();

        tweenImg?.Stop();
        tweenImg = new TweenImageOpacity(buttonImage, id)
            .SetDuration(settings.duration)
            .SetEaseType(EaseType.EaseOutSine)
            .SetID(buttonImage.GetHashCode())
            .Initialize();
    }
}
