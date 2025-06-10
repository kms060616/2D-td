using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SeedInventory : MonoBehaviour
{
    public static SeedInventory Instance;

    public SeedInventoryData data = new SeedInventoryData();

    private string savePath => Path.Combine(Application.persistentDataPath, "seed_inventory.json");

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadInventory();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectSeed(string seedType)
    {
        if (!data.collectedSeeds.Contains(seedType))
        {
            data.collectedSeeds.Add(seedType);
            Debug.Log($"[�κ��丮] ���� �߰�: {seedType}");
            SaveInventory();
        }
    }

    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"[����] �κ��丮 �����: {savePath}");
    }

    public void LoadInventory()
    {
        string path = Application.persistentDataPath + "/seed_inventory.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SeedInventoryData>(json);
        }
        else
        {
            data = new SeedInventoryData(); //�̰� ������ ���� ����
        }
    }
}
