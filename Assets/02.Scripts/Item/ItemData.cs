using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("������ ����")]
    public string itemName;
    [TextArea]
    public string description;
    public Sprite imgae;

    [Header("������ ȿ��")]
    public float staminaRecovery;
    public float dash;

    [Header("���� �ð�")]
    public float reuseTime;
}
