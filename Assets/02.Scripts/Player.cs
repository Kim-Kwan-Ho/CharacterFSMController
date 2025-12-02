using UnityEngine;

public class Player : BaseBehaviour
{
    [Header("Compoents")]
    [SerializeField] private Animator _animator;


    [Header("State Machine")]
    private StateMachine _stateMachine;
    private State_Idle _stateIdle;



    protected override void Initialize()
    {
        base.Initialize();
        _stateMachine = new StateMachine();
        _stateIdle = new State_Idle(this, _stateMachine, "Idle");
    }

    private void Start()
    {
        _stateMachine.InitializeState(_stateIdle);
    }



    public void SetAnimatorBoolean(string animatonName, bool isOn)
    {
        _animator.SetBool(animatonName, isOn);
    }

#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _animator = GetComponent<Animator>();
    }
#endif

}
