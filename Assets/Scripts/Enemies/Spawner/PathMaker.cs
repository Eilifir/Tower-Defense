using System.Collections.Generic;
using UnityEngine;

public class PathMaker : MonoBehaviour
{
    [SerializeField] GameObject pathFather;
    static List<Vector2> points = new();
    public List<Vector2> pointsList => points;
    private void Awake()
    {
        if (pathFather == null)
        {
            Debug.LogWarning("There was no gameobject assigned to make the path");
            return;
        }
        for (int i = 0; i < pathFather.transform.childCount; i++)
        {
            points.Add(pathFather.transform.GetChild(i).position);
        }

    }

}
