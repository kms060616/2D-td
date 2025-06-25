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

    public void SellSeed(string seedType, int sellPrice)
    {
        if (data.collectedSeeds.Contains(seedType))
        {
            data.collectedSeeds.Remove(seedType);
            data.starDust += sellPrice;

            SaveInventory();
            Debug.Log($"[�Ǹ�] {seedType} �� {sellPrice} ������ ȹ��");
        }
        else
        {
            Debug.Log($"[�Ǹ� ����] {seedType} ����");
        }
    }

    public void AcquireItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogWarning("[����] AcquireItem�� ���޵� itemName�� null �Ǵ� �� ���ڿ��Դϴ�.");
            return;
        }

        Debug.Log($"[�����] AcquireItem ȣ���: {itemName}");

        if (!data.acquiredItems.Contains(itemName))
        {
            data.acquiredItems.Add(itemName);
            SaveInventory();
            Debug.Log($"[�κ��丮] ������ �߰���: {itemName}");
        }
    }

    public bool HasItem(string itemName)
    {
        return data.acquiredItems.Contains(itemName);
    }

    public int starDust
    {
        get => data.starDust;
        set
        {
            data.starDust = value;
            SaveInventory();
        }
    }

    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadInventory()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<SeedInventoryData>(json);
        }
        else
        {
            data = new SeedInventoryData();
        }
    }

    public void CollectSeed(string seedType)
    {
        if (!data.collectedSeeds.Contains(seedType))
        {
            data.collectedSeeds.Add(seedType);
            SaveInventory();
            Debug.Log($"[�κ��丮] ���� �߰�: {seedType}");
        }
    }
    public void BuyItem(string itemName, int price)
    {
        if (SeedInventory.Instance.starDust >= price)
        {
            SeedInventory.Instance.starDust -= price;
            SeedInventory.Instance.AcquireItem(itemName); 
            Debug.Log($"[����] {itemName} ���� ����!");
        }
        else
        {
            Debug.Log("[���� ����] ������ ����");
        }
    }

}
