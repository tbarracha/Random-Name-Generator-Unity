
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    public abstract class ScriptableIndexedPlayerPrefListSO : ScriptableObject
    {
        [Header("Index")]
        [NaughtyAttributes.ResizableTextArea] [TextArea] [SerializeField] protected string description = "Insert list description... \n Ex: 'Enemy damage levels (this means that it has X dmg at index/lvl 0, Y dmg at index/lvl 1, etc)'";
        [Space]
        [SerializeField] protected bool resetToDefaultIndex;
        [SerializeField] protected PlayerPrefInt playerPrefListIndex;

        public GameEvent OnValueChanged => playerPrefListIndex.OnValueChanged;
        public GameEvent<int> OnIndexChanged => playerPrefListIndex.OnIntChanged;


        /// <summary>
        /// Int value from the PlayerPrefInt
        /// </summary>
        public int Index => playerPrefListIndex.Int;

        public void ResetToDefault(bool save) => playerPrefListIndex.ResetToDefault(save);

        public void SetIndex(int index, bool save) => playerPrefListIndex.SetInt(index, save);

        public void IncrementIndex(bool save) => playerPrefListIndex.SetInt((int)Mathf.Clamp(playerPrefListIndex.Int + 1, 0, Mathf.Infinity), save);

        public void DecrementIndex(bool save) => playerPrefListIndex.SetInt((int)Mathf.Clamp(playerPrefListIndex.Int - 1, 0, Mathf.Infinity), save);

        public int LoadIndex()
        {
            playerPrefListIndex.Load();
            return Index;
        }

        protected virtual void OnValidate()
        {
           if (resetToDefaultIndex)
            {
                ResetToDefault(true);
                resetToDefaultIndex = false;
            }
        }
    }
}