using System;
using UnityEngine;

public class Proyectile : MonoBehaviour, IFactoryProduct
{
    Vector2 target;
    float speed;
    [SerializeField] LayerMask enemyMask;
    DamageInfo damageInfo;
    IArrivalBehavior arrivalBehavior;
    System.Action<IFactoryProduct> _returnToPool;
    public void Init(Vector2 target, float speed, float damage, BulletFlyWeight data)
    {
        this.target = target;
        this.speed = speed;
        damageInfo = new DamageInfo();
        damageInfo.amount = damage;
        damageInfo.damageType = data.damageType;
        this.arrivalBehavior = data.arrivalBehavior;
    }

    public void OnCreated()
    {
        //throw new NotImplementedException();
    }

    public void SetReturnToPool(Action<IFactoryProduct> returnAction)
    {
        _returnToPool = returnAction;
    }

    void Update()
    {
        if(arrivalBehavior == null) return;
        transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, target) < 0.01f)
        {
            arrivalBehavior.OnArrival(transform.position, damageInfo, enemyMask);
            _returnToPool(this);
        }
    }
    private void OnEnable()
    {
        arrivalBehavior = null;
    }
}
