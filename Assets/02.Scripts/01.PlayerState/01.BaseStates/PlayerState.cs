public class PlayerState
{
    protected Player _player;
    protected StateMachine _stateMachine;
    protected string _animationName;



    public PlayerState(Player player, StateMachine stateMachine, string animationName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animationName = animationName;
    }
    
    public virtual void EnterState()
    {
        _player.SetAnimatorBoolean(_animationName, true);
    }

    public virtual void Update()
    {

    }

    public virtual void ExitState()
    {
        _player.SetAnimatorBoolean(_animationName, false);
    }

}
