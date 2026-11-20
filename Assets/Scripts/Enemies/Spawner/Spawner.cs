using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField]PathMaker pathMaker;
    [SerializeField]EnemyFactory enemyFactory;

    public void Spawn(EnemyType type)
    {
        if (enemyFactory == null || pathMaker == null) return;
        List<Vector2> points = pathMaker.pointsList;
        var enemy = enemyFactory.CreateTyped(points[0], Quaternion.identity);
        var enemyType = EnemyFlyWeightPointer.Get(type);
        enemy.Initialize(enemyType, points);
        EventManager<GameEvents>.Publish(GameEvents.enemySpawned, new EnemySpawnedEvent());
    }
}
