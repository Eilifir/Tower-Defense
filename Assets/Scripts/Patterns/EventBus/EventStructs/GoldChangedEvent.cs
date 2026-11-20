using UnityEngine;

public readonly struct GoldChangedEvent
{
    public int gold { get; }
    public GoldChangedEvent(int amount)
    {
        gold = amount;
    }
}
