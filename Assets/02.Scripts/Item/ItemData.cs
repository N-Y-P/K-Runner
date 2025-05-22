using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("아이템 정보")]
    public string itemName;
    [TextArea]
    public string description;
    public Sprite imgae;

    [Header("아이템 효과")]
    public float staminaRecovery;
    public float dash;

    [Header("재사용 시간")]
    public float reuseTime;
}
