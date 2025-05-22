using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    //����ĳ��Ʈ�� interactable ���̾ ���� �������� ���� �� �������� promptText�� �ߵ���

    [Header("���̾� ����ũ")]
    public LayerMask interactLayerMask;

    [Header("������Ʈ UI")]
    public TextMeshProUGUI promptText;


    public float maxCheckDistance;//�󸶳� �ָ��ִ°� üũ����
    public GameObject interactPrefab;
    private IInteractable current;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;//�ε��� �� ���� 

        if (Physics.Raycast(ray, out hit, maxCheckDistance, interactLayerMask))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if(interactable != null )
            {
                if(interactable != current )
                {
                    current = interactable;
                    SetPromptText();
                }
                return;
            }
        }

        ClearPrompt();
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        //���̷� �΋H�� �������� ������Ʈ ���
        promptText.text = current.GetInteractPrompt();
    }
    private void ClearPrompt()
    {
        current = null;
        promptText.gameObject.SetActive(false);
    }
    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && current != null)
        {
            var itemData = (current as Item)?.itemData;
            if (itemData != null)
            {
                SlotManager.Instance.AddItem(itemData);
                Destroy((current as MonoBehaviour).gameObject);
            }
        }
    }
}
