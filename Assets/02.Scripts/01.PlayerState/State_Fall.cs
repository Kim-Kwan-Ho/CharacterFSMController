
public class State_Fall : PlayerState
{
    public State_Fall(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void Update()
    {
        base.Update();
        float movement = _player.MovementInput;
        _player.SetVelocity(movement * _player.MoveSpeed);
        if (_player.IsGrounded())
        {
            if (_player.MovementInput == 0)
            {
                _stateMachine.ChangeState(_player.StateIdle);
            }
            else
            {
                _stateMachine.ChangeState(_player.StateMove);
            }
        }
    }

    

}
