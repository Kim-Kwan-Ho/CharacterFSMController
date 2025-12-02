
public class State_Idle : PlayerState
{
    public State_Idle(Player player, StateMachine stateMachine, string animationName) 
    {
        _player = player;
        _stateMachine = stateMachine;
        _animationName = animationName;
    }
}
