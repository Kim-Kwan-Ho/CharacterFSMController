using UnityEngine;
public class State_Jump : OnAirState
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
        if (_player.IsFalling())
        {
            _stateMachine.ChangeState(_player.StateFall);
            return;
        }
    }
}
