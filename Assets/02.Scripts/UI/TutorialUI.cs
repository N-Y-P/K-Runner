using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    //Ʃ�丮�� UI�� �ٴ� ��ũ��Ʈ
    public GameObject tutorialUI;

    private void Start()
    {
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if(Input.anyKey)
        {
            Time.timeScale = 1.0f;
            tutorialUI.SetActive(false);
        }
    }
}
