using UnityEngine;

public readonly struct LivesChangedEvent
{
    public int currentLives { get; }
    public int livesLost { get; }
    public LivesChangedEvent(int currentLives, int livesLost)
    {
        this.currentLives = currentLives;
        this.livesLost = livesLost;
    }
}
