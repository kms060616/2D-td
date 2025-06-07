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

        // ������ ���� ���� offset ����
        switch (destinationDirection)
        {
            case DoorDirection.Up:
                finalPos += Vector3.up * offset;     // �� ���� �ܵ�
                break;
            case DoorDirection.Down:
                finalPos += Vector3.down * offset;   // �� �Ʒ��� �ܵ�
                break;
            case DoorDirection.Left:
                finalPos += Vector3.left * offset;   // �� ���� �ܵ�
                break;
            case DoorDirection.Right:
                finalPos += Vector3.right * offset;  // �� ������ �ܵ�
                break;
        }

        finalPos.z = 0f;
        other.transform.position = finalPos;
    }
}
