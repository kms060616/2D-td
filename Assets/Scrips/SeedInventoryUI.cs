using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedInventoryUI : MonoBehaviour
{
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
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (string seed in SeedInventory.Instance.data.collectedSeeds)
        {
            GameObject slot = Instantiate(seedSlotPrefab, contentParent);

            // 이미지 설정만 남기고 텍스트 제거
            Image iconImage = slot.transform.Find("IconImage").GetComponent<Image>();

            // seedType에 따라 스프라이트 바꾸기
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
    }
}
