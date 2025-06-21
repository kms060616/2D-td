using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public int roomLevel = 1;
    
    public GameObject[] possibleEnemies; // 다양한 몬스터 프리팹 배열

    public List<PlantEnemy> plantEnemies = new List<PlantEnemy>();
    public bool isCleared = false;
    public Transform[] spawnPoints;
    private bool hasSpawned = false;

    public void OnPlayerEnter(GameObject player)
    {
        if (!hasSpawned)
        {
            SpawnRandomEnemies();
            hasSpawned = true; 
        }

        foreach (var enemy in plantEnemies)
        {
            enemy.ApplyLevelScaling(roomLevel);
        }
    }

    public void SpawnRandomEnemies()
    {
        foreach (Transform point in spawnPoints)
        {
            GameObject prefab = possibleEnemies[Random.Range(0, possibleEnemies.Length)];
            GameObject enemy = Instantiate(prefab, point.position, Quaternion.identity, transform);

            PlantEnemy plant = enemy.GetComponent<PlantEnemy>();
            if (plant != null)
            {
                plantEnemies.Add(plant);
            }
        }
    }

    public void OnPlantDied(PlantEnemy plant)
    {
        plantEnemies.Remove(plant);
        if (plantEnemies.Count == 0)
        {
            isCleared = true;
            Debug.Log("방 클리어!");
        }
    }

    
}
