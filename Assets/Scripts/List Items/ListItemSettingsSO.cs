
using UnityEngine;

[CreateAssetMenu(menuName = "Settings / List Item Settings")]
public class ListItemSettingsSO : ScriptableObject
{
    public Vector2 sizeSelected = new Vector2(128 + 40, 32);
    public Vector2 sizeIdle = new Vector2(128, 32);
    [Space]
    public float moveDuration = .2f;
    public float fadeDuration = .2f;
    [Space]
    public Color[] textColors = { Color.white, Color.gray };
}