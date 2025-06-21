using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public int numberOfRooms = 5;
    public float roomHeight = 20f;

    void Start()
    {
        GenerateRooms();
    }

    void GenerateRooms()
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            Vector2 position = new Vector2(0, i * roomHeight); // ���� ���� �� ����
            GameObject newRoom = Instantiate(roomPrefab, position, Quaternion.identity);

            RoomController controller = newRoom.GetComponent<RoomController>();
            if (controller != null)
            {
                controller.roomLevel = i + 1;
                controller.SpawnRandomEnemies(); // ������ ���ÿ� ���� ��ȯ
            }
        }
    }
}
