
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Lists colliders and invokes based on contact with filtered colliders
    /// </summary>
    public class OverlapSphere : OverlapScanner
    {
        [SerializeField] protected float radius = 1;

        public float Radius { get => radius; set => radius = value; }

        public override Collider[] OverlapScan() => SphereScan(WorldPosition);

        public Collider[] SphereScan(Vector3 position)
        {
            colliders = Physics.OverlapSphere(position + positionOffset, radius, contactLayers);
            ColliderCheck();

            return colliders;
        }

        public Collider[] SphereScan(Vector3 position, float radius)
        {
            colliders = Physics.OverlapSphere(position + positionOffset, radius, contactLayers);
            ColliderCheck();

            return colliders;
        }

#if UNITY_EDITOR
        [Header("Render")]
        [SerializeField] Color color = Color.red;
        [SerializeField] bool drawGizmos = true;

        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = color;
                Gizmos.DrawWireSphere(WorldPosition + positionOffset, radius);
            }
        }
#endif
    }
}


