using UnityEngine;

public readonly struct BuildSpotClickedEvent
{
    public Vector2 position { get; }
    public BuildSite site { get; }
    public BuildSpotClickedEvent(Vector2 pos, BuildSite site)
    {
        position = pos;
        this.site = site;
    }
}
