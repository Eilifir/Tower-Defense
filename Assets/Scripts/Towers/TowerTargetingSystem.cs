using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TowerTargetingSystem : MonoBehaviour
{
    List<ITarget> enemiesInRange = new List<ITarget>();
    ITarget targetEnemy;
    Vector2 aimPoint;
    bool canShoot = true;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] BulletFactory bulletFactory;
    TowerFlyWeight towerInfo;
    [SerializeField] Sprite[] sprites;
    SpriteRenderer sr;

    public int level { get; private set; } = 0;
    public int maxLevel { get; private set; } = 3;
    public BuildSite buildSpot;

    private void OnEnable()
    {
        canShoot = true;
        sr= GetComponent<SpriteRenderer>();
    }
    public void Initialize(TowerFlyWeight data, BulletFactory factory)
    {
        towerInfo = data;
        bulletFactory = factory;
    }
    void Update()
    {
        FindEnemiesInRange(towerInfo.range);
        FindNearestEnemy();
        if(targetEnemy == null) return;
        if (canShoot)
        { 
            if(TryAim(targetEnemy, out aimPoint))
            {
                StartCoroutine(Cooldown());
                Fire();
            }
        }
    }

    void FindEnemiesInRange(float range)
    {
        enemiesInRange.Clear();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        foreach (Collider2D enemy in hits)
        {
            if (enemy.GetComponent<ITarget>() == null || enemy.GetComponent<ITarget>().IsValid() == false) continue;
            enemiesInRange.Add(enemy.GetComponent<ITarget>());
        }
    }

    void FindNearestEnemy()
    {
        if(enemiesInRange.Count <= 0) { targetEnemy = null; return; }
        ITarget closestTarget = null;
        float distance = float.MaxValue;
        foreach (ITarget enemy in enemiesInRange)
        {
            float enemyDistanceToCheck = Vector2.Distance(transform.position, enemy.Position);
            if (distance > enemyDistanceToCheck)
            {
                closestTarget = enemy;
                distance = enemyDistanceToCheck;
            }
        } 
        targetEnemy = closestTarget;
    }

    bool TryAim(ITarget target, out Vector2 aimPoint)
    {
        aimPoint = default;
        if (towerInfo.bulletSpeed <= 0) return false;
        //Classic phyiscs time = distance/speed
        float distanceTowerToTarget = Vector2.Distance(transform.position, target.Position);
        float timeForTheBulletToReachEnemy = distanceTowerToTarget / towerInfo.bulletSpeed;
        //tunnning the prediction
        for(int i = 0; i < 5; i++)
        {
            if (!target.TryPredictFuturePosition(timeForTheBulletToReachEnemy, out Vector2 predictedPos)) return false;
            distanceTowerToTarget = Vector2.Distance(transform.position, predictedPos);
            timeForTheBulletToReachEnemy = distanceTowerToTarget/towerInfo.bulletSpeed;
            aimPoint = predictedPos;
        }
        if(Vector2.Distance(transform.position, aimPoint) > towerInfo.range) return false;
        return true;
    }
    void Fire()
    {
        var singleOrAreaDamage = BulletFlyWeightPointer.Get(towerInfo.bulletType);
        var bullet = bulletFactory.CreateTyped(transform.position, Quaternion.identity);
        bullet.Init(aimPoint, towerInfo.bulletSpeed, towerInfo.damage, singleOrAreaDamage);
    }
    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(towerInfo.fireInterval);
        canShoot = true;
    }
    public void UpgradeTower()
    {
        if(level > maxLevel) return;
        level++;
        sr.sprite = sprites[level];
        TowerFlyWeight data = TowerFlyWeightPointer.Get(towerInfo.towerType, level);
        Initialize(data, bulletFactory);
    }
    public TowerTypes TowerType { get { return towerInfo.towerType; } }
}
