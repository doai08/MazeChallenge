using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public int x;
    public int y;
    public void SetPosition(int newX, int newY)
    {
        SetCell(newX, newY);
        transform.position = new Vector3(x, y, transform.position.z);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //Position Logic
    public void SetCell(int newX, int newY)
    {
        x = newX;
        y = newY;
    }
    public Vector2Int GetCell()
    {
        return new Vector2Int(x, y);
    }
    public int GetCellX()
    {
        return x;
    }
    public int GetCellY()
    {
        return y;
    }
}
