using UnityEngine;
public interface IArrivalBehavior
{
    void OnArrival(Vector2 landingPoint, DamageInfo damage, LayerMask enemyLayer);
}