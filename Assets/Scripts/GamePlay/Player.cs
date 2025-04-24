using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int x;
    public int y;
    public int moveSpeed = PlayerConst.MOVE_SPEED;
    public int angularSpeed = PlayerConst.ROTATE_SPEED;

    //PositionWorld
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
    public void MoveAndRotate(int targetX, int targetY)
    {

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (targetPosition == transform.position) return;

        // Tính toán hướng đi theo 4 hướng chính (Up, Down, Left, Right)
        float angle = 0f;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                angle = -90f;
            else if (direction.x < 0)
                angle = 90f;
        }
        else
        {
            if (direction.y > 0)
                angle = 0f;
            else if (direction.y < 0)
                angle = 180f;
        }

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * angularSpeed);


        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        SetCell(targetX, targetY);
    }
}
