using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    public int currentFloor = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ¾ÀÀÌ ¹Ù²î¾îµµ À¯Áö
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextFloor()
    {
        currentFloor++;
        Debug.Log($"ÇöÀç Ãþ: {currentFloor}");
    }
}
