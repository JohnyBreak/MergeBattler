using System.Collections.Generic;
using UnityEngine;

public class DistanceHelper : MonoBehaviour
{
    [SerializeField] private List<Entity> m_Enemies;

    public static DistanceHelper Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Entity FindClosest(Entity character) 
    {
        float dist = float.MaxValue;
        Entity closest = null;

        foreach (var item in m_Enemies)
        {
            var dir = item.transform.position - character.transform.position;
            if (dir.sqrMagnitude < dist)
            {
                dist = dir.sqrMagnitude;
                closest = item;
            } 
        } 
        return closest;
    }
    public float GetSqrDistance(Entity first, Entity second) 
    {
        var dir = first.transform.position - second.transform.position;
        return dir.sqrMagnitude;
    }
}
