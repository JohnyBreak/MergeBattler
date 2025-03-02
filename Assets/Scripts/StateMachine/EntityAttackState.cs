using System.Collections;
using UnityEngine;

public class EntityAttackState : IEntityState
{
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
        //if(target not in range)
        //{
        //    _sm.SwitchState(_moveState);
        //    return;
        //}
    }

    public void Enter()
    {
        _sm.StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1);

        Attack();

        yield return new WaitForSeconds(1);
        _sm.SwitchState(_moveState);
    }

    private void Attack()
    {
        Debug.Log($"Attack with {_attack}");
    }

    public void Exit()
    {
        _sm.StopCoroutine(AttackRoutine());
        _attackRoutine = null;
    }

    public void Update()
    {
        CheckSwitchState();
    }
}