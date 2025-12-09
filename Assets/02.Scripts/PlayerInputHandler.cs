using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : BaseBehaviour, PlayerAction.ICharacterActions
{
    private PlayerAction _playerAction;

    private float _movementInput;
    public float MovementInput { get { return _movementInput; } }

    private bool _jumpInput;
    public bool JumpInput  { get { return _jumpInput; }}

    private bool _runInput;
    public bool RunInput {get { return _runInput; }}

    private bool _dashInput;
    public bool DashInput {get { return _dashInput; }}

    protected override void Awake()
    {
        _playerAction = new PlayerAction();
        _playerAction.Character.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _playerAction.Character.Enable();
    }

    private void OnDisable()
    {
        _playerAction.Character.Disable();
    }

    private void OnDestroy()
    {
        _playerAction.Dispose();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _jumpInput = context.performed;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _runInput = true;
        }
        else if (context.canceled)
        {
            _runInput = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        _dashInput = context.performed;
    }
}
