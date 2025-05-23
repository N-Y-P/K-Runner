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
    [HideInInspector] public bool isAvailable = true;//처음은 사용가능한 상태
    private ItemData data;

    public ItemData Data => data;

    public void Setup(ItemData data, int index)
    {
        this.data = data;
        icon.sprite = data.image;
        indexText.text = index.ToString();
        reuseText.text = ""; // 처음엔 빈칸
        fillImage.fillAmount = 0f;
    }

    public IEnumerator CooldownRoutine()//쿨타임 코루틴
    {
        isAvailable = false;//쿨타임 중에는 사용 불가
        float timeLeft = data.reuseTime;
        fillImage.fillAmount = 1f;

        while (timeLeft > 0f)
        {
            // 초 단위로 카운트다운 
            reuseText.text = Mathf.Ceil(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            fillImage.fillAmount = Mathf.Clamp01((data.reuseTime - timeLeft) / data.reuseTime);
            yield return null;
        }

        // 쿨타임 끝
        reuseText.text = "";//텍스트 다시 비우기
        fillImage.fillAmount = 0f;
        isAvailable = true;//이제 다시 사용가능하다
    }
}
