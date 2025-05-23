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
        // 씬 로드될 때마다 재바인딩
        SceneManager.sceneLoaded += OnSceneLoaded;
        BindAndSubscribe();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (playerStat != null)
            playerStat.OnStaminaChanged -= UpdateFill;
    }

    // 씬 로드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindAndSubscribe();
    }

    // PlayerStat 재바인딩
    private void BindAndSubscribe()
    {
        if (playerStat != null)
            playerStat.OnStaminaChanged -= UpdateFill;

        // PlayerAutoBinder에서 최신 PlayerStat 가져오기(씬 변경 됐을 때 해당 씬에 있는 PlayerStat 가져오게하기 위함)
        playerStat = PlayerAutoBinder.Instance.PlayerStat;
        if (playerStat != null)
        {
            playerStat.OnStaminaChanged += UpdateFill;
            // UI 초기값 갱신
            UpdateFill(playerStat.curStamina, playerStat.maxStamina);
        }
    }
    private void UpdateFill(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}
