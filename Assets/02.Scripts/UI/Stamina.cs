using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    //Canvas�� StaminaBar�� �� ��ũ��Ʈ
    //�÷��̾��� ���� ���¹̳��� �ݿ��� fillamount�� �����մϴ�

    public Image fillImage;
    private PlayerStat playerStat;
    private void OnEnable()
    {
        // �� �ε�� ������ ����ε�
        SceneManager.sceneLoaded += OnSceneLoaded;
        BindAndSubscribe();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (playerStat != null)
            playerStat.OnStaminaChanged -= UpdateFill;
    }

    // �� �ε�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindAndSubscribe();
    }

    // PlayerStat ����ε�
    private void BindAndSubscribe()
    {
        if (playerStat != null)
            playerStat.OnStaminaChanged -= UpdateFill;

        // PlayerAutoBinder���� �ֽ� PlayerStat ��������(�� ���� ���� �� �ش� ���� �ִ� PlayerStat ���������ϱ� ����)
        playerStat = PlayerAutoBinder.Instance.PlayerStat;
        if (playerStat != null)
        {
            playerStat.OnStaminaChanged += UpdateFill;
            // UI �ʱⰪ ����
            UpdateFill(playerStat.curStamina, playerStat.maxStamina);
        }
    }
    private void UpdateFill(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}
