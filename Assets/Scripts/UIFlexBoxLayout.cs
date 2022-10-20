
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StardropTools.UI;

public class UIFlexBoxLayout : LayoutGroup
{
    [Header("Flex box")]
    [SerializeField] RectTransform parent;
    [SerializeField] UIOrientation orientation;
    [SerializeField] bool parentIsFlexBox;

    [Header("Flex Items")]
    public Vector2 size;
    [SerializeField] bool clickToCopyParentSize;
    [Space]
    [SerializeField] bool clickToGetItems;
    [SerializeField] UIFlexItem[] flexItems = new UIFlexItem[0];
    [SerializeField] List<RectTransform> rectsToIgnore = new List<RectTransform>();

    public override void CalculateLayoutInputHorizontal()
    {
        if (childAlignment != TextAnchor.UpperLeft)
            childAlignment = TextAnchor.UpperLeft;

        base.CalculateLayoutInputHorizontal();

        if (parent == null)
            parent = transform.parent.GetComponent<RectTransform>();

        // don't do anything if this component doesn't have a parent
        if (parent == null)
            return;

        if (clickToGetItems)
        {
            GetFlexItems();
            clickToGetItems = false;
        }


    }

    public void AddRectToIgnore(RectTransform rect)
    {
        rectsToIgnore.Add(rect);
    }

    /// <summary>
    /// Get flex items, ignoring rects inside the RectsToIgnore array
    /// </summary>
    public void GetFlexItems()
    {
        List<UIFlexItem> flexList = new List<UIFlexItem>();
        float percent = 1 / transform.childCount;

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform rect = transform.GetChild(i).GetComponent<RectTransform>();
            
            // ignore and move next
            if (rectsToIgnore.Contains(rect))
                continue;

            flexList.Add(new UIFlexItem(rect, percent));
        }

        flexItems = flexList.ToArray();
    }

    #region Ignore
    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }
    #endregion
}
