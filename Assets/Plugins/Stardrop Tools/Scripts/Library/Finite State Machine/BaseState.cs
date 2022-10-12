
using UnityEngine;

namespace StardropTools.FiniteStateMachine
{
    /// <summary>
    /// Base state Class from which all other state may derive from.
    /// </summary>
    [System.Serializable]
    public class BaseState : IBaseState
    {
        [SerializeField] protected string stateName;
        [SerializeField] protected int stateID;
        [SerializeField] protected float timeInState;
        [Space]
        [SerializeField] protected FiniteStateMachine stateMachine;

        public FiniteStateMachine StateMachine => stateMachine;
        public int StateID => stateID;
        public string StateName => stateName;
        public float TimeInState => timeInState;

        public bool IsInitialized { get; protected set; }
        public bool IsPaused { get; protected set; }
        public int GetStateID() => stateID;


        public readonly GameEvent<BaseState> OnStateEnter = new GameEvent<BaseState>();
        public readonly GameEvent<BaseState> OnStateExit = new GameEvent<BaseState>();
        public readonly GameEvent<BaseState> OnStateUpdate = new GameEvent<BaseState>();
        public readonly GameEvent<BaseState> OnStateInput = new GameEvent<BaseState>();

        public readonly GameEvent OnEnter = new GameEvent();
        public readonly GameEvent OnExit = new GameEvent();
        public readonly GameEvent OnUpdate = new GameEvent();
        public readonly GameEvent OnInput = new GameEvent();


        #region Constructor
        public BaseState() { }

        public BaseState(string stateName) => this.stateName = stateName;

        public BaseState(FiniteStateMachine stateMachine, int stateID, string stateName)
        {
            this.stateMachine = stateMachine;
            this.stateID = stateID;
            this.stateName = stateName;
        }
        #endregion // Constructor


        #region Setters
        public BaseState SetStateID(int stateID)
        {
            this.stateID = stateID;
            return this;
        }

        public BaseState SetStateName(string stateName)
        {
            this.stateName = stateName;
            return this;
        }

        public BaseState SetStateIDandName(int stateID, string stateName)
        {
            this.stateID = stateID;
            this.stateName = stateName;
            return this;
        }

        public BaseState SetStateMachine(FiniteStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            return this;
        }
        #endregion // setters

        public virtual void Initialize(FiniteStateMachine stateMachine, int stateID)
        {
            if (IsInitialized)
                return;

            this.stateMachine = stateMachine;
            this.stateID = stateID;

            IsInitialized = true;
        }

        public virtual void Initialize(FiniteStateMachine stateMachine, int stateID, string stateName)
        {
            if (IsInitialized)
                return;

            this.stateMachine = stateMachine;
            this.stateID = stateID;
            this.stateName = stateName;

            IsInitialized = true;
        }


        public virtual void EnterState()
        {
            timeInState = 0;

            OnEnter?.Invoke();
            OnStateEnter?.Invoke(this);
        }


        public virtual void ExitState()
        {
            OnExit?.Invoke();
            OnStateExit?.Invoke(this);
        }


        public virtual void HandleInput()
        {
            if (IsPaused)
                return;

            OnStateInput?.Invoke(this);
        }


        public virtual void UpdateState()
        {
            if (IsPaused)
                return;

            timeInState += Time.deltaTime;

            OnUpdate?.Invoke();
            OnStateUpdate?.Invoke(this);
        }

        public void PauseState()
        {
            if (IsPaused)
                return;

            IsPaused = true;
        }

        public void ResumeState()
        {
            if (IsPaused == false)
                return;

            IsPaused = false;
        }


        protected virtual void ChangeState(BaseStateComponent nextState)
            => stateMachine.ChangeState(nextState);

        protected virtual void ChangeState(int nextStateID)
            => stateMachine.ChangeState(nextStateID);

        public void SetID(int id) => stateID = id;
    }
}