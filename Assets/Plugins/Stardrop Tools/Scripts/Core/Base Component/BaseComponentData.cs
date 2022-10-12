

namespace StardropTools
{
    /// <summary>
    /// BaseComponent Initialization focused data
    /// </summary>
    [System.Serializable]
    public struct BaseComponentData
    {
        [UnityEngine.SerializeField] BaseInitialization initializationAt;
        [UnityEngine.SerializeField] BaseInitialization lateInitializationAt;
        public bool stopUpdateOnDisable;

        public BaseInitialization InitializationAt { get => initializationAt; }
        public BaseInitialization LateInitializationAt { get => lateInitializationAt; }
    }
}

