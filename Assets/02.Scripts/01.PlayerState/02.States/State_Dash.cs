using UnityEngine;

public class State_Dash : PlayerState
{
    private float _gravityScale;
    private float _dashDuration;
    private int _dashDirection;
    public State_Dash(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _dashDuration = _player.DashDuration;
        _dashDirection = _player.GetFacingDirection();
        _gravityScale = _player.GetGravityScale();
        _player.ChangeGravityScale(0);
        _player.StartDashCoolTime();
    }

    public override void Update()
    {
        base.Update();
        _dashDuration -= Time.deltaTime;

        if (_dashDuration <= 0)
        {
            if (_player.CanSlideWall())
            {
                _stateMachine.ChangeState(_player.StateWallSlide);
                return;
            }
            if (_player.IsGrounded())
            {
                float movement = _player.MovementInput;
                if (movement != 0)
                {
                    if (_player.RunInput)
                    {
                        _stateMachine.ChangeState(_player.StateRun);
                        return;
                    }
                    else
                    {
                        _stateMachine.ChangeState(_player.StateMove);
                        return;
                    }
                }
                else
                {
                    _stateMachine.ChangeState(_player.StateIdle);
                    return;
                }
            }
            if (_player.IsFalling())
            {
                _stateMachine.ChangeState(_player.StateFall);
                return;
            }
        }
        _player.SetVelocityXResetY(_dashDirection * _player.DashSpeed);
    }

    public override void ExitState()
    {
        base.ExitState();
        _player.SetVelocityX(0);
        _player.ChangeGravityScale(_gravityScale);
    }




}
