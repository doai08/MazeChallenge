using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaze : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    // Hàm vẽ đường đi
    public void DrawPath(List<Vector2Int> path, Color color)
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = path.Count;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        // Vẽ các điểm
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(path[i].x, path[i].y));
        }
    }
}