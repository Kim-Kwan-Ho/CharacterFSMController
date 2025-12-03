using UnityEngine;

public class Player : BaseBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    
    [Header("State Machine")]
    private StateMachine _stateMachine;
    private State_Idle _stateIdle;
    public State_Idle StateIdle { get { return _stateIdle; } }
    private State_Move _stateMove;
    public State_Move StateMove {get { return _stateMove; }}
    private State_Jump _stateJump;
    public State_Jump StateJump {get { return _stateJump; }}



    // Todo: Change This To Module
    [Header("Stats")]
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }
    [SerializeField] private float _jumpPower;
    public float JumpPower {get { return _jumpPower; }}


    [Header("Direction")]
    private bool _isFacingRight;


    [Header("GroundCheck")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckOffSet;
    [SerializeField] private float _groundCheckDist;

    // Todo: Change This To New Input System
    [Header("Input")]
    private float _movementInput;
    public float MovementInput { get { return _movementInput; } }
    private bool _jumpInput;
    public bool JumpInput { get { return _jumpInput; } }





#region 
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
        _stateJump = new State_Jump(this, _stateMachine, "Jump");
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
    public void SetForce(Vector2 force ,ForceMode2D forceMode)
    {
        _rigid.AddForce(force, forceMode);
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
        _jumpInput = Input.GetButtonDown("Jump");
    }
    public bool IsGrounded()
    {
        return Physics2D.Raycast((Vector2)transform.position - _groundCheckOffSet, Vector2.down, _groundCheckDist, _groundLayer); ;
    }
    public bool IsFalling()
    {
        return _rigid.velocity.y <= 0;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var start = transform.position - (Vector3)_groundCheckOffSet;
        var end = start + ((Vector3.down) * _groundCheckDist);
        Gizmos.DrawLine(start, end);
    }
    protected override void OnBindField()
    {
        base.OnBindField();
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
#endif

}
