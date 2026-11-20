using UnityEngine;

public class BuildSite : MonoBehaviour
{
    public bool isOccupied { get; private set; } = false;
    TowerTargetingSystem tower;
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void OccupySpot(TowerTargetingSystem tower)
    {
        isOccupied = true;
        this.tower = tower;
        sr.enabled = false;
    }
    public void FreeTheSpot()
    {
        isOccupied = false ;
        sr.enabled = true;
    }
    public TowerTargetingSystem GetTower()
        { return tower; }
}
