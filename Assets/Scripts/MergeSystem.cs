using System.Collections.Generic;
using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    [SerializeField] private Entity[] _entities;

    private Dictionary<int, Entity> _entityDict = new Dictionary<int, Entity>();

    private void Awake()
    {
        foreach (var entity in _entities)
        {
            _entityDict[entity.Level] = entity;
        }
    }

    public void Merge(Entity first, Entity second) 
    {
        if (first.Level == second.Level)
        {
            HandleMerge(first, second);
        }
        else 
        {
            Back(first, second);
        }
    }

    private void HandleMerge(Entity first, Entity second)
    {
        if (_entityDict.TryGetValue(first.Level + 1, out var newEntity))
        {
            if (newEntity == null)
            {
                Back(first, second);
                return;
            }


            var newObject = Instantiate(newEntity);
            newObject.transform.position = second.transform.position;

            Destroy(first.gameObject);
            Destroy(second.gameObject);
        }
        else 
        {
            Back(first, second);
            return;
        }


        //if (first.Level >= _entities.Length) 
        //{
        //    Back(first, second);
        //    return;
        //}

        //var prefab = _entities[first.Level + 1];
        //if (prefab == null) 
        //{
        //    Back(first, second);
        //    return;
        //}

        //var newEntity = Instantiate(prefab);
        //newEntity.transform.position = second.transform.position;

        //Destroy(first.gameObject);
        //Destroy(second.gameObject);
    }

    private void Back(Entity first, Entity second)
    {
        first.ReturnToPosition();
        //second.ReturnToPosition();
    }
}
