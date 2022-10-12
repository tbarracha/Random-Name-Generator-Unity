
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Base component for easy GameObject and Transform manipulation
    /// <para> Mostly used for dynamic objects that are very frequently moved around </para>
    /// </summary>
    public class BaseObject : BaseComponent
    {
        #region Core Data
        protected BaseObjectData objectData;

        public BaseObjectData ObjectData { get => objectData; }
        public GameObject GameObject { get => objectData.GameObject; }
        public Transform Transform { get => objectData.Transform; }
        public Transform Parent { get => Transform.parent; set => SetParent(value); }

        public bool IsActive { get => ObjectData.IsActive; }

        public void SetActive(bool value)
        {
            ObjectData.SetActive(value);

            if (value)
                OnActivate?.Invoke();
            else
                OnDeactivate?.Invoke();
        }
        #endregion // core


        #region Position
        /// <summary>
        /// Current world position of the object
        /// </summary>
        public Vector3 WorldPosition { get => Transform.position; set => Transform.position = value; }

        /// <summary>
        /// Current local position of the object
        /// <para> "Local Position" is the objects position in relation to its parent, where the parent is the origin (Vector3.Zero) </para>
        /// </summary>
        public Vector3 LocalPosition { get => Transform.localPosition; set => Transform.localPosition = value; }

        /// <summary>
        /// World Position of this object where the Initialize() method was called
        /// </summary>
        public Vector3 InitializedPosition { get; protected set; }

        /// <summary>
        /// World Position where the object was Enabled/Activated
        /// </summary>
        public Vector3 EnabledPosition { get; protected set; }

        /// <summary>
        /// World Position where the object was Disabled/Deactivated
        /// </summary>
        public Vector3 DisabledPosition { get; protected set; }

        /// <summary>
        /// Transform.forward
        /// </summary>
        public Vector3 Forward { get => Transform.forward; }

        /// <summary>
        /// X (horizontal) value of the World Position vector
        /// </summary>
        public float PosX { get => WorldPosition.x; set => objectData.SetPositionX(value); }

        /// <summary>
        /// Y (height) value of the World Position vector
        /// </summary>
        public float PosY { get => WorldPosition.y; set => objectData.SetPositionY(value); }

        /// <summary>
        /// Z (depth) value of the World Position vector
        /// </summary>
        public float PosZ { get => WorldPosition.z; set => objectData.SetPositionZ(value); }

        /// <summary>
        /// X (horizontal) value of the Local Position vector
        /// </summary>
        public float LocalPosX { get => LocalPosition.x; set => objectData.SetLocalPositionX(value); }

        /// <summary>
        /// Y (height) value of the Local Position vector
        /// </summary>
        public float LocalPosY { get => LocalPosition.y; set => objectData.SetLocalPositionY(value); }

        /// <summary>
        /// Z (depth) value of the Local Position vector
        /// </summary>
        public float LocalPosZ { get => LocalPosition.z; set => objectData.SetLocalPositionZ(value); }

        /// <summary>
        /// Set the current world position of this object
        /// </summary>
        public void SetWorldPosition(Vector3 position) => WorldPosition = position;

        /// <summary>
        /// Set the current local position of this object
        /// </summary>
        public void SetLocalPosition(Vector3 localPosition) => LocalPosition = localPosition;
        #endregion // position


        #region Rotation
        /// <summary>
        /// Current world quaternion rotation of the object
        /// </summary>
        public Quaternion WorldRotation { get => Transform.rotation; set => Transform.rotation = value; }

        /// <summary>
        /// Current local quaternion rotation of the object
        /// <para> "Local Rotation" is the objects rotation in relation to its parent, where the parent is the origin (Quaternion.Identity) </para>
        /// </summary>
        public Quaternion LocalRotation { get => Transform.localRotation; set => Transform.localRotation = value; }

        /// <summary>
        /// Current world rotation of the object in Euler Angles
        /// </summary>
        public Vector3 WorldEulerAngles { get => Transform.eulerAngles; set => Transform.eulerAngles = value; }

        /// <summary>
        /// Current local rotation of the object in Euler Angles
        /// </summary>
        public Vector3 LocalEulerAngles { get => Transform.localEulerAngles; set => Transform.localEulerAngles = value; }

        /// <summary>
        /// X value of the objects World Euler Angles vector
        /// </summary>
        public float EulerX { get => WorldEulerAngles.x; set => objectData.SetEulerX(value); }

        /// <summary>
        /// Y value of the objects World Euler Angles vector
        /// </summary>
        public float EulerY { get => WorldEulerAngles.y; set => objectData.SetEulerY(value); }

        /// <summary>
        /// Z value of the objects World Euler Angles vector
        /// </summary>
        public float EulerZ { get => WorldEulerAngles.z; set => objectData.SetEulerZ(value); }

        /// <summary>
        /// X value of the objects Local Euler Angles vector
        /// </summary>
        public float LocalEulerX { get => LocalEulerAngles.x; set => objectData.SetEulerX(value); }

        /// <summary>
        /// Y value of the objects Local Euler Angles vector
        /// </summary>
        public float LocalEulerY { get => LocalEulerAngles.y; set => objectData.SetEulerY(value); }

        /// <summary>
        /// Z value of the objects Local Euler Angles vector
        /// </summary>
        public float LocalEulerZ { get => LocalEulerAngles.z; set => objectData.SetEulerZ(value); }

        /// <summary>
        /// Set this objects World Quaternion Rotation
        /// </summary>
        public void SetRotation(Quaternion rotation) => WorldRotation = rotation;

        /// <summary>
        /// Set this objects Local Quaternion Rotation
        /// </summary>
        public void SetLocalRotation(Quaternion localRotation) => LocalRotation = localRotation;

        /// <summary>
        /// Set this objects World Euler Angles Rotation
        /// </summary>
        public void SetEulerAngles(Vector3 euler) => WorldRotation = Quaternion.Euler(euler);

        /// <summary>
        /// Set this objects Local Euler Angles Rotation
        /// </summary>
        public void SetLocalEulerAngles(Vector3 localEuler) => WorldRotation = Quaternion.Euler(localEuler);
        #endregion // rotation


        #region Scale
        /// <summary>
        /// Current objects Local Scale
        /// <para> Scale is always local </para>
        /// </summary>
        public Vector3 LocalScale { get => Transform.localScale; set => Transform.localScale = value; }

        /// <summary>
        /// X value of the objects Local Scale vector
        /// </summary>
        public float LocalScaleX { get => LocalScale.x; set => objectData.SetScaleX(value); }

        /// <summary>
        /// Y value of the objects Local Scale vector
        /// </summary>
        public float LocalScaleY { get => LocalScale.y; set => objectData.SetScaleY(value); }

        /// <summary>
        /// Z value of the objects Local Scale vector
        /// </summary>
        public float LocalScaleZ { get => LocalScale.z; set => objectData.SetScaleZ(value); }

        /// <summary>
        /// Set this objects local scale
        /// </summary>
        public void SetLocalScale(Vector3 localScale) => LocalScale = localScale;
        #endregion // scale


        #region Events

        /// <summary>
        /// Event fired when this object is Activated
        /// </summary>
        public readonly GameEvent OnActivate = new GameEvent();

        /// <summary>
        /// Event fired when this object is Deactivated
        /// </summary>
        public readonly GameEvent OnDeactivate = new GameEvent();

        /// <summary>
        /// Event fired when we change parent via the SetParent() method
        /// </summary>
        public readonly GameEvent OnParentChange = new GameEvent();

        /// <summary>
        /// Event fired when children change via the SetChild() method
        /// </summary>
        public readonly GameEvent OnChildrenChange = new GameEvent();
        #endregion // Events


        protected virtual void DataCheck()
        {
            if (objectData.GameObject == null)
                objectData = new BaseObjectData(gameObject);
        }

        public override void Initialize()
        {
            base.Initialize();

            DataCheck();
            InitializedPosition = WorldPosition;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            DataCheck();
            EnabledPosition = WorldPosition;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            DataCheck();
            DisabledPosition = WorldPosition;
        }

        protected virtual void OnValidate()
        {
            DataCheck();
        }

        /// <summary>
        /// Set the new Parent of this object, if it isn't its child
        /// </summary>
        public void SetParent(Transform parent)
        {
            if (Transform.parent != parent)
            {
                Transform.parent = parent;
                OnParentChange?.Invoke();
            }

            else
                Debug.Log(name + "is already child of " + parent);
        }

        /// <summary>
        /// Set a new child of this object, it it isn't already a child
        /// </summary>
        public void SetChild(Transform child)
        {
            if (child.parent != Transform)
            {
                child.parent = Transform;
                OnChildrenChange?.Invoke();
            }

            else
                Debug.Log(name + "is already parent of " + child);
        }


        /// <summary>
        /// Set this objects children index in relation to its parent
        /// </summary>
        public void SetSiblingIndex(int siblingIndex) => Transform.SetSiblingIndex(siblingIndex);

        /// <summary>
        /// Returns the Direction TO target Vector
        /// </summary>
        public Vector3 DirectionTo(Vector3 target) => target - WorldPosition;

        /// <summary>
        /// Returns the Direction from this objects position TO target Transform
        /// </summary>
        public Vector3 DirectionTo(Transform target) => target.position - WorldPosition;

        /// <summary>
        /// Returns the Direction FROM target Transform to this objects position
        /// </summary>
        public Vector3 DirectionFrom(Vector3 target) => WorldPosition - target;

        /// <summary>
        /// Returns the Direction FROM target Transform to this objects position
        /// </summary>
        public Vector3 DirectionFrom(Transform target) => WorldPosition - target.position;


        /// <summary>
        /// Returns the Distance TO and FROM this objects position relative to target Vector
        /// </summary>
        public float DistanceTo(Vector3 target) => DirectionTo(target).magnitude;

        /// <summary>
        /// Returns the Distance TO and FROM this objects position relative to target Transform
        /// </summary>
        public float DistanceTo(Transform target) => DirectionTo(target).magnitude;


        /// <summary>
        /// Rotates this object towards target Vector imediately.
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion LookAt(Vector3 direction, bool lockX = true, bool lockY = false, bool lockZ = true)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(lookRot);

            return lookRot;
        }


        /// <summary>
        /// Rotates object towards target Transform imediately.
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion LookAt(Transform target, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = LookAt(lookDir, lockX, lockY, lockZ);

            return targetRot;
        }


        /// <summary>
        /// Rotates object smoothly based on lookSpeed toward target direction. Must be updated!
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion SmoothLookAt(Vector3 direction, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);
            Quaternion targetRot = Quaternion.Slerp(WorldRotation, lookRot, Time.deltaTime * lookSpeed);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(targetRot);

            return targetRot;
        }

        /// <summary>
        /// Rotates object smoothly based on lookSpeed toward target transform. Must be updated!
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion SmoothLookAt(Transform target, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = SmoothLookAt(lookDir, lookSpeed, lockX, lockY, lockZ);

            return targetRot;
        }
    }
}