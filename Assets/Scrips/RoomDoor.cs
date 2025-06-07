using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public Transform destinationPoint; // 이동할 위치

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = destinationPoint.position;
            Debug.Log("방 이동!");
        }
    }
}
