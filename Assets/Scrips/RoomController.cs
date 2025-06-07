using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public int roomLevel = 1; // 1Ãþ, 2Ãþ, 3Ãþ...

    public List<PlantEnemy> plantEnemies = new List<PlantEnemy>();
    public bool isCleared = false;

    public void OnPlayerEnter(GameObject player)
    {
        foreach (var enemy in plantEnemies)
        {
            enemy.ApplyLevelScaling(roomLevel);
        }
    }

    public void OnPlantDied(PlantEnemy plant)
    {
        plantEnemies.Remove(plant);
        if (plantEnemies.Count == 0)
        {
            isCleared = true;
            Debug.Log("¹æ Å¬¸®¾î!");
        }
    }
}
