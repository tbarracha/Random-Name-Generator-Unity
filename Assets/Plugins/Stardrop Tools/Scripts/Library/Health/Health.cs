
using UnityEngine;
using NaughtyAttributes;

namespace StardropTools
{
    public class Health : MonoBehaviour
    {
        [ProgressBar("Health Percent", 1, EColor.Red)]
        [Range(0, 1)][SerializeField] float percent;
        [SerializeField] int startHealth;
        [SerializeField] int maxHealth;
        [SerializeField] int health;
        [SerializeField] bool setStartAsMax;
        [SerializeField] bool setMaxAsStart;
        [SerializeField] bool isDead;

        [Header("Scriptable Reference")]
        [SerializeField] HealthContainerSO healthContainer;

        public int CurrentHealth => health;
        public int StartHealth => startHealth;
        public int MaxHealth => maxHealth;
        public float PercentHealth => percent;
        public bool IsDead => isDead;
        

        public GameEvent OnDamaged = new GameEvent();
        public GameEvent OnHealed = new GameEvent();

        public GameEvent<int> OnDamagedAmount = new GameEvent<int>();
        public GameEvent<int> OnHealedAmount = new GameEvent<int>();

        public GameEvent<float> OnPercentDamaged = new GameEvent<float>();
        public GameEvent<float> OnPercentHealed = new GameEvent<float>();

        public GameEvent<int> OnHealthChanged = new GameEvent<int>();
        public GameEvent<float> OnHealthPercentChanged = new GameEvent<float>();

        public GameEvent OnDeath = new GameEvent();
        public GameEvent OnRevived = new GameEvent();


        public void Initialize(int health)
        {
            this.startHealth = health;
            this.maxHealth = health;
            this.health = health;

            InitialEvents();
        }

        public void Initialize(int startHealth, int maxHealth)
        {
            this.startHealth = startHealth;
            this.maxHealth = maxHealth;
            health = startHealth;

            InitialEvents();
        }

        public void Initialize(int startHealth, int maxHealth, int health)
        {
            this.startHealth = startHealth;
            this.maxHealth = maxHealth;
            this.health = health;

            InitialEvents();
        }


        void InitialEvents()
        {
            GetPercent();
            OnHealthChanged?.Invoke(health);

            if (healthContainer != null)
            {
                OnHealthChanged.AddListener(healthContainer.SetHealth);
                OnHealthPercentChanged.AddListener(healthContainer.SetPercentHealth);
            }
        }


        /// <summary>
        /// Decreases health by damage and returns remaining health. Also checks if health reaches zero
        /// </summary>
        public int ApplyDamage(int damageAmount)
        {
            if (isDead)
                return 0;

            health = Mathf.Clamp(health - damageAmount, 0, maxHealth);

            if (health == 0 && isDead == false)
            {
                isDead = true;
                OnDeath?.Invoke();
            }

            GetPercent();

            OnDamaged?.Invoke();
            OnDamagedAmount?.Invoke(damageAmount);
            OnPercentDamaged?.Invoke(percent);
            OnHealthChanged?.Invoke(health);

            return health;
        }

        /// <summary>
        /// Value from 0 to 1 and Returns remaining health
        /// </summary>
        public int ApplyDamagePercent(float percent, bool fromMaxHealth)
        {
            if (isDead)
                return 0;

            int damage = fromMaxHealth ? Mathf.CeilToInt(percent * maxHealth) : Mathf.CeilToInt(percent * health);
            return ApplyDamage(damage);
        }



        /// <summary>
        /// Returns remaining health
        /// </summary>
        public int ApplyHeal(int healAmount)
        {
            if (isDead)
                return 0;

            health = Mathf.Clamp(health + healAmount, 0, maxHealth);

            if (health > 0 && isDead == true)
                isDead = false;

            GetPercent();

            OnHealed?.Invoke();
            OnHealedAmount?.Invoke(healAmount);
            OnPercentHealed?.Invoke(percent);
            OnHealthChanged?.Invoke(health);

            return health;
        }


        /// <summary>
        /// Value from 0 to 1 and Returns remaining health
        /// </summary>
        public int ApplyHealPercent(float percent, bool fromMaxHealth)
        {
            if (isDead)
                return 0;

            int heal = fromMaxHealth ? Mathf.CeilToInt(percent * maxHealth) : Mathf.CeilToInt(percent * health);
            return ApplyHeal(heal);
        }


        /// <summary>
        /// Clear dead flag and fill Health to Max
        /// </summary>
        public void Revive()
        {
            isDead = false;
            health = maxHealth;
            GetPercent();

            OnHealthChanged?.Invoke(health);
        }


        /// <summary>
        /// Clear dead flag and fill Health to set value
        /// </summary>
        public void Revive(int reviveHealth)
        {
            isDead = false;
            health = reviveHealth;
            GetPercent();

            OnHealthChanged?.Invoke(health);
            OnRevived?.Invoke();
        }


        /// <summary>
        /// Clear dead flag and fill Health to set percent
        /// </summary>
        public void Revive(float percentMaxHealth)
        {
            isDead = false;
            health = Mathf.CeilToInt(percentMaxHealth * maxHealth);
            GetPercent();

            OnHealthChanged?.Invoke(health);
        }

        float GetPercent()
        {
            percent = Mathf.Clamp(health / (float)maxHealth, 0, 1);
            OnHealthPercentChanged?.Invoke(percent);

            return percent;
        }


        void UpdateScriptableValues()
        {

        }

        private void OnValidate()
        {
            GetPercent();

            if (startHealth > maxHealth)
                maxHealth = startHealth;

            health = Mathf.Clamp(health, 0, maxHealth);

            if (setMaxAsStart)
            {
                startHealth = maxHealth;
                health = maxHealth;
                GetPercent();

                setStartAsMax = false;
            }

            if (setStartAsMax)
            {
                maxHealth = startHealth;
                health = startHealth;
                GetPercent();

                setMaxAsStart = false;
            }
        }
    }
}