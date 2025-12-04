public class GroundState : PlayerState
{
    public GroundState(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void Update()
    {
        base.Update();
        
        // Detect falling from ground (cliff, disappearing platform)
        if (!_player.IsGrounded() && _player.IsFalling())
        {
            _stateMachine.ChangeState(_player.StateFall);
            return;
        }


        if (_player.IsGrounded() && _player.JumpInput)
        {
            _stateMachine.ChangeState(_player.StateJump);
            return;
        }
        
        if (_player.DashInput && _player.CanDash)
        {
            _stateMachine.ChangeState(_player.StateDash);
            return;
        }
        
    }

}
