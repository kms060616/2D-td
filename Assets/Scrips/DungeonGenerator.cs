using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject doorPrefab;
    public int numberOfRooms = 5;
    public float roomHeight = 20f;

    private List<GameObject> rooms = new List<GameObject>();

    void Start()
    {
        GenerateRooms();
    }

    void GenerateRooms()
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            
            Vector3 roomPos = new Vector3(0, i * roomHeight, 0);
            GameObject newRoom = Instantiate(roomPrefab, roomPos, Quaternion.identity);
            rooms.Add(newRoom);

            RoomController controller = newRoom.GetComponent<RoomController>();
            if (controller != null)
            {
                controller.roomLevel = i + 1;
                controller.SpawnRandomEnemies();
            }

            
            GameObject doorUp = Instantiate(
                doorPrefab,
                roomPos + Vector3.up * (roomHeight / 2f - 1f),
                Quaternion.identity,
                newRoom.transform
            );

            
            GameObject doorDown = Instantiate(
                doorPrefab,
                roomPos + Vector3.down * (roomHeight / 2f - 1f),
                Quaternion.identity,
                newRoom.transform
            );

            
            if (i > 0)
            {
                GameObject prevRoom = rooms[i - 1];

                Transform prevDoorUp = prevRoom.transform.Find("DoorUp(Clone)");
                Transform thisDoorDown = doorDown.transform;

                // ¿¬°á
                if (prevDoorUp != null && thisDoorDown != null)
                {
                    var doorUpScript = prevDoorUp.GetComponent<DoorTrigger>();
                    var doorDownScript = thisDoorDown.GetComponent<DoorTrigger>();

                    if (doorUpScript != null && doorDownScript != null)
                    {
                        doorUpScript.targetPosition = thisDoorDown;
                        doorDownScript.targetPosition = prevDoorUp;
                    }
                }
            }
        }
    }
}
    
