using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //플레이어의 기본적인 이동, 점프, 상호작용을 다룹니다
    [Header("Movement")]
    public float walkSpeed;//걷는 속도
    public float dashSpeed;//달리는 속도
    public float curSpeed;//현재 속도
    public float jumpForce;
    private Vector2 moveInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform camPos;
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;
    private float camCurXRot;
    private Vector2 mouseDelta;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        curSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        Look();
    }
    #region Move
    public void Move()
    {
        Vector3 dir = transform.forward * moveInput.y + transform.right * moveInput.x;
        dir *= curSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }
    #endregion

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            curSpeed = dashSpeed;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curSpeed = walkSpeed;
        }
    }

    #region Look

    public void Look()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot,minXLook, maxXLook);
        camPos.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    #endregion

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(transform.position + Vector3.down * 0.1f,
                                   0.2f, groundLayerMask);
    }

}
