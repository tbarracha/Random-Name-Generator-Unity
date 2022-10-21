
using UnityEngine;
using StardropTools.Tween;

public class CopyToClipboardNotification : MonoBehaviour
{
    [SerializeField] RectTransform selfRect;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] TMPro.TextMeshProUGUI textMesh;
    [Space]
    [SerializeField] AnimationCurve fadeCurve;
    [SerializeField] Vector2 start;
    [SerializeField] Vector2 end;
    [SerializeField] float moveDuration = .2f;
    [SerializeField] float fadeDuration = .15f;

    Tween tweenMove, tweenOpacity;

    private void Start()
    {
        SetActive(false);
    }

    public void Animate()
    {
        SetActive(true);

        tweenMove?.Stop();
        tweenOpacity?.Stop();

        tweenMove = new TweenAnchoredPosition(selfRect, start, end)
                .SetDuration(moveDuration)
                .SetEaseType(EaseType.EaseOutSine)
                .SetID(selfRect.GetHashCode())
                .Initialize();

        tweenOpacity = new TweenFloat()
                .SetStartEnd(0, 1)
                .SetEaseType(EaseType.AnimationCurve)
                .SetAnimationCurve(fadeCurve)
                .SetDuration(fadeDuration)
                .SetID(selfRect.GetHashCode())
                .Initialize();

        (tweenOpacity as TweenFloat).OnTweenFloat.AddListener(SetGraphicsOpacity);
        tweenMove.OnTweenComplete.AddListener(() => SetActive(false));
    }

    void SetActive(bool value) => selfRect.gameObject.SetActive(value);

    void SetGraphicsOpacity(float opacity)
    {
        UtilitiesUI.SetImageOpacity(image, opacity);
        UtilitiesUI.SetTextMeshOpacity(textMesh, opacity);
    }
}
