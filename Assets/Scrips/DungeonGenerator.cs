using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject doorPrefab;
    public int numberOfRooms = 7;
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
            GameObject currentRoom = Instantiate(roomPrefab, roomPos, Quaternion.identity);
            rooms.Add(currentRoom);

            RoomController controller = currentRoom.GetComponent<RoomController>();
            if (controller != null)
            {
                controller.roomLevel = i + 1;
                controller.SpawnRandomEnemies(i + 1); // ������ ���� �� ����
            }

            Vector3 doorOffsetUp = Vector3.up * (roomHeight / 2f - 0f);
            Vector3 doorOffsetDown = Vector3.down * (roomHeight / 2f - 6.5f);

            GameObject doorUp = null;
            GameObject doorDown = null;

            // �Ʒ� �� ���� (ù �� ����)
            if (i != 0)
            {
                doorDown = Instantiate(doorPrefab, roomPos + doorOffsetDown, Quaternion.identity, currentRoom.transform);
                doorDown.name = "DoorDown";
            }

            // �� �� ���� (������ �� ����)
            if (i != numberOfRooms - 1)
            {
                doorUp = Instantiate(doorPrefab, roomPos + doorOffsetUp, Quaternion.identity, currentRoom.transform);
                doorUp.name = "DoorUp";
            }

            // ���� (���� �� �Ʒ��� �� ���� �� ����)
            if (i > 0 && doorDown != null)
            {
                GameObject prevRoom = rooms[i - 1];
                Transform prevDoorUp = prevRoom.transform.Find("DoorUp");

                if (prevDoorUp != null)
                {
                    Debug.Log($"[�� ����] {doorDown.name} �� {prevDoorUp.name}");
                    ConnectDoors(doorDown, prevDoorUp.gameObject);
                }
                else
                {
                    Debug.LogWarning($"���� �濡�� DoorUp�� ã�� �� ����! �� index: {i - 1}");
                }
            }
        }
    }

    void ConnectDoors(GameObject doorA, GameObject doorB)
    {
        DoorTrigger triggerA = doorA.GetComponent<DoorTrigger>();
        DoorTrigger triggerB = doorB.GetComponent<DoorTrigger>();

        if (triggerA != null && triggerB != null)
        {
            triggerA.targetPosition = doorB.transform;
            triggerB.targetPosition = doorA.transform;
        }
    }
}
    
