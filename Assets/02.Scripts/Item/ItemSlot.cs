using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //아이템 슬롯 프리팹에 붙일 스크립트

    public Image icon;
    public Image fillImage;
    public TextMeshProUGUI reuseText;
    public TextMeshProUGUI indexText;
    [HideInInspector] public bool isAvailable = true;
    private ItemData data;

    public ItemData Data => data;

    public void Setup(ItemData data, int index)
    {
        this.data = data;
        icon.sprite = data.image;
        indexText.text = index.ToString();
        reuseText.text = "";                  // 처음엔 빈칸
        fillImage.fillAmount = 1f;             // 사용 가능 상태
    }

    public IEnumerator CooldownRoutine()
    {
        isAvailable = false;
        float timeLeft = data.reuseTime;
        fillImage.fillAmount = 1f;

        while (timeLeft > 0f)
        {
            // 초 단위로 카운트다운 (5,4,3…)
            reuseText.text = Mathf.Ceil(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            fillImage.fillAmount = Mathf.Clamp01((data.reuseTime - timeLeft) / data.reuseTime);
            yield return null;
        }

        // 쿨타임 끝
        reuseText.text = "";
        fillImage.fillAmount = 0f;
        isAvailable = true;
    }
}
