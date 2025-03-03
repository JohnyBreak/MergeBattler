using System.Collections;
using UnityEngine;

public class EntityAttackState : IEntityState
{

    private Entity _enemy;
    private StateMachineBehaviour _sm;
    private IEntityState _moveState;
    private float _attack;
    private Coroutine _attackRoutine;

    public void Init(
        StateMachineBehaviour sm,
        IEntityState moveState,
        float attack)
    {
        _sm = sm;
        _moveState = moveState;
        _attack = attack;
    }

    public void CheckSwitchState()
    {
        if (_enemy == null)
        {
            _sm.SwitchState(_moveState);
            return;
        }

        if (DistanceHelper.Instance.GetSqrDistance(_sm.Entity, _enemy) > 2)
        {
            _sm.SwitchState(_moveState);
            return;
        }
    }

    public void Enter()
    {
        _enemy = DistanceHelper.Instance.FindClosest(_sm.Entity);
        _attackRoutine = _sm.StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);

            Attack();

        }
    }

    private void Attack()
    {
        Debug.Log($"Attack with {_attack}");
    }

    public void Exit()
    {
        if (_attackRoutine != null) 
        {
            _sm.StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }
    }

    public void Update()
    {
        CheckSwitchState();
    }
}