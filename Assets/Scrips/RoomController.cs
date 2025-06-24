using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public int roomLevel = 1;
    
    public GameObject[] possibleEnemies; // �پ��� ���� ������ �迭

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
            Debug.Log("�� Ŭ����!");
        }
    }
    public void SpawnRandomEnemies(int level)
    {
        hasSpawned = true;
        int monsterCount = Mathf.Min(level + 1, spawnPoints.Length); // ��: 1��=2����, 3��=4����...

        List<Transform> spawnPool = new List<Transform>(spawnPoints);
        for (int i = 0; i < monsterCount; i++)
        {
            // ���� ��ġ ���� & ����
            int randIndex = Random.Range(0, spawnPool.Count);
            Transform point = spawnPool[randIndex];
            spawnPool.RemoveAt(randIndex);

            GameObject prefab = possibleEnemies[Random.Range(0, possibleEnemies.Length)];
            GameObject enemy = Instantiate(prefab, point.position, Quaternion.identity, transform);

            PlantEnemy plant = enemy.GetComponent<PlantEnemy>();
            if (plant != null)
            {
                plant.ApplyLevelScaling(level);     // ü�� �����ϸ�!
                plantEnemies.Add(plant);
            }
        }
    }


}
