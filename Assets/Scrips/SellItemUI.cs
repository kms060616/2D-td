using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItemUI : MonoBehaviour
{
    public TextMeshProUGUI seedNameText;
    public TextMeshProUGUI sellPriceText;
    public Button sellButton;

    private string seedType;
    private int sellPrice;

    private int price;

    private ShopManager manager;

    public void Setup(string seedName, int price, ShopManager mgr)
    {
        seedType = seedName;
        sellPrice = price;

        seedNameText.text = seedName;
        sellPriceText.text = $"{price}¡Ú";
        this.price = price;

        sellButton.onClick.RemoveAllListeners();
        sellButton.onClick.AddListener(Sell);
        manager = mgr;
    }

    private void Sell()
    {
        SeedInventory.Instance.SellSeed(seedType, sellPrice);
    manager.UpdateStarDustUI();
    Destroy(gameObject);
    }

    void SellSeed()
    {
        if (SeedInventory.Instance.data.collectedSeeds.Contains(seedType))
        {
            SeedInventory.Instance.data.collectedSeeds.Remove(seedType);
            SeedInventory.Instance.data.starDust += price;
            SeedInventory.Instance.SaveInventory();

            UnityEngine.Debug.Log($"[ÆÇ¸Å] {seedType} ¡æ {price} º°°¡·ç È¹µæ");
            manager.GenerateSeedSellSlots();
            manager.UpdateStarDustUI();
        }
    }
}
