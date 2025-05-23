using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //일단은 스태미나만 기록. 추후 스탯 추가 가능성

    [Header("스태미나 설정")]
    public float maxStamina = 100f;
    public float regenPerSecond = 5f;//자연회복
    public float drainPerSecond = 20f;//스태미나 소모
    public bool isDash = false;
    public float curStamina;//현재 스태미나 

    public event Action<float, float> OnStaminaChanged;

    private void Start()
    {
        curStamina = maxStamina;
        OnStaminaChanged?.Invoke(curStamina, maxStamina);
    }
    private void Update()
    {
        //isDash가 false면 회복 / true면 소모
        float delta = Time.deltaTime * (isDash ? -drainPerSecond : regenPerSecond);
        curStamina = Mathf.Clamp(curStamina + delta, 0f, maxStamina);
        if(isDash && curStamina <= 0f)
        {
            isDash = false;

        }
        OnStaminaChanged?.Invoke(curStamina, maxStamina);
    }

    public void SetDash(bool dash)
    {
        if(dash)
        {
            if(curStamina > 0f)
            {
                isDash = true;
            }
        }
        else
        {
            isDash = false;
        }
    }
    public void RecoverStamina(float amount)//스태미나 회복 아이템이 사용
    {
        curStamina = Mathf.Clamp(curStamina + amount, 0f, maxStamina);
        OnStaminaChanged?.Invoke(curStamina, maxStamina);
    }
}
