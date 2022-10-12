
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Lists colliders and events based on contact with filtered colliders
    /// </summary>
    public class OverlapBox : OverlapScanner
    {
        [SerializeField] protected Vector3 boxScale = Vector3.one;

        public Vector3 BoxScale { get => boxScale; set => boxScale = value; }

        public override Collider[] OverlapScan()
            => OverlapBoxScan(WorldPosition, WorldRotation);

        public Collider[] OverlapBoxScan(Vector3 position, Quaternion rotation)
        {
            colliders = Physics.OverlapBox(position + positionOffset, boxScale / 2, rotation, contactLayers);
            ColliderCheck();

            return colliders;
        }

        public Collider[] OverlapBoxScan(Vector3 position, Vector3 scale, Quaternion rotation)
        {
            colliders = Physics.OverlapBox(position + positionOffset, scale / 2, rotation, contactLayers);
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
                Utilities.DrawCube(WorldPosition + positionOffset, boxScale, WorldRotation);
            }
        }
#endif
    }

}