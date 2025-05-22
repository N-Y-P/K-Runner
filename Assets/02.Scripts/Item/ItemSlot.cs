using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //������ ���� �����տ� ���� ��ũ��Ʈ
    public Image icon;
    public Image fillImage;
    public TextMeshProUGUI reuseText;
    public TextMeshProUGUI indexText;

    public void Setup(ItemData data, int index)
    {
        icon.sprite = data.image;
        reuseText.text = data.reuseTime.ToString("F1");
        indexText.text = index.ToString();
    }
}
