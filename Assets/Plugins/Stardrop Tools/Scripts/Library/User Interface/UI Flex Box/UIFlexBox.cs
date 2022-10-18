using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.UI
{
    /// <summary>
    /// Class that aims to facilitate one dimensional layouts with irregular children sizes
    /// </summary>
    public class UIFlexBox : BaseUIObject
    {
        [Header("Components")]
        [SerializeField] RectTransform parent;
        [SerializeField] UIOrientation orientation;
        [SerializeField] bool parentIsFlexBox;

        [Header("Flex Items")]
        public Vector2 size;
        [SerializeField] bool copyParentSize;
        [Space]
        [SerializeField] bool getItems;
        [SerializeField] UIFlexItem[] flexItems = new UIFlexItem[0];

        public void GetFlexItems()
        {
            flexItems = new UIFlexItem[RectTransform.childCount];
            float percent = 1.0f / flexItems.Length;

            for (int i = 0; i < RectTransform.childCount; i++)
            {
                flexItems[i] = new UIFlexItem(RectTransform.GetChild(i).GetComponent<RectTransform>(), percent, false);
                UtilitiesUI.SetRectTransformAnchor(flexItems[i].RectTransform, UIAnchor.TopLeft);
                UtilitiesUI.SetRectTransformPivot(flexItems[i].RectTransform, UIPivot.TopLeft);

                switch (orientation)
                {
                    case UIOrientation.Horizontal:
                        flexItems[i].SetSize(percent * SizeDelta.x, SizeDelta.y);
                        break;

                    case UIOrientation.Vertical:
                        flexItems[i].SetSize(SizeDelta.x, percent * SizeDelta.y);
                        break;

                    default:
                        break;
                }
            }
        }

        public void RefreshFlexItems()
        {
            if (flexItems.Length == 0)
                return;

            // Get Reference size
            float remainingSize = 0;

            if (orientation == UIOrientation.Horizontal)
                remainingSize = SizeRect.x;

            else if (orientation == UIOrientation.Vertical)
                remainingSize = SizeRect.y;


            // Loop through items and set size
            // They occupy percentage, based on space left
            for (int i = 0; i < flexItems.Length; i++)
            {
                var item = flexItems[i];
                float targetSize = remainingSize * item.Percent;
                remainingSize -= targetSize;

                switch (orientation)
                {
                    case UIOrientation.Horizontal:
                        
                        item.SetSize(targetSize, SizeDelta.y);

                        if (i > 0)
                        {
                            var prevItem = flexItems[i - 1];
                            item.RectTransform.anchoredPosition = new Vector2(prevItem.RectTransform.anchoredPosition.x + prevItem.RectTransform.sizeDelta.x, 0);
                        }

                        break;


                    case UIOrientation.Vertical:

                        item.SetSize(SizeDelta.x, targetSize);

                        if (i > 0)
                        {
                            var prevItem = flexItems[i - 1];
                            item.RectTransform.anchoredPosition = new Vector2(0, prevItem.RectTransform.anchoredPosition.y - prevItem.RectTransform.sizeDelta.y);
                        }

                        break;


                    default:
                        Debug.Log("Flex Orientation CAN'T be set to BOTH");
                        break;
                }
            }
        }

        public void SetSize(Vector2 size)
        {
            parentIsFlexBox = parent.GetComponent<UIFlexBox>() != null;

            if (parentIsFlexBox == false)
            {
                this.size = size;
                SetSizeDelta(size);
            }
        }

        public void CopyParentSize()
        {
            if (parent == null)
                parent = RectTransform.parent.GetComponent<RectTransform>();

            parentIsFlexBox = parent.GetComponent<UIFlexBox>() != null;

            if (parentIsFlexBox == false)
                size = parent.rect.size;

            else
            {
                switch (orientation)
                {
                    case UIOrientation.Horizontal:
                        size = new Vector2(parent.sizeDelta.x, SizeDelta.y);
                        break;

                    case UIOrientation.Vertical:
                        size = new Vector2(SizeDelta.x, parent.sizeDelta.y);
                        break;

                    default:
                        break;
                }
            }
        }

        public void RefreshChildrenFlexBoxes()
        {
            UIFlexBox[] childrenFlexBoxes = GetComponentsInChildren<UIFlexBox>();

            if (childrenFlexBoxes.Length > 0)
                for (int i = 0; i < childrenFlexBoxes.Length; i++)
                    childrenFlexBoxes[i].RefreshChildrenFlexBoxes();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (parent == null)
                parent = RectTransform.parent.GetComponent<RectTransform>();

            parentIsFlexBox = parent.GetComponent<UIFlexBox>() != null;

            if (copyParentSize)
            {
                CopyParentSize();
                copyParentSize = false;
            }

            SetSize(size);

            if (getItems)
            {
                GetFlexItems();
                getItems = false;
            }

            RefreshFlexItems();
        }
    }
}