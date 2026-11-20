using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveController : MonoBehaviour
{
    [SerializeField] WaveData waveData;
    [SerializeField] Spawner spawner;
    int wavesSpawned = 0;
    bool isSpawning = false;
    bool earlySpawn = false;
    bool stopUntilPlayerStarts = false;
    private void OnEnable()
    {
        EventManager<GameEvents>.Subscribe<LevelLostEvent>(GameEvents.levelLost, LostLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (waveData == null || spawner == null) return;
        if(isSpawning == false && stopUntilPlayerStarts && wavesSpawned < waveData.Waves.Count -1)
        {
            StartCoroutine(SpawnWave());
        }
        if(Keyboard.current.spaceKey.wasPressedThisFrame && !earlySpawn)
        {
            stopUntilPlayerStarts = true;
            earlySpawn = true;
        }
    }
    IEnumerator SpawnWave()
    {
        isSpawning = true;      
        foreach(Wave wave in waveData.Waves)
        {
            wavesSpawned++;
            foreach(SpawnGroup group in wave.groups)
            {
                for (int i = 0; i < group.count; i++)
                {
                    spawner.Spawn(group.EnemyType);
                    yield return new WaitForSeconds(group.spawnInterval);
                }
                yield return null;
            }
            earlySpawn = false;
            float t = 0;
            while(t < wave.delayAfterWave && earlySpawn == false && wavesSpawned < waveData.Waves.Count)
            {
                t += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
        isSpawning= false;
        EventManager<GameEvents>.Publish(GameEvents.allWavesHaveSpawned, new AllWavesHaveSpawnedEvent());
    }
    void LostLevel(LevelLostEvent data)
    {
        StopAllCoroutines();
    }
}
