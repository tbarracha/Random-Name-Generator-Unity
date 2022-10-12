
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Play, CrossFade or Trigger through Animator States on Layer Zero
    /// </summary>
    public class SingleLayerAnimation : MonoBehaviour
    {
        [Header("Simple Animation")]
        [SerializeField] protected Animator animator;
        [SerializeField] protected AnimationEventDetector animEventDetector;
        [SerializeField] protected AnimState[] animStates;
        [SerializeField] int currentAnimID;
        [Min(0)] [SerializeField] protected float overralCrossFadeTime = 0;
        [Space]
        [SerializeField] protected bool getAnimStates;
        [SerializeField] bool createAnyStateTransitions;
        [Space]
        [SerializeField] bool clearAnyStateTransitions;
        [SerializeField] bool clearTriggerParameters;
        [Space]
        [SerializeField] bool debug;

        protected float animDuration;
        protected Coroutine animDurationCR;

        public int StateCount { get => animStates.Length; }
        public AnimState CurrentState { get => animStates[currentAnimID]; }
        public int CurrentAnimID { get => currentAnimID; }

        public readonly GameEvent OnAnimStart = new GameEvent();
        public readonly GameEvent OnAnimComplete = new GameEvent();

        public readonly GameEvent<int> OnAnimStartID = new GameEvent<int>();
        public readonly GameEvent<int> OnAnimCompleteID = new GameEvent<int>();

        public readonly GameEvent<AnimState> OnAnimStateStart = new GameEvent<AnimState>();
        public readonly GameEvent<AnimState> OnAnimStateComplete = new GameEvent<AnimState>();

        public GameEvent<int> OnIntAnimEvent { get => animEventDetector.OnIntAnimEvent; }
        public GameEvent<string> OnStringAnimEvent { get => animEventDetector.OnStringAnimEvent; }


        /// <summary>
        /// Play target animation ID. Not Smooth!
        /// </summary>
        public void PlayAnimation(int animID, bool disableOnFinish = false)
        {
            if (animID == currentAnimID)
                return;

            if (animID < 0 || animID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animID);
                return;
            }


            var targetState = animStates[animID];

            if (animator.enabled == false)
                animator.enabled = true;

            //animator.Play(targetState.StateName, targetState.Layer);
            animator.Play(targetState.StateHash, targetState.Layer);

            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
            currentAnimID = animID;

            OnAnimStartID?.Invoke(currentAnimID);
            OnAnimStateStart?.Invoke(CurrentState);

            if (debug)
                Debug.Log(targetState.StateName);
        }

        /// <summary>
        /// Smoothly Crossfade from Current Animation, to Target Animation ID
        /// </summary>
        public void CrossFadeAnimation(int animID, bool disableOnFinish = false)
        {
            if (animID == currentAnimID)
                return;

            if (animID < 0 || animID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animID);
                return;
            }


            var targetState = animStates[animID];

            if (animator.enabled == false)
                animator.enabled = true;

            //animator.CrossFade(targetState.StateName, targetState.crossfade);
            animator.CrossFade(targetState.StateHash, targetState.crossfade);

            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
            currentAnimID = animID;

            OnAnimStart?.Invoke();
            OnAnimStartID?.Invoke(currentAnimID);
            OnAnimStateStart?.Invoke(CurrentState);

            if (debug)
                Debug.Log(targetState.StateName);
        }


        /// <summary>
        /// Set target animation id trigger parameter as true
        /// & smoothly crossfade to target animation
        /// </summary>
        public void TriggerAnimation(int animID, float resetTriggerDelay = .01f, bool disableOnFinish = false)
        {
            if (animID == currentAnimID)
                return;

            if (animID < 0 || animID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animID);
                return;
            }

            var targetState = animStates[animID];

            // must be name instead of hash because parameter index does not always
            // match state index since we may want to make array changes in the inspector
            animator.SetTrigger(targetState.StateName);
            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);

            currentAnimID = animID;
            Invoke(nameof(ResetTrigger), resetTriggerDelay);

            if (debug)
                Debug.Log(targetState.StateName);
        }

        public void ResetTrigger()
            => animator.ResetTrigger(currentAnimID);

        protected void AnimationLifetime(float time, bool disableOnFinish)
        {
            if (isActiveAndEnabled == false)
                return;

            if (animDurationCR != null)
                StopCoroutine(animDurationCR);

            animDurationCR = StartCoroutine(AnimDurationCR(time, disableOnFinish));
        }

        protected System.Collections.IEnumerator AnimDurationCR(float time, bool disableOnFinish)
        {
            yield return WaitForSecondsManager.GetWait("animation", time);
            animator.enabled = disableOnFinish;

            OnAnimComplete?.Invoke();
            OnAnimCompleteID?.Invoke(currentAnimID);
            OnAnimStateComplete?.Invoke(CurrentState);
        }

        public void ChangeRuntimeAnimatorController(RuntimeAnimatorController runtimeAnimator)
        {
            animator.runtimeAnimatorController = runtimeAnimator;
        }

