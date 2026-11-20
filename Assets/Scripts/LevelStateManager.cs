using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    int enemiesAlive = 0;
    bool didAllWavesSpawn = false;
    int lives = int.MaxValue;
    private void OnEnable()
    {
        EventManager<GameEvents>.Subscribe<EnemySpawnedEvent>(GameEvents.enemySpawned, EnemySpawned);
        EventManager<GameEvents>.Subscribe<AllWavesHaveSpawnedEvent>(GameEvents.allWavesHaveSpawned, AllWavesHaveSpawned);
        EventManager<GameEvents>.Subscribe<LivesChangedEvent>(GameEvents.livesChanged, Lives);
        EventManager<GameEvents>.Subscribe<EnemyDeathEvent>(GameEvents.enemyDeath, EnemiesDied);
        EventManager<GameEvents>.Subscribe<EnemyReachedTheEndEvent>(GameEvents.enemyReachedTheEnd, EnemiesReachedTheEnd);
    }
    private void OnDisable()
    {
        EventManager<GameEvents>.Unsubscribe<EnemySpawnedEvent>(GameEvents.enemySpawned, EnemySpawned);
        EventManager<GameEvents>.Unsubscribe<AllWavesHaveSpawnedEvent>(GameEvents.allWavesHaveSpawned, AllWavesHaveSpawned);
        EventManager<GameEvents>.Unsubscribe<LivesChangedEvent>(GameEvents.livesChanged, Lives);
        EventManager<GameEvents>.Unsubscribe<EnemyDeathEvent>(GameEvents.enemyDeath, EnemiesDied);
        EventManager<GameEvents>.Unsubscribe<EnemyReachedTheEndEvent>(GameEvents.enemyReachedTheEnd, EnemiesReachedTheEnd);
    }

    void EnemySpawned(EnemySpawnedEvent data)
    {
        enemiesAlive++;
    }
    void EnemiesDied(EnemyDeathEvent data )
    {
        enemiesAlive--;
        CheckConditions();
    }
    void EnemiesReachedTheEnd(EnemyReachedTheEndEvent data)
    {
        enemiesAlive--;
        CheckConditions();
    }
    void AllWavesHaveSpawned(AllWavesHaveSpawnedEvent data)
    {
        didAllWavesSpawn = true;
    }
    void Lives(LivesChangedEvent data)
    {
        lives = data.currentLives;
        CheckConditions();
    }
    void CheckConditions()
    {
        if (didAllWavesSpawn && enemiesAlive <= 0) Win();
        if (lives <= 0) Lose();
    }
    void Win()
    {
        EventManager<GameEvents>.Publish(GameEvents.levelWon, new LevelWonEvent());
        Debug.Log("YOU WIN");
    }
    void Lose()
    {
        EventManager<GameEvents>.Publish(GameEvents.levelLost, new LevelLostEvent());
        Debug.Log("YOU LOSE");
    }
}
