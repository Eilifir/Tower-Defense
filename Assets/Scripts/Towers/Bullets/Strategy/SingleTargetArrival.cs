using UnityEngine;

public class SingleTargetArrival :IArrivalBehavior
{
    private readonly float radius;
    public SingleTargetArrival(float radius)
    {
        this.radius = radius;
    }
    public void OnArrival(Vector2 landingPoint, DamageInfo damage, LayerMask enemyLayer)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(landingPoint, radius, enemyLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<IDamageable>(out IDamageable enemy))
            { 
                enemy.TakeDamage(damage);
                break;
            }
        }
    }
}
