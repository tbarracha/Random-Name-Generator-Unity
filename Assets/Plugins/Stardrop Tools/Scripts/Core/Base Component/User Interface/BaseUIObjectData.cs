
using UnityEngine;

namespace StardropTools.UI
{
    /// <summary>
    /// GameObject and Transform manipulation data
    /// </summary>
    [System.Serializable]
    public struct BaseUIObjectData
    {
        [SerializeField] RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform;

        public BaseUIObjectData(GameObject gameObject)
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

         #region Rect & RectTransform

        public Vector2 AnchoredPosition { get => rectTransform.anchoredPosition; set => rectTransform.anchoredPosition = value; }
        public Vector2 SizeDelta { get => rectTransform.sizeDelta; set => rectTransform.sizeDelta = value; }

        public float WidthDelta { get => SizeDelta.x; set => SetWidthDelta(value); }
        public float HeightDelta { get => SizeDelta.y; set => SetHeightDelta(value); }


        public Rect Rect { get => rectTransform.rect; }
        public Vector2 SizeRect
        {
            get => rectTransform.rect.size;
            set
            {
                Rect rect = rectTransform.rect;
                rect.size = value;
            }
        }

        public float WidthRect { get => SizeRect.x; set => SetWidthRect(value); }
        public float HeightRect { get => SizeRect.x; set => SetHeightRect(value); }


        public void SetWidthDelta(float width) => SizeDelta = UtilsVector.SetVectorX(SizeDelta, width);
        public void SetHeightDelta(float height) => SizeDelta = UtilsVector.SetVectorY(SizeDelta, height);

        public void SetWidthRect(float width) => SizeRect = UtilsVector.SetVectorX(SizeRect, width);
        public void SetHeightRect(float height) => SizeRect = UtilsVector.SetVectorY(SizeRect, height);
        #endregion // Rect
    }
}