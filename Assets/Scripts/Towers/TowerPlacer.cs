using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] PlayerResources resources;
    [SerializeField] BulletFactory bulletFactory;
    [SerializeField] List<TowerEntry> towers;

    public bool TryBuild(BuildSite spot, TowerTypes type)
    {
        if(spot.isOccupied || spot == null) return false;
        foreach(TowerEntry entry in towers)
        {
            if(entry.type == type)
            {
                if (resources.TrySpend(TowerFlyWeightPointer.Get(type, 0).cost) == false) return false;
                var tower = Instantiate(entry.prefab, spot.transform.position, Quaternion.identity);
                tower.Initialize(TowerFlyWeightPointer.Get(type, 0), bulletFactory);
                spot.OccupySpot(tower);
                tower.buildSpot = spot;
                return true;
            }
        }
        return false;
    }
    
    public bool TrySell(TowerTargetingSystem tower, TowerTypes type)
    {
        if (tower == null) return false;
        Destroy(tower.gameObject);
        TowerFlyWeight cost = TowerFlyWeightPointer.Get(type, tower.level);
        int refund = Mathf.FloorToInt(cost.cost * 0.7f);
        EnemyDeathEvent refundEvent = new EnemyDeathEvent(refund);
        resources.AddGold(refundEvent);
        tower.buildSpot.FreeTheSpot();
        return true;
    }

    public bool TryUpgrade(TowerTargetingSystem tower)
    {
        if (tower == null) return false;
        print("upgraded");
        TowerFlyWeight cost = TowerFlyWeightPointer.Get(tower.TowerType, tower.level+1);
        if(resources.TrySpend(cost.cost) == false) return false;
        tower.UpgradeTower();
        return true;
    }
}
