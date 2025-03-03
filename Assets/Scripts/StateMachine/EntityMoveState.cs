using UnityEngine;

public class EntityMoveState : IEntityState
{
    private const float _moveTime = 2;

    private Entity _enemy;
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
        if (_enemy == null) 
        {
            _sm.SwitchState(_idleState);
            return;
        }

        if (DistanceHelper.Instance.GetSqrDistance(_sm.Entity, _enemy) <= 2)
        {
            _sm.SwitchState(_attackState);
            return;
        }

        //if (_timer < _moveTime)
        //{
        //    _timer += Time.deltaTime;
        //    return;
        //}

        //_sm.SwitchState(_idleState);
    }

    public void Enter()
    {
        _enemy = DistanceHelper.Instance.FindClosest(_sm.Entity);

        _timer = 0;
    }

    public void Exit()
    {
    }

    public void Update()
    {
        CheckSwitchState();

        var direction = (_enemy.transform.position - _sm.transform.position).normalized;
        _sm.transform.position += (direction * _speed * Time.deltaTime);
        _sm.transform.LookAt(_enemy.transform, Vector3.up);
    }
}