using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("아이템 정보")]
    public string itemName;
    public string description;

    [Header("아이템 효과")]
    public float staminaRecovery;
    public float dash;

    [Header("재사용 시간")]
    public float reuseTime;
}
