using UnityEngine;

public class Player : BaseBehaviour
{
    [Header("Compoents")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("State Machine")]
    private StateMachine _stateMachine;
    private State_Idle _stateIdle;
    public State_Idle StateIdle { get { return _stateIdle; } }
    private State_Move _stateMove;
    public State_Move StateMove {get { return _stateMove; }}



    [Header("Stats")]
    [SerializeField] private float _moveSpeed = 3f;
    public float MoveSpeed { get { return _moveSpeed; } }


    [Header("Input")]
    private float _movementInput;
    public float MovementInput { get { return _movementInput; } }


    [Header("Direction")]
    private bool _isFacingRight;


#region Init
    protected override void Initialize()
    {
        base.Initialize();
        InitializeStates();
        InitializeOthers();
    }
    private void InitializeStates()
    {
        _stateMachine = new StateMachine();
        _stateIdle = new State_Idle(this, _stateMachine, "Idle");
        _stateMove = new State_Move(this, _stateMachine, "Move");
    }
    private void InitializeOthers()
    {
        _isFacingRight = true;
    }
#endregion
    private void Start()
    {
        _stateMachine.InitializeState(_stateIdle);
    }

    private void Update()
    {
        HandleInput();
        _stateMachine.UpdateState();
    }
    public void SetAnimatorBoolean(string animatonName, bool isOn)
    {
        _animator.SetBool(animatonName, isOn);
    }

    public void SetVelocity(float x)
    {
        Vector2 targetVel = new Vector2(x, _rigid.velocity.y);
        _rigid.velocity = targetVel;
        CheckDirection(x);
    }

    private void CheckDirection(float x)
    {
        if (x > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
        }
        else if (x < 0 && _isFacingRight)
        {
            _isFacingRight = false;
        }
        _spriteRenderer.flipX = !_isFacingRight;
    }

    // Todo: Change This Input to New Input System
    private void HandleInput()
    {
        _movementInput = Input.GetAxis("Horizontal");
    }

#if UNITY_EDITOR

    protected override void OnBindField()
    {
        base.OnBindField();
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
#endif

}
