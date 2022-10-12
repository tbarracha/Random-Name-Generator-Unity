
using UnityEngine;

namespace StardropTools.Pool
{
    public class PooledEffect : BaseObject, IPoolable<PooledEffect>
    {
        [SerializeField] float lifetime = 0;
        [SerializeField] bool resetOnSpawn = true;
        [Space]
        [SerializeField] ParticleSystem[] particles;
        [SerializeField] TrailRenderer[] trails;

        protected PoolItem<PooledEffect> poolItem;

        public void Despawn() => poolItem.Despawn();

        public void OnDespawn() { }

        public void OnSpawn()
        {
            if (lifetime > 0)
                Invoke(nameof(Despawn), lifetime);

            if (resetOnSpawn)
            {
                if (particles.Length > 0)
                    foreach (var p in particles)
                        p.Clear();

                if (trails.Length > 0)
                    foreach (var t in trails)
                        t.Clear();
            }
        }

        public void SetPoolItem(PoolItem<PooledEffect> poolItem) => this.poolItem = poolItem;
    }
}