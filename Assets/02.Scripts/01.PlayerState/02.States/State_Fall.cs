public class State_Fall : OnAirState
{
    public State_Fall(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void Update()
    {
        base.Update();
        if (_player.IsGrounded())
        {
            if (_player.MovementInput == 0)
            {
                _stateMachine.ChangeState(_player.StateIdle);
                return;
            }
            else
            {
                _stateMachine.ChangeState(_player.StateMove);
                return;
            }
        }

        if (_player.CanSlideWall())
        {
            _stateMachine.ChangeState(_player.WallSlide);
            return;
        }

    }

    

}
