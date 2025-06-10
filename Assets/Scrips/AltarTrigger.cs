using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AltarTrigger : MonoBehaviour
{
    
    [SerializeField] private string nextSceneName = "AstrogardenScene"; // ¿Ãµø«“ æ¿ ¿Ã∏ß

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SeedInventory.Instance.SaveInventory(); // æææ— ¿˙¿Â
            SceneManager.LoadScene(nextSceneName);  // ¥Ÿ¿Ω √˛¿∏∑Œ ¿Ãµø
        }
    }

}
