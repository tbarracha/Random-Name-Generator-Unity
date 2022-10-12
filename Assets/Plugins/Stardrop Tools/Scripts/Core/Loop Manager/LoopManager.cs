

public class LoopManager : Singleton<LoopManager>
{
    public bool IsInitialized { get; private set; }

    public static readonly GameEvent OnAwake = new GameEvent();
    public static readonly GameEvent OnStart = new GameEvent();
    public static readonly GameEvent OnUpdate = new GameEvent();
    public static readonly GameEvent OnLateUpdate = new GameEvent();
    public static readonly GameEvent OnFixedUpdate = new GameEvent();

    public static readonly GameEvent OnEnabled = new GameEvent();
    public static readonly GameEvent OnDisabled = new GameEvent();


    public static UnityEngine.Transform Transform;


    public void Initialize()
    {
        if (IsInitialized)
            return;

        Transform = transform;

        IsInitialized = true;
    }


    protected override void Awake()
    {
        base.Awake();
        OnAwake?.Invoke();
    }


    private void Start() => OnStart?.Invoke();

    private void Update() => OnUpdate?.Invoke();

    private void FixedUpdate() => OnFixedUpdate?.Invoke();

    private void OnEnable() => OnEnabled?.Invoke();

    private void OnDisable() => OnDisabled?.Invoke();
}
