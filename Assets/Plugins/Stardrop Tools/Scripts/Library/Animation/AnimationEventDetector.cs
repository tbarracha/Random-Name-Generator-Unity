
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Used in conjunction with an Animator Component. Detects animation events based on event ID
    /// </summary>
    public class AnimationEventDetector : MonoBehaviour
    {
        [SerializeField] bool debug;

        public readonly GameEvent<int> OnIntAnimEvent = new GameEvent<int>();
        public readonly GameEvent<string> OnStringAnimEvent = new GameEvent<string>();

        public void IntAnimEvent(int eventID)
        {
            if (debug)
                Debug.LogFormat("Anim event: {0}, detected!", eventID);

            OnIntAnimEvent?.Invoke(eventID);
        }

        public void StringAnimEvent(string eventString)
        {
            if (debug)
                Debug.LogFormat("Anim event: {0}, detected!", eventString);

            OnStringAnimEvent?.Invoke(eventString);
        }
    }
}
