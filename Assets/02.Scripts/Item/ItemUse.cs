using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    //������ ����� ����մϴ�
    //�ε��� ���ڸ� ������ �� ���ڿ� �ش��ϴ� �������� ����� �� �ֽ��ϴ�(input.GetkeyDown)
    //�����۸��� ��Ÿ���� ������, 0�� �Ǳ� ������ ����� �� �����ϴ�(�ڷ�ƾ ���)

    private IEnumerator Cooldown(ItemData data)
    {
        yield return null;
    }
}
