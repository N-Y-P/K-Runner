using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    //아이템을 획득하면 슬롯에 아이템프리팹을 추가하는 스크립트

    public static SlotManager Instance;

    [Header("Slots")]
    public Transform slotsParent;
    [Header("ItemPrefab")]
    public GameObject itemSlotPrefab;

    private Dictionary<int, ItemSlot> _slots = new Dictionary<int, ItemSlot>();
    private int slotIndex = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void AddItem(ItemData data)
    {
        var go = Instantiate(itemSlotPrefab, slotsParent);
        var slot = go.GetComponent<ItemSlot>();
        slot.Setup(data, slotIndex++);
    }
    public void UseSlot(int index)
    {

    }
}
