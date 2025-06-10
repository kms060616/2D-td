using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedInventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;       // ���� ���� �г�
    public Transform contentParent;         // SeedSlot�� ���� �θ�
    public GameObject seedSlotPrefab;       // ���� �ϳ� �����ִ� ������

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

            // �̹��� ������ ����� �ؽ�Ʈ ����
            Image iconImage = slot.transform.Find("IconImage").GetComponent<Image>();

            // seedType�� ���� ��������Ʈ �ٲٱ�
            switch (seed)
            {
                case "������":
                    iconImage.sprite = cactusSprite;
                    break;
                case "����":
                    iconImage.sprite = mushroomSprite;
                    break;
                case "��":
                    iconImage.sprite = flowerSprite;
                    break;
                default:
                    iconImage.sprite = defaultSeedSprite;
                    break;
            }

        }
    }
}
