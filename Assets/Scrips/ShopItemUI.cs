using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using static ShopManager;

[System.Serializable]

public class ShopItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;
    public Image iconImage;
    public Button buyButton;

    private string itemId;
    private int cost;

    public string itemName;
    public string seedType; // 필요한 씨앗 종류
    public int price;
    public Sprite icon;

    private ShopManager manager;
    private ShopManager.ShopItemData itemData;

    public void Setup(string name, int price, Sprite icon, ShopManager.ShopItemData data, ShopManager shopManager)
    {
        itemNameText.text = name;
        priceText.text = $"{price}★";

        Transform iconTransform = transform.Find("IconImage");
        if (iconTransform == null)
        {
            Debug.LogError("[ShopItemUI] 'IconImage' 오브젝트를 찾을 수 없습니다. 프리팹 구조를 확인하세요.");
            return;
        }

        iconImage = iconTransform.GetComponent<Image>();
        if (iconImage == null)
        {
            Debug.LogError("[ShopItemUI] 'IconImage'에 Image 컴포넌트가 없습니다.");
            return;
        }

        iconImage.sprite = icon;

        itemData = data;
        manager = shopManager;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => manager.AttemptPurchase(itemData));
    }


}
