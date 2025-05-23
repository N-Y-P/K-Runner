using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAutoBinder : MonoBehaviour
{
    //씬 이동 되면 그 씬의 플레이어 정보를 받아오고
    //그 정보를 다른 스크립트에서 가져가 사용할 수 있게 함
    public static PlayerAutoBinder Instance { get; private set; }
    public PlayerStat PlayerStat { get; private set; }
    public PlayerController PlayerController { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BindPlayer();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        PlayerStat = FindObjectOfType<PlayerStat>();
        PlayerController = FindObjectOfType<PlayerController>();

        if (PlayerStat == null)
            Debug.LogWarning("PlayerStat가 없다");
        if (PlayerController == null)
            Debug.LogWarning("PlayerController가 없다");
    }
}
