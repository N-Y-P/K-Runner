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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddItem(ItemData data)
    {
        var go = Instantiate(itemSlotPrefab, slotsParent);
        var slot = go.GetComponent<ItemSlot>();
        slot.Setup(data, slots.Count + 1);
        slots.Add(slot);
    }
    public ItemData UseSlot(int index)
    {
        // 1) 슬롯 유효성
        if (index < 1 || index > slots.Count)
        {
            Debug.Log("아직 아이템이 없음");
            return null;
        }

        var slot = slots[index - 1];

        // 2) 쿨타임 중인지
        if (!slot.isAvailable)
        {
            Debug.Log("아직 쿨타임 중입니다");
            return null;
        }

        // 3) UI 쿨다운 시작
        StartCoroutine(slot.CooldownRoutine());

        // 4) 실제 데이터 반환
        return slot.Data;
    }
}
