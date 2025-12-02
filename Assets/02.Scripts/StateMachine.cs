public class StateMachine 
{
    private PlayerState _currentState;
    public PlayerState CurrentState { get { return _currentState; } }

    public void InitializeState(PlayerState playerState)
    {
        _currentState = playerState;
        _currentState.EnterState();
    }

    public void ChangeState(PlayerState newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
    
    public void UpdateState()
    {
        _currentState.Update();
    }

}
