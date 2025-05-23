using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    //튜토리얼 UI에 붙는 스크립트
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
