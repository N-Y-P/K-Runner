using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    //������ ����� ����մϴ�
    //�ε��� ���ڸ� ������ �� ���ڿ� �ش��ϴ� �������� ����� �� �ֽ��ϴ�(input.GetkeyDown)

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            HandleUse(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            HandleUse(2);
    }

    private void HandleUse(int slotIndex)
    {
        //���Կ��� ������ ������
        var data = SlotManager.Instance.UseSlot(slotIndex);
        if (data == null) return;

        // ���¹̳� ȸ�� ȿ��
        if (data.staminaRecovery > 0f)
        {
            PlayerAutoBinder.Instance.PlayerStat.RecoverStamina(data.staminaRecovery);
        }

        //���¹̳� �Ҹ� ���� ���� �ð� ���� ���� �ӵ��� ��� �ӵ���
        if (data.dash > 0f)
        {
            StartCoroutine(DashBuffRoutine(data.duration));
        }
    }

    private IEnumerator DashBuffRoutine(float buffDuration)
    {
        PlayerAutoBinder.Instance.PlayerController.dashItemActive = true;
        yield return new WaitForSeconds(buffDuration);

        PlayerAutoBinder.Instance.PlayerController.dashItemActive = false;
    }
}
