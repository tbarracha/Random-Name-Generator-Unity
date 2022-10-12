
using UnityEngine;


public static class LoopManagerInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initializer() => LoopManager.Instance.Initialize();
}