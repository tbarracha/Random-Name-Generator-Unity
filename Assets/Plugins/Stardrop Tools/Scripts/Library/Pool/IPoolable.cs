

namespace StardropTools.Pool
{
    /// <summary>
    /// Interface used in all Components that you want to be part of a Pool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPoolable<T> where T : UnityEngine.Component
    {
        public void SetPoolItem(PoolItem<T> poolItem);

        public void OnSpawn();
        public void OnDespawn();

        public void Despawn();
    }
}