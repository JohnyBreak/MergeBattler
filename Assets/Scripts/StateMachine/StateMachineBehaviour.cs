using UnityEngine;

public class StateMachineBehaviour : MonoBehaviour
{
    private IEntityState _currentState;

    private void Awake()
    {
        var idle = new EntityIdleState();
        var move = new EntityMoveState();
        var attack = new EntityAttackState();

        idle.Init(this, move, attack);
        move.Init(this, idle, attack, 2);
        _currentState = idle;
        
    }

    private void Start()
    {
        _currentState.Enter();
    }

    private void Update()
    {
        _currentState.Update();
    }

    internal void SwitchState(IEntityState moveState)
    {
        _currentState.Exit();
        _currentState = moveState;
        _currentState.Enter();
    }
}