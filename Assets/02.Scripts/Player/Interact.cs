using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    //����ĳ��Ʈ�� interactable ���̾ ���� �������� ���� �� �������� promptText�� �ߵ���
    public TextMeshPro promptText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {

    }
}
