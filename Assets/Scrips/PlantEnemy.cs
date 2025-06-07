using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    public int baseHP = 2;
    private int currentHP;

    private RoomController room;

    void Start()
    {
        room = GetComponentInParent<RoomController>();
        if (room != null && !room.plantEnemies.Contains(this))
        {
            room.plantEnemies.Add(this);
        }

        // fallback: �ּ� ü�� ����
        currentHP = baseHP;
    }

    public void ApplyLevelScaling(int level)
    {
        currentHP = baseHP + level - 1;
        Debug.Log($"{gameObject.name} ü��: {currentHP}");
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        room.OnPlantDied(this);
        Destroy(gameObject);
    }
}
