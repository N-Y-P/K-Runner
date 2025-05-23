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
        // ���� �ε�� ������ ���� ���ε�
        SceneManager.sceneLoaded += OnSceneLoaded;
        BindPlayerStat();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (playerStat != null)
            playerStat.OnStaminaChanged -= UpdateFill;
    }

    // �� �ε� �ݹ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindPlayerStat();
    }

    // Find�� PlayerStat ã�Ƽ� �̺�Ʈ �籸��
    private void BindPlayerStat()
    {
        // ���� ���� ����
        if (playerStat != null)
            playerStat.OnStaminaChanged -= UpdateFill;

        // ���� ã��
        playerStat = FindObjectOfType<PlayerStat>();
        if (playerStat != null)
        {
            playerStat.OnStaminaChanged += UpdateFill;
            // �ٷ� UI ����
            UpdateFill(playerStat.curStamina, playerStat.maxStamina);
        }
        else
        {
            Debug.LogWarning("���� PlayerStat�� �����ϴ�!");
        }
    }
    private void UpdateFill(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}
