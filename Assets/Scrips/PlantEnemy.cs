using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    private RoomController room;

    void Start()
    {
        room = GetComponentInParent<RoomController>();
        if (room != null && !room.plantEnemies.Contains(this))
        {
            room.plantEnemies.Add(this);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("PlantEnemy died!");
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        room.OnPlantDied(this);
        Destroy(gameObject);
    }
}
