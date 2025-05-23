using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //�ϴ��� ���¹̳��� ���. ���� ���� �߰� ���ɼ�

    [Header("���¹̳� ����")]
    public float maxStamina = 100f;
    public float regenPerSecond = 5f;//�ڿ�ȸ��
    public float drainPerSecond = 20f;//���¹̳� �Ҹ�
    public bool isDash = false;
    public float curStamina;//���� ���¹̳� 

    public event Action<float, float> OnStaminaChanged;

    private void Start()
    {
        curStamina = maxStamina;
        OnStaminaChanged?.Invoke(curStamina, maxStamina);
    }
    private void Update()
    {
        //isDash�� false�� ȸ�� / true�� �Ҹ�
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
    public void RecoverStamina(float amount)//���¹̳� ȸ�� �������� ���
    {
        curStamina = Mathf.Clamp(curStamina + amount, 0f, maxStamina);
        OnStaminaChanged?.Invoke(curStamina, maxStamina);
    }
}
