public class OnAirState : PlayerState
{
    public OnAirState(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void Update()
    {
        base.Update();
        float movement = _player.MovementInput;
        if (movement != 0)
        {
            _player.SetVelocity(movement * _player.MoveSpeed); 
        }
    }

}
