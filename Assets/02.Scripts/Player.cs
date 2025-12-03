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
    public State_Move StateMove { get { return _stateMove; } }
    private State_Jump _stateJump;
    public State_Jump StateJump { get { return _stateJump; } }
    private State_Fall _stateFall;
    public State_Fall StateFall { get { return _stateFall; } }
    private State_WallSlide _wallSlide;
    public State_WallSlide WallSlide { get { return _wallSlide; } }
    private State_WallJump _wallJump;
    public State_WallJump WallJump { get { return _wallJump; } }



    // Todo: Change This To Module
    [Header("Stats")]
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }
    [SerializeField] private float _jumpPower;
    public float JumpPower {get { return _jumpPower; }}
    [SerializeField] private float _wallDecreaseRatio;
    public float WallDecreaseRatio {get { return _wallDecreaseRatio; }}
    [SerializeField] private float _wallJumpPower;
    public float WallJumpPower { get { return _wallJumpPower; } }
    [SerializeField] private Vector2 _wallJumpDirection;
    public Vector2 WallJumpDirection { get { return _wallJumpDirection; } }

    [Header("Direction")]
    private bool _isFacingRight;
    public bool IsFacingRight {get { return _isFacingRight; }}

    [Header("GroundCheck")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckOffSet;
    [SerializeField] private float _groundCheckDist;

    [Header("WallCheck")]
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private Vector2 _wallCheckTopOffSet;
    [SerializeField] private Vector2 _wallCheckBottomOffSet;
    [SerializeField] private float _wallCheckDist;


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
        // Todo: Change String -> AnimationHash
        _stateIdle = new State_Idle(this, _stateMachine, "Idle");
        _stateMove = new State_Move(this, _stateMachine, "Move");
        _stateJump = new State_Jump(this, _stateMachine, "Jump");
        _stateFall = new State_Fall(this, _stateMachine, "Fall");
        _wallSlide = new State_WallSlide(this, _stateMachine, "WallSlide");
        _wallJump = new State_WallJump(this, _stateMachine, "WallJump");
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
    public void SetForce(Vector2 force, ForceMode2D forceMode)
    {
        _rigid.AddForce(force, forceMode);
        CheckDirection(force.x);
    }
    public void ChangeVelocityByRatio(float ratio)
    {
        _rigid.velocity = _rigid.velocity * ratio;
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
        return Physics2D.Raycast((Vector2)transform.position + _groundCheckOffSet, Vector2.down, _groundCheckDist, _groundLayer);
    }
    public bool IsFalling()
    {
        return _rigid.velocity.y <= 0;
    }

    public bool CanSlideWall()
    {
        Vector2 direction = _isFacingRight ? Vector2.right : Vector2.left;
        return Physics2D.Raycast((Vector2)transform.position + _wallCheckTopOffSet, direction, _wallCheckDist, _wallLayer)
        && Physics2D.Raycast((Vector2)transform.position + _wallCheckBottomOffSet, direction, _wallCheckDist, _wallLayer);

    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Jump
        Gizmos.color = Color.red;
        var start = transform.position + (Vector3)_groundCheckOffSet;
        var end = start + ((Vector3.down) * _groundCheckDist);
        Gizmos.DrawLine(start, end);

        // Wall Check
        Gizmos.color = Color.blue;
        Vector2 direction = _isFacingRight ? Vector2.right : Vector2.left;
        start = transform.position + (Vector3)_wallCheckTopOffSet;
        end = start + (Vector3)(direction * _wallCheckDist);
        Gizmos.DrawLine(start, end);

        start = transform.position + (Vector3)_wallCheckBottomOffSet;
        end = start + (Vector3)(direction * _wallCheckDist);
        Gizmos.DrawLine(start, end);

        // Wall Jump
        Gizmos.color = Color.green;
        start = transform.position;
        direction = _isFacingRight ? new Vector2(-_wallJumpDirection.x, _wallJumpDirection.y) : _wallJumpDirection;
        end = start + (Vector3)(direction);
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
