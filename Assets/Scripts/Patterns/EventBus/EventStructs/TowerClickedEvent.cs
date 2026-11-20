using UnityEngine;

public readonly struct TowerClickedEvent
{
    public Vector2 position { get; }
    public TowerTargetingSystem tower { get; }
    public TowerClickedEvent(Vector2 pos, TowerTargetingSystem tower)
    {
        position = pos;
        this.tower = tower;
    }
}
