using UnityEngine;

public static class BulletFlyWeightPointer
{
    public static readonly BulletFlyWeight Arrow = new BulletFlyWeight()
    {
        arrivalBehavior = new SingleTargetArrival(0.5f),
        damageType = DamageType.Normal
    };
    public static readonly BulletFlyWeight Catapult = new BulletFlyWeight()
    {
        arrivalBehavior = new AreaArrival(2f),
        damageType = DamageType.Normal
    };
    public static readonly BulletFlyWeight MagicMissile = new BulletFlyWeight()
    {
        arrivalBehavior = new SingleTargetArrival(0.5f),
        damageType = DamageType.Arcane
    };
    public static BulletFlyWeight Get(BulletTypes type)
    {
        switch(type)
        {
            case BulletTypes.Arrow: return Arrow;
            case BulletTypes.Catapult: return Catapult;
            case BulletTypes.MagicMissile: return MagicMissile;
            default: return Arrow;
        }
    }
}

public enum BulletTypes
{
    Arrow,
    Catapult,
    MagicMissile
}
