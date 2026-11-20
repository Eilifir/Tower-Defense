using System.Collections.Generic;
using UnityEngine;

public class InitEnemies : MonoBehaviour
{
    [SerializeField] GameObject WaypointsFather;
    [SerializeField] EnemyType type;
    List<Vector2> points = new List<Vector2>();
    void Start()
    {
        for (int i = 0; i < WaypointsFather.transform.childCount; i++)
        {
            points.Add(WaypointsFather.transform.GetChild(i).position);
        }
        Enemy enemy = GetComponent<Enemy>();
        enemy.Initialize(EnemyFlyWeightPointer.Get(type), points);
    }

}

