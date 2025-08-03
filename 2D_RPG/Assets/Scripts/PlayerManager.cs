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
    #endregion

    void Move()
    {
        var move = new Vector3(moveInput.x, moveInput.y, 0f) * Time.deltaTime * speed;
        transform.Translate(move);
    }

    #region アクションベント
    void OnMouvePerformrd(InputAction.CallbackContext context)
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
