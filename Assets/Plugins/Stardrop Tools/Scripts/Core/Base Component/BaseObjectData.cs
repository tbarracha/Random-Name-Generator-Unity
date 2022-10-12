
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// GameObject and Transform manipulation data
    /// </summary>
    [System.Serializable]
    public struct BaseObjectData
    {
        [SerializeField] GameObject gameObject;
        [SerializeField] Transform transform;
        private bool isActive;

        public BaseObjectData(GameObject gameObject)
        {
            this.gameObject = gameObject;
            transform = gameObject.transform;
            isActive = gameObject.activeInHierarchy;
        }

        #region Core
        public GameObject GameObject { get => gameObject; }
        public Transform Transform { get => transform; }
        public bool IsActive { get => isActive; }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
            isActive = value;
        }
        #endregion // core


        #region Position
        public Vector3 Position { get => transform.position; set => transform.position = value; }
        public Vector3 LocalPosition { get => transform.localPosition; set => transform.localPosition = value; }

        public void SetPositionX(float x) => Position = UtilsVector.SetVectorX(Position, x);
        public void SetPositionY(float y) => Position = UtilsVector.SetVectorY(Position, y);
        public void SetPositionZ(float z) => Position = UtilsVector.SetVectorZ(Position, z);

        public void SetLocalPositionX(float x) => LocalPosition = UtilsVector.SetVectorX(Position, x);
        public void SetLocalPositionY(float y) => LocalPosition = UtilsVector.SetVectorY(Position, y);
        public void SetLocalPositionZ(float z) => LocalPosition = UtilsVector.SetVectorZ(Position, z);

        #endregion // position


        #region Rotation
        public Quaternion Rotation { get => transform.rotation; set => transform.rotation = value; }
        public Quaternion LocalRotation { get => transform.localRotation; set => transform.localRotation = value; }

        public Vector3 EulerAngles { get => transform.eulerAngles; set => transform.eulerAngles = value; }
        public Vector3 LocalEulerAngles { get => transform.localEulerAngles; set => transform.localEulerAngles = value; }

        public void SetEulerX(float x) => EulerAngles = UtilsVector.SetVectorX(EulerAngles, x);
        public void SetEulerY(float y) => EulerAngles = UtilsVector.SetVectorY(EulerAngles, y);
        public void SetEulerZ(float z) => EulerAngles = UtilsVector.SetVectorZ(EulerAngles, z);

        public void SetLocalEulerX(float x) => LocalEulerAngles = UtilsVector.SetVectorX(LocalEulerAngles, x);
        public void SetLocalEulerY(float y) => LocalEulerAngles = UtilsVector.SetVectorY(LocalEulerAngles, y);
        public void SetLocalEulerZ(float z) => LocalEulerAngles = UtilsVector.SetVectorZ(LocalEulerAngles, z);
        #endregion // Rotation


        #region Scale
        public Vector3 LocalScale { get => transform.localScale; set => transform.localScale = value; }

        public void SetScaleX(float x) => LocalScale = UtilsVector.SetVectorX(LocalScale, x);
        public void SetScaleY(float y) => LocalScale = UtilsVector.SetVectorY(LocalScale, y);
        public void SetScaleZ(float z) => LocalScale = UtilsVector.SetVectorZ(LocalScale, z);
        #endregion // Scale
    }
}