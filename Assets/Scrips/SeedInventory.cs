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
            Debug.Log($"[판매] {seedType} → {sellPrice} 별가루 획득");
        }
        else
        {
            Debug.Log($"[판매 실패] {seedType} 없음");
        }
    }

    public void AcquireItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogWarning("[오류] AcquireItem에 전달된 itemName이 null 또는 빈 문자열입니다.");
            return;
        }

        Debug.Log($"[디버그] AcquireItem 호출됨: {itemName}");

        if (!data.acquiredItems.Contains(itemName))
        {
            data.acquiredItems.Add(itemName);
            SaveInventory();
            Debug.Log($"[인벤토리] 아이템 추가됨: {itemName}");
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
            Debug.Log($"[인벤토리] 씨앗 추가: {seedType}");
        }
    }
    public void BuyItem(string itemName, int price)
    {
        if (SeedInventory.Instance.starDust >= price)
        {
            SeedInventory.Instance.starDust -= price;
            SeedInventory.Instance.AcquireItem(itemName); 
            Debug.Log($"[구매] {itemName} 구매 성공!");
        }
        else
        {
            Debug.Log("[구매 실패] 별가루 부족");
        }
    }

}
