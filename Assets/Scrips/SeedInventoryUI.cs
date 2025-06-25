using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedInventoryUI : MonoBehaviour
{
    [Header("������ ���� ��������Ʈ")]
    public Sprite thornItemSprite;

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
        // ���� ���Ե� ����
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // ���� ���� ����
        foreach (string seed in SeedInventory.Instance.data.collectedSeeds)
        {
            GameObject slot = Instantiate(seedSlotPrefab, contentParent);
            Image iconImage = slot.transform.Find("IconImage").GetComponent<Image>();

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

        // ������ ���� ����
        foreach (string item in SeedInventory.Instance.data.acquiredItems)
        {
            GameObject slot = Instantiate(seedSlotPrefab, contentParent);
            Image iconImage = slot.transform.Find("IconImage").GetComponent<Image>();
            TextMeshProUGUI nameText = slot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

            // ������ �̸� �ؽ�Ʈ ǥ��
            nameText.text = item;

            // ����: ������ �̸��� ���� �̹��� �ٸ��� �ϱ�
            switch (item)
            {
                case "����":
                    iconImage.sprite = thornItemSprite;
                    break;
                default:
                    iconImage.sprite = defaultSeedSprite;
                    break;
            }
        }
    }
}
