using UnityEngine;

public static class TowerFlyWeightPointer
{
    public static readonly TowerFlyWeight[] Archer = new TowerFlyWeight[] {
    //Level 1
    new TowerFlyWeight
    {
        range = 4,
        fireInterval = 0.5f,
        bulletSpeed = 5,
        damage = 1,
        cost = 70,
        bulletType = BulletTypes.Arrow,
        towerType = TowerTypes.Archer
    },
    new TowerFlyWeight
    {
        range = 4.5f,
        fireInterval = 0.5f,
        bulletSpeed = 5,
        damage = 1.5f,
        cost = 110,
        bulletType = BulletTypes.Arrow,
        towerType = TowerTypes.Archer
    },new TowerFlyWeight
    {
        range = 4.5f,
        fireInterval = 0.45f,
        bulletSpeed = 5,
        damage = 2,
        cost = 150,
        bulletType = BulletTypes.Arrow,
        towerType = TowerTypes.Archer
    },new TowerFlyWeight
    {
        range = 5,
        fireInterval = 0.45f,
        bulletSpeed = 5,
        damage = 3,
        cost = int.MaxValue,
        bulletType = BulletTypes.Arrow,
        towerType = TowerTypes.Archer
    }
    };
    public static readonly TowerFlyWeight[] Mage = new TowerFlyWeight[] {
    new TowerFlyWeight{
        range = 5,
        fireInterval = 1,
        bulletSpeed = 8,
        damage = 2,
        cost = 100,
        bulletType = BulletTypes.MagicMissile,
        towerType = TowerTypes.Mage
    },new TowerFlyWeight{
        range = 5,
        fireInterval = 1,
        bulletSpeed = 8,
        damage = 2.75f,
        cost = 150,
        bulletType = BulletTypes.MagicMissile,
        towerType = TowerTypes.Mage
    },new TowerFlyWeight{
        range = 5,
        fireInterval = 1,
        bulletSpeed = 8,
        damage = 3.25f,
        cost = 180,
        bulletType = BulletTypes.MagicMissile,
        towerType = TowerTypes.Mage
    },new TowerFlyWeight{
        range = 5,
        fireInterval = 1,
        bulletSpeed = 8,
        damage = 4.75f,
        cost = int.MaxValue,
        bulletType = BulletTypes.MagicMissile,
        towerType = TowerTypes.Mage
    },
    };
    public static readonly TowerFlyWeight[] AoE = new TowerFlyWeight[] {
    new TowerFlyWeight{
        range = 6,
        fireInterval = 3,
        bulletSpeed = 2,
        damage = 4,
        cost = 120,
        bulletType = BulletTypes.Catapult,
        towerType = TowerTypes.Aoe
    },new TowerFlyWeight{
        range = 6,
        fireInterval = 3,
        bulletSpeed = 2,
        damage = 4.5f,
        cost = 180,
        bulletType = BulletTypes.Catapult,
        towerType = TowerTypes.Aoe
    },new TowerFlyWeight{
        range = 6,
        fireInterval = 3,
        bulletSpeed = 2,
        damage = 6,
        cost = 220,
        bulletType = BulletTypes.Catapult,
        towerType = TowerTypes.Aoe
    },new TowerFlyWeight{
        range = 6,
        fireInterval = 3,
        bulletSpeed = 2,
        damage = 8,
        cost = int.MaxValue,
        bulletType = BulletTypes.Catapult,
        towerType = TowerTypes.Aoe
    },
    };
    public static TowerFlyWeight Get(TowerTypes type, int level)
    {
        switch(type)
        {
            case TowerTypes.Archer: return Archer[level];
            case TowerTypes.Mage: return Mage[level];
            case TowerTypes.Aoe: return AoE[level];
            default: return Archer[level];
        }
    }

}
