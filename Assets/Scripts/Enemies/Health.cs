using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    float currentHealth;
    [SerializeField]float maxHealth = 5.0f;
    bool triggerDeath = false;
    public event System.Action Died;
    ArmorType armorType;
    DamageArmorKey key;

    public void Init(float health, ArmorType armor)
    {
        maxHealth = health; currentHealth = health;
        armorType = armor;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        triggerDeath = false;
    }
    public void TakeDamage(DamageInfo info)
    {
        key = new DamageArmorKey(info.damageType, armorType);
        float multiplier = DamageTable.Get(key);
        currentHealth -= info.amount*multiplier;
        if(currentHealth <= 0 && triggerDeath == false)
        {
            triggerDeath = true;
            Died?.Invoke();
        }
    }

}
