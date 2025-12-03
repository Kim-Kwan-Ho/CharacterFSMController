public class State_WallSlide : PlayerState
{
    public State_WallSlide(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void Update()
    {
        base.Update();

        if (_player.IsGrounded())
        {
            _stateMachine.ChangeState(_player.StateIdle);
            return;
        }

        if (!_player.CanSlideWall())
        {
            _stateMachine.ChangeState(_player.StateFall);
            return;
        }

        if (IsGettingOppositeInput())
        {
            _stateMachine.ChangeState(_player.StateFall);
            return;
        }

        if (_player.JumpInput)
        {
            _stateMachine.ChangeState(_player.StateWallJump);
            return;
        }
        _player.ChangeVelocityByRatio(_player.WallDecreaseRatio);
    }
    
    private bool IsGettingOppositeInput()
    {
        float movement = _player.MovementInput;
        if (_player.IsFacingRight && movement < 0)
        {
            return true;
        }
        if (!_player.IsFacingRight && movement > 0)
        {
            return true;
        }
        return false;
    }

}
