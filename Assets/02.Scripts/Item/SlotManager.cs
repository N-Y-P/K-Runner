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
        // 1) ���� ��ȿ��
        if (index < 1 || index > slots.Count)
        {
            Debug.Log("���� �������� ����");
            return null;
        }

        var slot = slots[index - 1];

        // 2) ��Ÿ�� ������
        if (!slot.isAvailable)
        {
            Debug.Log("���� ��Ÿ�� ���Դϴ�");
            return null;
        }

        // 3) UI ��ٿ� ����
        StartCoroutine(slot.CooldownRoutine());

        // 4) ���� ������ ��ȯ
        return slot.Data;
    }
}
