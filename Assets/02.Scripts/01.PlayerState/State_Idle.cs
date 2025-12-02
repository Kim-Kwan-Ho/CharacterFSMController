public class State_Idle : PlayerState
{
    public State_Idle(Player player, StateMachine stateMachine, string animationName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animationName = animationName;
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
