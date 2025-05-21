using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //플레이어의 기본적인 이동, 점프, 슈퍼점프를 다룹니다
    [Header("Movement")]
    public float moovSpeed;
    public float dashSpeed;
    public float jumpForce;
    private Vector2 moveInput;

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
        
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        
    }
    #region Move
    public void Move()
    {
        Vector3 dir = transform.forward * moveInput.y + transform.right * moveInput.x;
        dir *= moovSpeed;
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

    #region Look

    public void Look()
    {

    }

    void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    #endregion

    void OnJump(InputAction.CallbackContext context)
    {

    }
}
