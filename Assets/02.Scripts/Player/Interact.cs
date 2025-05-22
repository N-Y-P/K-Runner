using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    //레이캐스트로 interactable 레이어를 가진 아이템을 보면 그 아이템의 promptText가 뜨도록
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
