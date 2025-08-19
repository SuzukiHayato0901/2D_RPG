using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region �v���C�x�[�g�ϐ�
    PlayerAction controls;          // �A�N�V�����N���X
    Vector2 moveInput;              // ���͂��ꂽ����
    [SerializeField] float speed;

    Animator animator;
    #endregion

    void Move()
    {
        var move = new Vector3(moveInput.x, moveInput.y, 0f) * Time.deltaTime * speed;
        transform.Translate(move);
        bool IsMoving = moveInput.sqrMagnitude > 0f;

        if (moveInput.x != 0)
        {
            moveInput.y = 0f;
        }

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
        animator.SetBool("IsMoving", IsMoving);
    }

    #region �A�N�V�����x���g
    void OnMovePerformed(InputAction.CallbackContext context)
    {
        // ���͎��̑���
        moveInput  = context.ReadValue<Vector2>();
    }

    void OnMouveCanceled(InputAction.CallbackContext context)
    {
        // ���������̓���
        moveInput = Vector2.zero;
    }

    #endregion

    #region Unity�C�x���g
    private void Awake()
    {
        controls = new PlayerAction();

        // animator�̎擾
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// ���͎�t�J�n
    /// </summary>
    private void OnEnable()
    {
        controls.Player.Move.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMouveCanceled;
    }

    /// <summary>
    /// �C�x���g����
    /// </summary>
    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled -= OnMouveCanceled;

        controls.Player.Move.Disable();     // ���͎�t�I��
    }
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }
}
