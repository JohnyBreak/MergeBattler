using UnityEngine;

public class EntityMoveState : IEntityState
{
    private const float _moveTime = 2;

    private StateMachineBehaviour _sm;
    private IEntityState _idleState;
    private IEntityState _attackState;
    private float _timer = 0;
    private float _speed;
    public void Init(StateMachineBehaviour sm, IEntityState idleState, IEntityState attackState, float speed)
    {
        _sm = sm;
        _idleState = idleState;
        _attackState = attackState;
        _speed = speed;
    }

    public void CheckSwitchState()
    {
        //if(target in range)
        //{
        //    _sm.SwitchState(_attackState);
        //    return;
        //}

        if (_timer < _moveTime)
        {
            _timer += Time.deltaTime;
            return;
        }
        Debug.Log("Switch to idle");
        _sm.SwitchState(_idleState);
    }

    public void Enter()
    {
        Debug.Log("EnterMove");
        _timer = 0;
    }

    public void Exit()
    {
        Debug.Log("ExitMove");
    }

    public void Update()
    {
        CheckSwitchState();
        _sm.transform.Translate(_sm.transform.forward * _speed * Time.deltaTime);
    }
}