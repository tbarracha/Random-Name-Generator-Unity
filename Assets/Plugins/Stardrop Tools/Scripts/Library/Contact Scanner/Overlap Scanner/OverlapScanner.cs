
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Lists colliders and invokes events based on contact with filtered colliders
    /// </summary>
    public abstract class OverlapScanner : BaseObject
    {
        [Header("Overlap Scanner")]
        [SerializeField] protected LayerMask contactLayers;
        [SerializeField] protected Vector3 positionOffset;
        [SerializeField] protected bool hasContact;
        [SerializeField] protected bool debug;
        [Space]
        protected Collider[] colliders;
        protected List<Collider> colliderList;

        public int ColliderCount { get => colliders.Exists() ? colliders.Length : 0; }
        public Collider[] Colliders { get => colliders; }
        public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
        public bool HasContact { get => hasContact; }

        #region Events
        public readonly GameEvent OnDetected = new GameEvent();

        public readonly GameEvent OnEnter = new GameEvent();
        public readonly GameEvent OnStay = new GameEvent();
        public readonly GameEvent OnExit = new GameEvent();

        public readonly GameEvent<Collider> OnColliderEnter = new GameEvent<Collider>();
        public readonly GameEvent<Collider> OnColliderStay = new GameEvent<Collider>();
        public readonly GameEvent<Collider> OnColliderExit = new GameEvent<Collider>();

        public readonly GameEvent<string> OnTagEnter = new GameEvent<string>();
        public readonly GameEvent<string> OnTagStay = new GameEvent<string>();
        public readonly GameEvent<string> OnTagExit = new GameEvent<string>();

        public readonly GameEvent<int> OnColliderCountChanged = new GameEvent<int>();

        #endregion // events

        public override void Initialize()
        {
            base.Initialize();

            colliders = new Collider[0];
            colliderList = new List<Collider>();
        }

        public void SetLayerMask(LayerMask layerMask) => contactLayers = layerMask;

        public virtual Collider[] OverlapScan()
        {
            ColliderCheck();
            return colliders;
        }


        /// <summary>
        /// Compare detected Physics.Overlap colliders with a copy of last frames list, invoking events if changes were detected
        /// </summary>
        protected void ColliderCheck()
        {
            if (colliders == null)
                return;

            if (colliderList == null)
                colliderList = new List<Collider>();

            // check for collider contact
            hasContact = colliders.Length > 0;

            // if both colliders length is different, we need to make some checks
            if (colliders.Length != colliderList.Count)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    Collider collider = colliders[i];

                    // Add Collider!
                    if (colliderList.Contains(collider) == false)
                    {
                        colliderList.Add(collider);

                        OnColliderEnter?.Invoke(collider);
                        OnTagEnter?.Invoke(collider.tag);

                        if (debug)
                            Debug.Log("Collider Entered");
                    }

                    // Remove Collider!
                    else
                    {
                        colliderList.Remove(collider);

                        OnColliderExit?.Invoke(collider);
                        OnTagExit?.Invoke(collider.tag);

                        if (debug)
                            Debug.Log("Collider Exited");
                    }

                    OnDetected?.Invoke();
                    OnColliderCountChanged?.Invoke(colliders.Length);
                }
            }

            // No Changes. If there are colliders, all of them existed on the previous frame
            else
            {
                if (colliders.Length > 0)
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Collider collider = colliders[i];

                        OnColliderStay?.Invoke(collider);
                        OnTagStay?.Invoke(collider.tag);
                    }
            }
        }

        // To do
        public void SortByDistance(Vector3 referencePosition)
        {

        }

        /// <summary>
        /// If there are any colliders, returns the first element that conatains the specified Type of Component
        /// </summary>
        public virtual T CheckForComponent<T>()
        {
            if (colliders != null && colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    T obj = colliders[i].GetComponent<T>();
                    if (obj != null)
                        return obj;
                }

                Debug.Log("Object not found");
                return default;
            }

            else
            {
                Debug.Log("No colliders detected");
                return default;
            }
        }


        /// <summary>
        /// If there are any colliders, returns a list of elements that conatain the specified Type of Component
        /// </summary>
        public virtual List<T> CheckForComponents<T>()
        {
            List<T> components = new List<T>();

            if (colliders != null && colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    T obj = colliders[i].GetComponent<T>();
                    if (obj != null)
                        components.Add(obj);
                }

                if (components.Count > 0)
                    return components;

                Debug.Log("Object not found");
                return default;
            }

            else
            {
                Debug.Log("No colliders detected");
                return default;
            }
        }
    }
}


