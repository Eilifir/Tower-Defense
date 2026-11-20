using UnityEngine;

public interface ITarget
{
    Vector2 Position { get; }
    public bool TryPredictFuturePosition(float t, out Vector2 predictedPosition);
    bool IsValid();
}
