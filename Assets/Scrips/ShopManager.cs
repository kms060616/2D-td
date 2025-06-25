using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopSlotPrefab;           // ShopSlot 프리팹
    public Transform slotParent;                // 슬롯이 들어갈 부모 오브젝트 (빈 오브젝트)
    public TextMeshProUGUI starDustText;        // 보유 별가루 표시

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

    public List<ShopItemData> itemPool = new List<ShopItemData>(); // 판매 후보 목록
    public int slotCount = 3;

    void Start()
    {
        UpdateStarDustUI();
        GenerateShop();
    }

    void GenerateShop()
    {
        if (shopInitialized) return; // 이미 생성됐으면 종료
        shopInitialized = true;

        for (int i = 0; i < slotCount; i++)
        {
            var item = itemPool[i]; // 랜덤 말고 고정된 순서로
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

    // 판매 탭 표시
    public void ShowSellPanel()
    {
        buySection.SetActive(false);
        sellSection.SetActive(true);
        GenerateSeedSellSlots();
    }

    public void UpdateStarDustUI()
    {
        starDustText.text = $"별가루: {SeedInventory.Instance.starDust}";
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;

        
        if (player != null)
        {
            player.enabled = true;
        }

        Debug.Log("상점 닫힘!");
    }

    public void AttemptPurchase(ShopItemData itemData)
    {
        if (SeedInventory.Instance.starDust >= itemData.price)
        {
            SeedInventory.Instance.starDust -= itemData.price;

            
            SeedInventory.Instance.AcquireItem(itemData.itemName);

            Debug.Log($"[상점] {itemData.itemName} 구매 성공!");
        }
        else
        {
            Debug.Log("[상점] 별가루 부족!");
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
