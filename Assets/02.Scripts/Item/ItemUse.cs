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
        {
            SlotManager.Instance.UseSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SlotManager.Instance.UseSlot(2);
        }
    }
}
