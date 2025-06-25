using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedInventoryUI : MonoBehaviour
{
    [Header("아이템 전용 스프라이트")]
    public Sprite thornItemSprite;

    public GameObject inventoryPanel;       // 열고 닫을 패널
    public Transform contentParent;         // SeedSlot을 넣을 부모
    public GameObject seedSlotPrefab;       // 씨앗 하나 보여주는 프리팹

    public Sprite cactusSprite;
    public Sprite mushroomSprite;
    public Sprite flowerSprite;
    public Sprite defaultSeedSprite;

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
            RefreshInventoryUI();
    }

    void RefreshInventoryUI()
    {
        // 기존 슬롯들 제거
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 씨앗 슬롯 생성
        foreach (string seed in SeedInventory.Instance.data.collectedSeeds)
        {
            GameObject slot = Instantiate(seedSlotPrefab, contentParent);
            Image iconImage = slot.transform.Find("IconImage").GetComponent<Image>();

            switch (seed)
            {
                case "선인장":
                    iconImage.sprite = cactusSprite;
                    break;
                case "버섯":
                    iconImage.sprite = mushroomSprite;
                    break;
                case "꽃":
                    iconImage.sprite = flowerSprite;
                    break;
                default:
                    iconImage.sprite = defaultSeedSprite;
                    break;
            }
        }

        // 아이템 슬롯 생성
        foreach (string item in SeedInventory.Instance.data.acquiredItems)
        {
            GameObject slot = Instantiate(seedSlotPrefab, contentParent);
            Image iconImage = slot.transform.Find("IconImage").GetComponent<Image>();
            TextMeshProUGUI nameText = slot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

            // 아이템 이름 텍스트 표시
            nameText.text = item;

            // 예시: 아이템 이름에 따라 이미지 다르게 하기
            switch (item)
            {
                case "가시":
                    iconImage.sprite = thornItemSprite;
                    break;
                default:
                    iconImage.sprite = defaultSeedSprite;
                    break;
            }
        }
    }
}
