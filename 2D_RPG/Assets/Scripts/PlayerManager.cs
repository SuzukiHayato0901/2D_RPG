using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region プライベート変数
    PlayerAction controls;          // アクションクラス
    Vector2 moveInput;              // 入力された方向
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

    #region アクションベント
    void OnMovePerformed(InputAction.CallbackContext context)
    {
        // 入力時の操作
        moveInput  = context.ReadValue<Vector2>();
    }

    void OnMouveCanceled(InputAction.CallbackContext context)
    {
        // 離した時の動作
        moveInput = Vector2.zero;
    }

    #endregion

    #region Unityイベント
    private void Awake()
    {
        controls = new PlayerAction();

        // animatorの取得
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 入力受付開始
    /// </summary>
    private void OnEnable()
    {
        controls.Player.Move.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMouveCanceled;
    }

    /// <summary>
    /// イベント解除
    /// </summary>
    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled -= OnMouveCanceled;

        controls.Player.Move.Disable();     // 入力受付終了
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
