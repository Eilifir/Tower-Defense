using UnityEngine;

public readonly struct EnemyDeathEvent
{
    public int gold { get; }

    public EnemyDeathEvent(int gold)
    {
        this.gold = gold;
    }
}
