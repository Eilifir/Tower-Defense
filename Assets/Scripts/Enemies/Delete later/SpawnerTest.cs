using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnerTest : MonoBehaviour
{
    [SerializeField] EnemyFactory enemyFactory;
    [SerializeField] EnemyType enemyType;
    EnemyFlyWeight enemyData;
    [SerializeField]List<Vector2> positions = new();
    [SerializeField] GameObject waypointFather;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < waypointFather.transform.childCount; i++)
        {
            positions.Add(waypointFather.transform.GetChild(i).position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyData = EnemyFlyWeightPointer.Get(enemyType);
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            var enemy = enemyFactory.CreateTyped(transform.position, Quaternion.identity);
            enemy.Initialize(enemyData, positions);
        }
    }
}
