using System;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour, ITarget, IFactoryProduct
{
    PathWalker walker;
    Health health;
    EnemyFlyWeight enemyInfo;
    bool hasEnded = false;
    System.Action<IFactoryProduct> _returnToPool;
    [SerializeField] EnemyVisuals visuals;

    public Vector2 Position => transform.position;
    #region Subscribe to Health
    private void Awake()
    {
        health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        health.Died += OnDied;
        hasEnded = false;
    }
    private void OnDisable()
    {
        health.Died -= OnDied;
        walker = null;
    }
    #endregion
    public void Initialize(EnemyFlyWeight data, List<Vector2> points)
    {
        enemyInfo = data;
        walker = new PathWalker(points);
        health.Init(data.maxHealth, data.armorType);
        transform.position = points[0];
        visuals.SetSprite(data.enemyType);
    }
    void Update()
    {
        if (walker == null) return;
        walker.Move(enemyInfo.speed*Time.deltaTime);
        transform.position = walker.Position;
        if (walker.HasFinished)
            ReachedTheEnd();
    }
   
    //The same for now but they'll change later
    void OnDied()
    {
        if (hasEnded) return;
        hasEnded = true;
        EventManager<GameEvents>.Publish(GameEvents.enemyDeath, new EnemyDeathEvent(enemyInfo.gold));
        _returnToPool(this);
    }
    void ReachedTheEnd()
    {
        if (hasEnded) return;
        hasEnded = true;
        EventManager<GameEvents>.Publish(GameEvents.enemyReachedTheEnd, new EnemyReachedTheEndEvent(enemyInfo.livesLost));
        _returnToPool(this);
    }

    public bool TryPredictFuturePosition(float t, out Vector2 predictedPosition)
    {
        predictedPosition = new Vector2();
        if(!IsValid()) return false;
        return walker.TryGetPositionAfter(enemyInfo.speed *t, out predictedPosition);
    }

    public bool IsValid()
    {
        return walker != null && gameObject.activeInHierarchy && !walker.HasFinished;
    }

    public void OnCreated()
    {
        //throw new NotImplementedException();
    }

    public void SetReturnToPool(Action<IFactoryProduct> returnAction)
    {
        _returnToPool = returnAction;
    }
}

