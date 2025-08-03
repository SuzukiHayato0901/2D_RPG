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
    #endregion

    void Move()
    {
        var move = new Vector3(moveInput.x, moveInput.y, 0f) * Time.deltaTime * speed;
        transform.Translate(move);
    }

    #region �A�N�V�����x���g
    void OnMouvePerformrd(InputAction.CallbackContext context)
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
    }

    private void OnEnable()
    {
        controls.Player.Move.Enable();

        controls.Player.Move.performed += OnMouvePerformrd;
        controls.Player.Move.canceled += OnMouveCanceled;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMouvePerformrd;
        controls.Player.Move.canceled -= OnMouveCanceled;

        controls.Player.Move.Disable();
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
