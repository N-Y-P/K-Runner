using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    //Canvas�� StaminaBar�� �� ��ũ��Ʈ
    //�÷��̾��� ���� ���¹̳��� �ݿ��� fillamount�� �����մϴ�
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
