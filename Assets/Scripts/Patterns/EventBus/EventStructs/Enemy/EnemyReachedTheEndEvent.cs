using UnityEngine;

public readonly struct EnemyReachedTheEndEvent
{
    public int livesLost { get; }
    public EnemyReachedTheEndEvent(int livesLost)
    {
       this.livesLost = livesLost;
    }
}
