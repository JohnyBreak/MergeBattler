using UnityEngine;

public class StateMachineBehaviour : MonoBehaviour
{
    [SerializeField] private Entity _entity;

    private IEntityState _currentState;

    public Entity Entity => _entity;

    private void Awake()
    {

        if (_entity == null) 
        {
            if (TryGetComponent<Entity>(out Entity entity)) 
            {
                _entity = entity;
            }
        }

        var idle = new EntityIdleState();
        var move = new EntityMoveState();
        var attack = new EntityAttackState();

        idle.Init(this, move, attack);
        move.Init(this, idle, attack, 2);
        attack.Init(this, move, 5);
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