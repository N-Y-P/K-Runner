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
            HandleUse(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            HandleUse(2);
    }

    private void HandleUse(int slotIndex)
    {
        //슬롯에서 데이터 꺼내기
        var data = SlotManager.Instance.UseSlot(slotIndex);
        if (data == null) return;

        // 스태미나 회복 효과
        if (data.staminaRecovery > 0f)
        {
            PlayerAutoBinder.Instance.PlayerStat.RecoverStamina(data.staminaRecovery);
        }

        //스태미나 소모 없이 일정 시간 동안 현재 속도를 대시 속도로
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
