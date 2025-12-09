using System.Collections;
using UnityEngine;

public class Player : BaseBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerInputHandler _inputHandler;
    
    [Header("State Machine")]
    private StateMachine _stateMachine;
    private State_Idle _stateIdle;
    public State_Idle StateIdle { get { return _stateIdle; } }
    private State_Move _stateMove;
    public State_Move StateMove { get { return _stateMove; } }
    private State_Run _stateRun;
    public State_Run StateRun { get { return _stateRun; } }
    private State_Jump _stateJump;
    public State_Jump StateJump { get { return _stateJump; } }
    private State_Fall _stateFall;
    public State_Fall StateFall { get { return _stateFall; } }
    private State_WallSlide _stateWallSlide;
    public State_WallSlide StateWallSlide { get { return _stateWallSlide; } }
    private State_WallJump _stateWallJump;
    public State_WallJump StateWallJump { get { return _stateWallJump; } }
    private State_Dash _stateDash;
    public State_Dash StateDash { get { return _stateDash; } }

    // Todo: Change This To Module
    [Header("Stats")]
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }
    [SerializeField] private float _runSpeed;
    public float RunSpeed { get { return _runSpeed; } }
    [SerializeField] private float _jumpPower;
    public float JumpPower {get { return _jumpPower; }}
    [SerializeField] private float _wallDecreaseRatio;
    public float WallDecreaseRatio {get { return _wallDecreaseRatio; }}
    [SerializeField] private float _wallJumpPower;
    public float WallJumpPower { get { return _wallJumpPower; } }
    [SerializeField] private Vector2 _wallJumpDirection;
    public Vector2 WallJumpDirection { get { return _wallJumpDirection; } }
    [SerializeField] private float _dashDuration;
    public float DashDuration { get { return _dashDuration; } }
    [SerializeField] private float _dashSpeed;
    public float DashSpeed { get { return _dashSpeed; } }


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


    public float MovementInput { get { return _inputHandler.MovementInput; } }
    public bool RunInput { get { return _inputHandler.RunInput; } }
    public bool JumpInput { get { return _inputHandler.JumpInput; } }
    public bool DashInput { get { return _inputHandler.DashInput; } }



    // Todo: Change This To Skill System
    [Header("Dash")]
    [SerializeField] private float _dashCooltime;
    private bool _canDash;
    public bool CanDash { get { return _canDash; } }
    private Coroutine _dashRoutine;

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
        _stateRun = new State_Run(this, _stateMachine, "Run");
        _stateJump = new State_Jump(this, _stateMachine, "Jump");
        _stateFall = new State_Fall(this, _stateMachine, "Fall");
        _stateWallSlide = new State_WallSlide(this, _stateMachine, "WallSlide");
        _stateWallJump = new State_WallJump(this, _stateMachine, "WallJump");
        _stateDash = new State_Dash(this, _stateMachine, "Dash");
    }
    private void InitializeOthers()
    {
        _isFacingRight = true;
        _canDash = true;
    }
#endregion
    private void Start()
    {
        _stateMachine.InitializeState(_stateIdle);
    }

    private void Update()
    {
        //HandleInput();
        _stateMachine.UpdateState();
    }
    public void SetAnimatorBoolean(string animatonName, bool isOn)
    {
        _animator.SetBool(animatonName, isOn);
    }

    public void SetVelocityX(float x)
    {
        Vector2 targetVel = new Vector2(x, _rigid.velocity.y);
        _rigid.velocity = targetVel;
        CheckDirection(x);
    }
    
    public void SetVelocityXResetY(float x)
    {
        Vector2 targetVel = new Vector2(x, 0);
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
    public int GetFacingDirection()
    {
        return _isFacingRight ? 1 : -1;
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

    public float GetGravityScale()
    {
        return _rigid.gravityScale;
    }
    public void ChangeGravityScale(float amount)
    {
        _rigid.gravityScale = amount;
    }

    public void StartDashCoolTime()
    {
        _canDash = false;
        _dashRoutine = StartCoroutine(CoDashTimer());
    }

    private IEnumerator CoDashTimer()
    {
        yield return new WaitForSeconds(_dashCooltime);
        _canDash = true;
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
        _inputHandler = GetComponent<PlayerInputHandler>();
    }
#endif
}
