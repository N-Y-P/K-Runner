using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    //Canvas의 StaminaBar에 들어갈 스크립트
    //플레이어의 현재 스태미나를 반영해 fillamount를 조절합니다
    public Image fillImage;
    public PlayerStat playerStat;

    private void OnEnable()
    {
        playerStat.OnStaminaChanged += UpdateFill;
    }
    private void OnDisable()
    {
        playerStat.OnStaminaChanged -= UpdateFill;
    }
    private void UpdateFill(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}
