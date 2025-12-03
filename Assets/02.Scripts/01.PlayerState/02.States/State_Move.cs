public class State_Move : GroundState
{
    public State_Move(Player player, StateMachine stateMachine, string animationName) :  base(player, stateMachine, animationName)
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
        _player.SetVelocity(movement * _player.MoveSpeed);
    }

}