#if UNITY_EDITOR

        /// <summary>
        /// 
        /// 1) Get Animator Controller Reference
        /// 2) Get Animator Controller States
        /// 3) Get Animation Clips from Animator
        /// 4) Check if States.Length == AnimClips.Length
        /// 5) Loop through states
        /// 5.1) Create AnimState based on state & animClip info
        /// 
        /// </summary>
        protected void GenerateAnimStates()
        {
            if (animator == null)
            {
                Debug.Log("Animator not found!");
                return;
            }

            // 1 & 2) Get Animator Controller States
            // 3) Get Animation Clips from Animator
            var controllerStates = AnimUtilities.GetAnimatorStates(animator, 0);
            var animClips = AnimUtilities.GetAnimationClips(animator);

            // 4) Check if States.Length == AnimClips.Length
            if (controllerStates.Length != animClips.Length)
            {
                Debug.Log("States.Lenth != Animation Clips.Length");
                return;
            }

            var animStateList = new System.Collections.Generic.List<AnimState>();

            // 5) Loop through states
            // 5.1) Create AnimState based on state & animClip info
            for (int i = 0; i < controllerStates.Length; i++)
            {
                var controllerState = controllerStates[i].state;
                var newState = new AnimState(controllerState.name, controllerState.nameHash, 0, .15f, animClips[i].length);
                animStateList.Add(newState);
                Debug.Log("State: " + controllerStates[i].state.name);
            }

            animStates = animStateList.ToArray();
        }


        // Create Any State triggerable Transitions with
        // parameter conditions that have state names

        // 1) create trigger parameters based on Animator States
        // 2) create transitions with parameter names
        void CreateAnyStateTriggerTransition()
        {
            // 1) create trigger parameters based on Animator States
            AnimUtilities.CreateTriggerParametersBasedOnStates(animator, 0, true);

            // 2) create transitions with parameter names
            for (int i = 0; i < animStates.Length; i++)
            {
                string stateName = animStates[i].StateName;
                AnimUtilities.CreateAnyStateTriggerTransition(animator, 0, stateName, stateName);
            }
        }

        void ClearAnyStateTransitions()
            => AnimUtilities.ClearAnyStateTransitions(animator, 0);

        void ClearTriggerParameters()
        {
            var parameters = animator.parameters;
            var listParamToRemove = new System.Collections.Generic.List<AnimatorControllerParameter>();

            // 1) loop through all params
            // 2) check if param is a trigger && name is equal to a reference state
            // 3) remove if true
            for (int i = 0; i < parameters.Length; i++)
            {
                var p = parameters[i];

                if (p.type == AnimatorControllerParameterType.Trigger)
                    for (int j = 0; j < animStates.Length; j++)
                    {
                        if (p.name == animStates[j].StateName)
                        {
                            listParamToRemove.Add(p);
                            break;
                        }
                    }
            }

            AnimUtilities.RemoveAnimatorParameters(animator, listParamToRemove.ToArray());
        }

        protected virtual void OnValidate()
        {
            if (animator == null)
                animator = GetComponent<Animator>();

            if (getAnimStates)
            {
                GenerateAnimStates();
                getAnimStates = false;
            }

            if (createAnyStateTransitions)
            {
                CreateAnyStateTriggerTransition();
                createAnyStateTransitions = false;
            }

            if (clearAnyStateTransitions)
            {
                ClearAnyStateTransitions();
                clearAnyStateTransitions = false;
            }

            if (clearTriggerParameters)
            {
                ClearTriggerParameters();
                clearTriggerParameters = false;
            }

            if (overralCrossFadeTime > 0)
                foreach (AnimState animeState in animStates)
                    animeState.crossfade = overralCrossFadeTime;
        }
#endif
    }
}

