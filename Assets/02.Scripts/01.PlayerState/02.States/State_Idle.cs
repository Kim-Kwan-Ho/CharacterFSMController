public class State_Idle : GroundState
{
    public State_Idle(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        _player.SetVelocity(0);
    }

    public override void Update()
    {
        base.Update();
        if (_player.MovementInput != 0)
        {
            _stateMachine.ChangeState(_player.StateMove);
        }
    }
    

}
