using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopPanel; // Inspector���� �Ҵ�

    private bool isPlayerInRange = false;
    private bool isShopOpen = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            ToggleShop();
        }
    }

    void ToggleShop()
    {
        isShopOpen = !isShopOpen;
        shopPanel.SetActive(isShopOpen);
        Time.timeScale = isShopOpen ? 0f : 1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            // ������ �ڵ����� �ݱ� (���� ����)
            if (isShopOpen)
            {
                ToggleShop();
            }
        }
    }
}
