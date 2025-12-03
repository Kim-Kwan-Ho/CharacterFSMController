public class State_Run : GroundState
{
    public State_Run(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void Update()
    {
        base.Update();
        float movement = _player.MovementInput;
        if (movement == 0)
        {
            _stateMachine.ChangeState(_player.StateIdle);
            return;
        }
        if (!_player.RunInput)
        {
            _stateMachine.ChangeState(_player.StateMove);
            return;
        }
        
        _player.SetVelocity(movement * _player.RunSpeed);
    }


}
