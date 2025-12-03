using UnityEngine;
public class State_Jump : PlayerState
{
    public State_Jump(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _player.SetForce(Vector2.up * _player.JumpPower, ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();
        float movement = _player.MovementInput;
        _player.SetVelocity(movement * _player.MoveSpeed);
        if (_player.IsGrounded() && _player.IsFalling())
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
