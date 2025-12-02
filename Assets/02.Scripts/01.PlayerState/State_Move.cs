public class State_Move : PlayerState
{
    public State_Move(Player player, StateMachine stateMachine, string animationName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animationName = animationName;
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
