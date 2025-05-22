using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    //아이템 사용을 담당합니다
    //인덱스 숫자를 누르면 그 숫자에 해당하는 아이템을 사용할 수 있습니다(input.GetkeyDown)
    //아이템마다 쿨타임이 있으며, 0이 되기 전까지 사용할 수 없습니다(코루틴 사용)

    private IEnumerator Cooldown(ItemData data)
    {
        yield return null;
    }
}
