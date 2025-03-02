public interface IEntityState 
{
    public void Enter();

    public void Update();

    public void CheckSwitchState();

    public void Exit();
}