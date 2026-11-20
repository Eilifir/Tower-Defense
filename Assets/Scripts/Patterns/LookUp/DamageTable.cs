using UnityEngine;

public static class DamageTable
{
    static readonly LookUpTable<DamageArmorKey, float> table = new LookUpTable<DamageArmorKey, float>(CalculateMultiplier);
    public static float Get(DamageArmorKey key)
    {
        return table.ReturnValue(key);
    }
    static float CalculateMultiplier(DamageArmorKey key)
    {
        if (key.armor == ArmorType.NoArmor) return 1f;
        if (key.damage == DamageType.Fire || key.damage == DamageType.Frost) return 0.66f;
        if (key.damage == DamageType.Arcane && key.armor == ArmorType.MagicalArmor) return 0.33f;
        if (key.damage == DamageType.Normal && key.armor == ArmorType.PhysicalArmor) return 0.33f;
        return 1f;
    }
}

public readonly struct DamageArmorKey
{
    public readonly DamageType damage;
    public readonly ArmorType armor;
    public DamageArmorKey(DamageType damage, ArmorType armor)
    {
        this.damage = damage;
        this.armor = armor;
    }
}
