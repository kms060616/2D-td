using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public enum DoorDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public Transform destinationPoint;
    public DoorDirection destinationDirection;
    public float offset = 1.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 finalPos = destinationPoint.position;

        // 도착지 방향 기준 offset 보정
        switch (destinationDirection)
        {
            case DoorDirection.Up:
                finalPos += Vector3.up * offset;     // 문 위쪽 잔디
                break;
            case DoorDirection.Down:
                finalPos += Vector3.down * offset;   // 문 아래쪽 잔디
                break;
            case DoorDirection.Left:
                finalPos += Vector3.left * offset;   // 문 왼쪽 잔디
                break;
            case DoorDirection.Right:
                finalPos += Vector3.right * offset;  // 문 오른쪽 잔디
                break;
        }

        finalPos.z = 0f;
        other.transform.position = finalPos;
    }
}
