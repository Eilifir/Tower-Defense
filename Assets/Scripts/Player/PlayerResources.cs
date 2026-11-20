using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] int gold;
    [SerializeField] int lives;

    private void OnEnable()
    {
        EventManager<GameEvents>.Subscribe<EnemyDeathEvent>(GameEvents.enemyDeath, AddGold);
        EventManager<GameEvents>.Subscribe<EnemyReachedTheEndEvent>(GameEvents.enemyReachedTheEnd, RemoveLives);
    }
    private void OnDisable()
    {
        EventManager<GameEvents>.Unsubscribe<EnemyDeathEvent>(GameEvents.enemyDeath,AddGold);
        EventManager<GameEvents>.Unsubscribe<EnemyReachedTheEndEvent>(GameEvents.enemyReachedTheEnd, RemoveLives);
    }
    //I'll use this for when I want to start a level with different parameters
    public void SetResources(int gold, int lives)
    {
        this.gold = gold;
        this.lives = lives;
    }

    void RemoveLives(EnemyReachedTheEndEvent data)
    {
        lives = Mathf.Clamp(lives-data.livesLost,0, lives);
        EventManager<GameEvents>.Publish(GameEvents.livesChanged, new LivesChangedEvent(lives, data.livesLost));
    }
    public void AddGold(EnemyDeathEvent data)
    {
        gold += data.gold;
        EventManager<GameEvents>.Publish(GameEvents.goldChanged, new GoldChangedEvent(gold));
    }

    public bool TrySpend(int amount)
    {
        if (amount > gold || amount < 0) return false;
        gold -= amount;
        EventManager<GameEvents>.Publish(GameEvents.goldChanged, new GoldChangedEvent(gold));
        return true;
    }
}
