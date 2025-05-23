using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    //레이캐스트로 interactable 레이어를 가진 아이템을 보면 그 아이템의 promptText가 뜨도록
    //E키를 누르면 상호작용 되도록

    [Header("레이어 마스크")]
    public LayerMask interactLayerMask;

    [Header("프롬프트 UI")]
    public TextMeshProUGUI promptText;


    public float maxCheckDistance;//얼마나 멀리있는걸 체크할지
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
        RaycastHit hit;//부딪힌 거 정보 

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
        //레이로 부딫힌 아이템의 프롬프트 출력
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
                //current가 interface라서 current.gameObject라고 쓸 수 없음
                Destroy((current as MonoBehaviour).gameObject);
            }
        }
    }
}
