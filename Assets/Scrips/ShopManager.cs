using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopSlotPrefab;           // ShopSlot ������
    public Transform slotParent;                // ������ �� �θ� ������Ʈ (�� ������Ʈ)
    public TextMeshProUGUI starDustText;        // ���� ������ ǥ��

    public GameObject shopPanel;

    public GameObject sellSlotPrefab;
    public Transform sellSlotParent;
    public int sellPricePerSeed = 1;

    public GameObject buySection; 
    public GameObject sellSection;
    public Player player;

    private bool shopInitialized = false;


    [System.Serializable]
    public class ShopItemData
    {
        public string itemName;
        public int price;
        public Sprite icon;
    }

    public List<ShopItemData> itemPool = new List<ShopItemData>(); // �Ǹ� �ĺ� ���
    public int slotCount = 3;

    void Start()
    {
        UpdateStarDustUI();
        GenerateShop();
    }

    void GenerateShop()
    {
        if (shopInitialized) return; // �̹� ���������� ����
        shopInitialized = true;

        for (int i = 0; i < slotCount; i++)
        {
            var item = itemPool[i]; // ���� ���� ������ ������
            GameObject slotObj = Instantiate(shopSlotPrefab, slotParent);
            ShopItemUI ui = slotObj.GetComponent<ShopItemUI>();
            ui.Setup(item.itemName, item.price, item.icon, item, this);

            ui.buyButton.onClick.AddListener(UpdateStarDustUI);
        }
    }

    public void ShowBuyPanel()
    {
        buySection.SetActive(true);
        sellSection.SetActive(false);
        GenerateShop();
    }

    // �Ǹ� �� ǥ��
    public void ShowSellPanel()
    {
        buySection.SetActive(false);
        sellSection.SetActive(true);
        GenerateSeedSellSlots();
    }

    public void UpdateStarDustUI()
    {
        starDustText.text = $"������: {SeedInventory.Instance.starDust}";
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;

        
        if (player != null)
        {
            player.enabled = true;
        }

        Debug.Log("���� ����!");
    }

    public void AttemptPurchase(ShopItemData itemData)
    {
        if (SeedInventory.Instance.starDust >= itemData.price)
        {
            SeedInventory.Instance.starDust -= itemData.price;

            
            SeedInventory.Instance.AcquireItem(itemData.itemName);

            Debug.Log($"[����] {itemData.itemName} ���� ����!");
        }
        else
        {
            Debug.Log("[����] ������ ����!");
        }
    }

    public void GenerateSeedSellSlots()
    {
        foreach (Transform child in sellSlotParent)
            Destroy(child.gameObject);

        foreach (var seed in SeedInventory.Instance.data.collectedSeeds)
        {
            GameObject slot = Instantiate(sellSlotPrefab, sellSlotParent);
            var ui = slot.GetComponent<SellItemUI>();
            ui.Setup(seed, sellPricePerSeed, this);
        }
    }
}
