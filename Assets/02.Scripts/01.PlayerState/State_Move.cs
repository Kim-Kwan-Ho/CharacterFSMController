public class State_Move : PlayerState
{
    public State_Move(Player player, StateMachine stateMachine, string animationName) :  base(player, stateMachine, animationName)
    {
        
    }


    public override void Update()
    {
        base.Update();
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
        
        float movement = _player.MovementInput;
        if (movement == 0)
        {
            _stateMachine.ChangeState(_player.StateIdle);
            return;
        }
        _player.SetVelocity(movement * _player.MoveSpeed);
    }

}
