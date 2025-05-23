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
    [HideInInspector] public bool isAvailable = true;//ó���� ��밡���� ����
    private ItemData data;

    public ItemData Data => data;

    public void Setup(ItemData data, int index)
    {
        this.data = data;
        icon.sprite = data.image;
        indexText.text = index.ToString();
        reuseText.text = ""; // ó���� ��ĭ
        fillImage.fillAmount = 0f;
    }

    public IEnumerator CooldownRoutine()//��Ÿ�� �ڷ�ƾ
    {
        isAvailable = false;//��Ÿ�� �߿��� ��� �Ұ�
        float timeLeft = data.reuseTime;
        fillImage.fillAmount = 1f;

        while (timeLeft > 0f)
        {
            // �� ������ ī��Ʈ�ٿ� 
            reuseText.text = Mathf.Ceil(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            fillImage.fillAmount = Mathf.Clamp01((data.reuseTime - timeLeft) / data.reuseTime);
            yield return null;
        }

        // ��Ÿ�� ��
        reuseText.text = "";//�ؽ�Ʈ �ٽ� ����
        fillImage.fillAmount = 0f;
        isAvailable = true;//���� �ٽ� ��밡���ϴ�
    }
}
