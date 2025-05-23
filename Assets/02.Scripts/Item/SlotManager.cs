using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    //아이템을 획득하면 슬롯에 아이템프리팹을 추가하는 스크립트

    public static SlotManager Instance;

    [Header("Slots")]
    public Transform slotsParent;
    [Header("ItemPrefab")]
    public GameObject itemSlotPrefab;


    private List<ItemSlot> slots = new List<ItemSlot>();

    // 플레이어 참조
    private PlayerStat playerStat;
    private PlayerController playerCtrl;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 씬 로드 콜백 등록
            SceneManager.sceneLoaded += OnSceneLoaded;
            // 초기 바인딩 (Start 씬)
            BindPlayerRefs();
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

    // 씬이 새로 로드될 때마다 호출됩니다
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindPlayerRefs();
    }

    // Find를 이용해 PlayerStat, PlayerController 재할당
    private void BindPlayerRefs()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        playerCtrl = FindObjectOfType<PlayerController>();

        if (playerStat == null) Debug.LogWarning("SlotManager: PlayerStat을 찾지 못했습니다!");
        if (playerCtrl == null) Debug.LogWarning("SlotManager: PlayerController를 찾지 못했습니다!");
    }

    public void AddItem(ItemData data)
    {
        var go = Instantiate(itemSlotPrefab, slotsParent);
        var slot = go.GetComponent<ItemSlot>();
        slot.Setup(data, slots.Count + 1);
        slots.Add(slot);
    }

    public void UseSlot(int index)
    {
        if (index < 1 || index > slots.Count)
        {
            Debug.Log("아직 아이템이 없음");
            return;
        }

        var slot = slots[index - 1];
        if (!slot.isAvailable)
        {
            Debug.Log("아직 쿨타임 중입니다");
            return;
        }

        var data = slot.Data;

        Debug.Log($"사용한 아이템: {data.itemName}");

        // 1) 스태미나 회복: 이제 이벤트 Invoke 오류 없이!
        if (data.staminaRecovery > 0f)
        {
            playerStat.RecoverStamina(data.staminaRecovery);
            Debug.Log("스태미나 증가");
        }

        // 2) 대시 버프 (이전과 동일)
        if (data.dash > 0f)
        {
            Debug.Log("대시 버프 시작");
            StartCoroutine(DashBuffRoutine(data.duration));
        }

        // 3) 쿨다운 UI 코루틴 실행
        StartCoroutine(slot.CooldownRoutine());
    }

    private IEnumerator DashBuffRoutine(float buffDuration)
    {
        playerCtrl.dashBuffActive = true;

        yield return new WaitForSeconds(buffDuration);

        playerCtrl.dashBuffActive = false;
        Debug.Log("대시 버프 종료");
    }
}
