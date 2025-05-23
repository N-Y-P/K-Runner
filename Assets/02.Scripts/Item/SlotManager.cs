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

    // �÷��̾� ����
    private PlayerStat playerStat;
    private PlayerController playerCtrl;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // �� �ε� �ݹ� ���
            SceneManager.sceneLoaded += OnSceneLoaded;
            // �ʱ� ���ε� (Start ��)
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

    // ���� ���� �ε�� ������ ȣ��˴ϴ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindPlayerRefs();
    }

    // Find�� �̿��� PlayerStat, PlayerController ���Ҵ�
    private void BindPlayerRefs()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        playerCtrl = FindObjectOfType<PlayerController>();

        if (playerStat == null) Debug.LogWarning("SlotManager: PlayerStat�� ã�� ���߽��ϴ�!");
        if (playerCtrl == null) Debug.LogWarning("SlotManager: PlayerController�� ã�� ���߽��ϴ�!");
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
            Debug.Log("���� �������� ����");
            return;
        }

        var slot = slots[index - 1];
        if (!slot.isAvailable)
        {
            Debug.Log("���� ��Ÿ�� ���Դϴ�");
            return;
        }

        var data = slot.Data;

        Debug.Log($"����� ������: {data.itemName}");

        // 1) ���¹̳� ȸ��: ���� �̺�Ʈ Invoke ���� ����!
        if (data.staminaRecovery > 0f)
        {
            playerStat.RecoverStamina(data.staminaRecovery);
            Debug.Log("���¹̳� ����");
        }

        // 2) ��� ���� (������ ����)
        if (data.dash > 0f)
        {
            Debug.Log("��� ���� ����");
            StartCoroutine(DashBuffRoutine(data.duration));
        }

        // 3) ��ٿ� UI �ڷ�ƾ ����
        StartCoroutine(slot.CooldownRoutine());
    }

    private IEnumerator DashBuffRoutine(float buffDuration)
    {
        playerCtrl.dashBuffActive = true;

        yield return new WaitForSeconds(buffDuration);

        playerCtrl.dashBuffActive = false;
        Debug.Log("��� ���� ����");
    }
}
