using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialUI;
    void Awake()
    {
        Time.timeScale = 0f;

    }
    private void Update()
    {
        if (Input.anyKey)
        {
            Time.timeScale = 1f;
            tutorialUI.SetActive(false);
        }
    }
}
