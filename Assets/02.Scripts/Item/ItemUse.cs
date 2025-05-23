using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    //아이템 사용을 담당합니다
    //인덱스 숫자를 누르면 그 숫자에 해당하는 아이템을 사용할 수 있습니다(input.GetkeyDown)

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
