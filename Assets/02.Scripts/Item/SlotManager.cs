using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    //�������� ȹ���ϸ� ���Կ� �������������� �߰��ϴ� ��ũ��Ʈ

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
        //�ε����� 1���� �۰ų� ���� ���� �������� ũ��
        if (index < 1 || index > slots.Count)
        {
            return null;
        }

        var slot = slots[index - 1];

        //������ ��밡���� ���°� �ƴ϶��(��Ÿ�� ���̶��)
        if (!slot.isAvailable)
        {
            return null;
        }

        //��ٿ� ����
        StartCoroutine(slot.CooldownRoutine());

        return slot.Data;
    }
}
