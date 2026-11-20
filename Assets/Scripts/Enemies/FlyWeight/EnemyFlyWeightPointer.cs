using UnityEngine;

public static class EnemyFlyWeightPointer
{
    public static readonly EnemyFlyWeight Fodder = new EnemyFlyWeight()
    {
        speed = 3,
        maxHealth = 3,
        armorType = ArmorType.NoArmor,
        gold = 10,
        livesLost = 1,
        enemyType = EnemyType.Fodder
    };

    public static readonly EnemyFlyWeight Tank = new EnemyFlyWeight()
    {
        speed = 2,
        maxHealth = 6,
        armorType = ArmorType.PhysicalArmor,
        gold = 30,
        livesLost = 3,
        enemyType = EnemyType.Tank
    };

    public static readonly EnemyFlyWeight Fast = new EnemyFlyWeight()
    {
        speed = 5,
        maxHealth = 2,
        armorType = ArmorType.NoArmor,
        gold = 15,
        livesLost = 1,
        enemyType = EnemyType.Fast
    };
    public static readonly EnemyFlyWeight AntiMage = new EnemyFlyWeight()
    {
        speed = 3,
        maxHealth = 3,
        armorType = ArmorType.MagicalArmor,
        gold = 20,
        livesLost = 2,
        enemyType = EnemyType.AntiMage
    };
    public static EnemyFlyWeight Get(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Fodder: return Fodder;
            case EnemyType.Tank: return Tank;
            case EnemyType.Fast: return Fast;
            case EnemyType.AntiMage: return AntiMage;
            default: return Fodder;
        }
    }
}
