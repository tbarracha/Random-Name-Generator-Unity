
using UnityEngine;

[CreateAssetMenu(menuName = "Settings / Generation Buttons Settings")]
public class GenButtonSettingsSO : ScriptableObject
{
    public Color[] textColors = { Color.white, Color.gray };
    public Sprite[] imgSprites;
    public float duration = .2f;
}