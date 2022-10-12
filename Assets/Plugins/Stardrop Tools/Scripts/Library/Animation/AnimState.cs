
namespace StardropTools
{
    [System.Serializable]
    public class AnimState
    {
        [UnityEngine.SerializeField] string stateName;
        [UnityEngine.SerializeField] int stateHash;
        [UnityEngine.SerializeField] int layer;
        [UnityEngine.SerializeField] float lengthInSeconds;
        [UnityEngine.SerializeField] string transitionName;
        [UnityEngine.Range(0, 1)] public float crossfade = .15f;

        public string StateName { get => stateName; }
        public int StateHash { get => stateHash; }
        public string TransitionName { get => transitionName; }
        public int Layer { get => layer; }
        public float LengthInSeconds { get => lengthInSeconds; }

        public AnimState()
        {
            crossfade = .15f;
        }

        public AnimState(string stateName, int stateHash, int layer, float crossfadeTime, float animLength)
        {
            this.stateName = stateName;
            this.stateHash = stateHash;
            this.layer = layer;
            crossfade = crossfadeTime;
            lengthInSeconds = animLength;
        }

        public AnimState(string stateName, int layer, string transitionName, float crossfadeTime, float animLength)
        {
            this.stateName = stateName;
            this.layer = layer;
            this.transitionName = transitionName;
            crossfade = crossfadeTime;
            lengthInSeconds = animLength;
        }
    }
}