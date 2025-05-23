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
        //인덱스가 1보다 작거나 실제 슬롯 개수보다 크면
        if (index < 1 || index > slots.Count)
        {
            return null;
        }

        var slot = slots[index - 1];

        //아이템 사용가능한 상태가 아니라면(쿨타임 중이라면)
        if (!slot.isAvailable)
        {
            return null;
        }

        //쿨다운 시작
        StartCoroutine(slot.CooldownRoutine());

        return slot.Data;
    }
}
