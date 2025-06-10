using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedItem : MonoBehaviour
{
    public string seedType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SeedInventory.Instance.CollectSeed(seedType);
            Destroy(gameObject);
        }
    }
}
