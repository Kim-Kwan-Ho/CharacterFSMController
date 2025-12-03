using UnityEngine;

public class State_WallJump : PlayerState
{
    public State_WallJump(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _player.SetForce(GetWallJumpDirection() * _player.WallJumpPower , ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();

        if (_player.IsFalling())
        {
            _stateMachine.ChangeState(_player.StateFall);
            return;
        }
        if (_player.CanSlideWall())
        {
            _stateMachine.ChangeState(_player.WallSlide);
            return;
        }
    }


    private Vector2 GetWallJumpDirection()
    {
        return _player.IsFacingRight ? new Vector2(-_player.WallJumpDirection.x, _player.WallJumpDirection.y) : _player.WallJumpDirection;
    }

}
