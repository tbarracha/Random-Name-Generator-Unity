
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Class that is attached to a Health Component that bridges the events of health
    /// <para>This is usefeull because this scriptable object can be reused between scenes and eliminates the need for a direct reference of a specific Health Component</para>
    /// </summary>
    [CreateAssetMenu(menuName = "Stardrop / Health Container SO")]
    public class HealthContainerSO : ScriptableObject
    {
        [SerializeField] int health;
        [SerializeField] float percent;

        public int Health => health;
        public float PercentHealth => percent;

        public readonly GameEvent<int> OnHealthChanged = new GameEvent<int>();
        public readonly GameEvent<float> OnHealthPercentChanged = new GameEvent<float>();
        
        public void SetHealth(int health)
        {
            this.health = health;
            OnHealthChanged?.Invoke(this.health);
        }

        public void SetPercentHealth(float percent)
        {
            this.percent = percent;
            OnHealthPercentChanged?.Invoke(this.percent);
        }
    }
}