using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    //Canvas의 StaminaBar에 들어갈 스크립트
    //플레이어의 현재 스태미나를 반영해 fillamount를 조절합니다
    public Image fillImage;
    private PlayerStat playerStat;
    private void OnEnable()
    {
        // 씬이 로드될 때마다 새로 바인딩
        SceneManager.sceneLoaded += OnSceneLoaded;
        BindPlayerStat();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (playerStat != null)
            playerStat.OnStaminaChanged -= UpdateFill;
    }

    // 씬 로드 콜백
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindPlayerStat();
    }

    // Find로 PlayerStat 찾아서 이벤트 재구독
    private void BindPlayerStat()
    {
        // 이전 구독 해제
        if (playerStat != null)
            playerStat.OnStaminaChanged -= UpdateFill;

        // 새로 찾기
        playerStat = FindObjectOfType<PlayerStat>();
        if (playerStat != null)
        {
            playerStat.OnStaminaChanged += UpdateFill;
            // 바로 UI 갱신
            UpdateFill(playerStat.curStamina, playerStat.maxStamina);
        }
        else
        {
            Debug.LogWarning("씬에 PlayerStat이 없습니다!");
        }
    }
    private void UpdateFill(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}
