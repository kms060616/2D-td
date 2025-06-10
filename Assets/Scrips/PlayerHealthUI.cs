using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image fillImage; // HP_Fill 이미지 연결
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (player != null && fillImage != null)
        {
            float ratio = (float)player.CurrentHP / player.maxHP;
            fillImage.fillAmount = ratio;
        }
    }
}
