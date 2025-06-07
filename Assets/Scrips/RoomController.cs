using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public List<PlantEnemy> plantEnemies = new List<PlantEnemy>();
    public bool isCleared = false;

    public void OnPlayerEnter(GameObject player)
    {
        Debug.Log("Player entered room.");
        // 디버프 적용 같은 것도 여기서 가능
    }

    public void OnPlantDied(PlantEnemy plant)
    {
        plantEnemies.Remove(plant);
        if (plantEnemies.Count == 0)
        {
            isCleared = true;
            Debug.Log("Room Cleared!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
