
using UnityEngine;

namespace StardropTools.UI
{
    public class BaseUIObject : BaseObject
    {
        protected BaseUIObjectData uiObjectData;

        #region Rect & RectTransform
        public RectTransform RectTransform => uiObjectData.RectTransform;

        public Vector2 AnchoredPosition { get => RectTransform.anchoredPosition; set => uiObjectData.AnchoredPosition = value; }
        public Vector2 SizeDelta { get => RectTransform.sizeDelta; set => uiObjectData.SizeDelta = value; }

        public float WidthDelta { get => SizeDelta.x; set => uiObjectData.WidthDelta = value; }
        public float HeightDelta { get => SizeDelta.y; set => uiObjectData.HeightDelta = value; }


        public Rect Rect { get => RectTransform.rect; }
        public Vector2 SizeRect
        {
            get => RectTransform.rect.size;
            set
            {
                Rect rect = RectTransform.rect;
                rect.size = value;
            }
        }

        public float WidthRect { get => SizeRect.x; set => uiObjectData.WidthRect = value; }
        public float HeightRect { get => SizeRect.x; set => uiObjectData.HeightRect = value; }


        public void SetAnchoredPosition(Vector2 anchoredPosition) => AnchoredPosition = anchoredPosition;
        public void SetAnchoredPosition(float x, float y) => AnchoredPosition = new Vector2(x, y);
        
        public void SetSizeDelta(Vector2 sizeDelta) => SizeDelta = sizeDelta;
        public void SetSizeDelta(float x, float y) => SizeDelta = new Vector2(x, y);

        #endregion // Rect

        protected override void DataCheck()
        {
            base.DataCheck();

            if (uiObjectData.RectTransform == null)
                uiObjectData = new BaseUIObjectData(gameObject);
        }


        // Set Pivot & Anchor
        public void SetPivot(UIPivot uiPivot) => UtilitiesUI.SetUIPivot(RectTransform, uiPivot);
        public void SetAnchor(UIAnchor uiAnchor) => UtilitiesUI.SetRectTransformAnchor(RectTransform, uiAnchor);


        // Copy Sizes
        public void CopySizeRect(RectTransform rectTransform) => SizeRect = rectTransform.rect.size;
        public void CopySizeDelta(RectTransform rectTransform) => SizeDelta = rectTransform.sizeDelta;
    }
}