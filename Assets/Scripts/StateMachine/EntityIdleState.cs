using UnityEngine;

public class EntityIdleState : IEntityState
{
    private const float _idleTime = 1;

    private StateMachineBehaviour _sm;
    private IEntityState _moveState;
    private IEntityState _attackState;
    private float _timer = 0;

    public void Init(StateMachineBehaviour sm, IEntityState moveState, IEntityState attackState)
    {
        _sm = sm;
        _moveState = moveState;
        _attackState = attackState;
    }

    public void CheckSwitchState()
    {
        //if(target in range)
        //{
        //    _sm.SwitchState(_attackState);
        //    return;
        //}

        if (_timer < _idleTime) 
        {
            _timer += Time.deltaTime;
            return;
        }

        _sm.SwitchState(_moveState);
    }

    public void Enter()
    {
        _timer = 0;
    }

    public void Exit()
    {
    }

    public void Update()
    {
        CheckSwitchState();
    }
}