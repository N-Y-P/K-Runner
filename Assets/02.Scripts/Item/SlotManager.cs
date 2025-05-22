using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    //�������� ȹ���ϸ� ���Կ� �������������� �߰��ϴ� ��ũ��Ʈ

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
