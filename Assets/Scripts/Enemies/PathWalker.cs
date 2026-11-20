using System.Collections.Generic;
using UnityEngine;

public class PathWalker
{
    private List<Vector2> points;
    private int targetIndex = 0;
    public Vector2 Position { get; private set; }
    public bool HasFinished { get; private set; }

    public PathWalker(List<Vector2> points)
    {
        this.points = points;
        Position = points[0];
    }

    public void Move(float distance)
    {
        if (HasFinished) return;
        Vector2 pos = Position;
        int index = targetIndex;
        bool finished = Walk(ref pos, ref index, distance);
        Position = pos;
        targetIndex = index;
        HasFinished = finished;
    }
    public bool TryGetPositionAfter(float distance, out Vector2 position)
    {
        position = Position;
        if(HasFinished) return false;
        Vector2 pos = Position;
        int index = targetIndex;
        if (Walk(ref pos, ref index, distance)) return false;
        position = pos;
        return true;
    }

    bool Walk(ref Vector2 pos, ref int index, float distance)
    {
        float cacheDistance = distance;
        while (Vector2.Distance(pos, points[index]) < cacheDistance)
        {
            cacheDistance -= Vector2.Distance(pos, points[index]);
            pos = points[index];
            if (index == points.Count - 1)
            {
                return true;
            }
            index++;
        }
        pos = Vector2.MoveTowards(pos, points[index], cacheDistance);
        return false;
    }
}
