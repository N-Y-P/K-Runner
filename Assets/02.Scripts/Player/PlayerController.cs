using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //�÷��̾��� �⺻���� �̵�, ����, ���������� �ٷ�� ��ũ��Ʈ

    [Header("Movement")]
    public float walkSpeed;//�ȴ� �ӵ�
    public float dashSpeed;//�޸��� �ӵ�
    public float curSpeed;//���� �ӵ�
    private Vector2 moveInput;
    //��� ������ ��� �� ���� �ӵ��� ��� �ӵ���, �� ���� ��� Ű�� ������� ���ϰ�
    [HideInInspector] public bool dashItemActive = false;

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

    void Start()
    {
        curSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        //��� Ű�� ������̰ų�, �������� ������̸� true, �Ѵ� ������� �ʴ´ٸ� false
        bool isDashingForMovement = stat.isDash || dashItemActive;
        curSpeed = isDashingForMovement ? dashSpeed : walkSpeed;
        Move();
    }
    void Update()
    {
        if (Time.timeScale > 0.5f)
        {
            Look();

        }
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
        if (dashItemActive) return;//��� �������� ����Ѵٸ� �Ʒ� ��� ��� ���ϰ�
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
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
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
        if (other.CompareTag("JumpPad"))
        {
            isSuperJump = true;
        }
        else if (other.CompareTag("Statue"))
        {
            SceneManager.LoadScene(1);
        }
        else if (other.CompareTag("Finish"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JumpPad"))
        {
            isSuperJump = false;
        }
    }
    #endregion
}
