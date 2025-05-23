using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //플레이어의 기본적인 이동, 점프, 슈퍼점프를 다루는 스크립트

    [Header("Movement")]
    public float walkSpeed;//걷는 속도
    public float dashSpeed;//달리는 속도
    public float curSpeed;//현재 속도
    private Vector2 moveInput;
    [HideInInspector] public bool dashBuffActive = false;

    [Header("Jump")]
    public float jumpForce;
    public float superJumpForce;
    private bool isSuperJump = false;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform camPos;
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;
    private float camCurXRot;
    private Vector2 mouseDelta;

    private Rigidbody rigidbody;
    private PlayerStat stat;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        stat = GetComponent<PlayerStat>();

    }

    // Start is called before the first frame update
    void Start()
    {
        curSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        //curSpeed = stat.isDash ? dashSpeed : walkSpeed;
        bool isDashingForMovement = stat.isDash || dashBuffActive;
        curSpeed = isDashingForMovement ? dashSpeed : walkSpeed;
        Move();
    }
    void Update()
    {
        Look();
    }
    #region Move , Dash
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
    public void OnDash(InputAction.CallbackContext context)
    {
        if (dashBuffActive) return;
        if (context.phase == InputActionPhase.Performed)
        {
            stat.SetDash(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            stat.SetDash(false);
        }
    }
    #endregion

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

    #region Jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded())
        {
            float force = isSuperJump ? superJumpForce : jumpForce;
            rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(transform.position + Vector3.down * 0.1f,
                                   0.2f, groundLayerMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("JumpPad"))
        {
            isSuperJump = true;
        }
        else if (other.CompareTag("Statue")) 
        {
            SceneManager.LoadScene(1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("JumpPad"))
        {
            isSuperJump = false;
        }
    }
    #endregion
}
