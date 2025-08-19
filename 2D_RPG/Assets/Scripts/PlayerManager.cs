using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region プライベート変数
    private PlayerAction controls;      // InputSystemで生成されるアクションクラス
    private Vector2 moveInput;          // 入力された方向
    [SerializeField] private float speed;           // 移動速度
    [SerializeField] private LayerMask ObjctUI;    // 衝突判定を行う対象レイヤー

    private Animator animator;          // Animatorコンポーネント
    #endregion

    #region Unityイベント
    private void Awake()
    {
        controls = new PlayerAction();                 // InputAction初期化
        animator = GetComponent<Animator>();          // Animator取得
    }

    private void OnEnable()
    {
        controls.Player.Move.Enable();                        // 入力受付開始
        controls.Player.Move.performed += OnMovePerformed;   // 入力時イベント登録
        controls.Player.Move.canceled += OnMoveCanceled;     // 離した時イベント登録
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMovePerformed;  // イベント解除
        controls.Player.Move.canceled -= OnMoveCanceled;    // イベント解除
        controls.Player.Move.Disable();                     // 入力受付終了
    }

    private void Update()
    {
        Move();    // 毎フレーム移動処理
    }
    #endregion

    // 移動処理
    private void Move()
    {
        // 右左優先でY入力を無効化
        if (moveInput.x != 0) moveInput.y = 0;

        // 移動予定位置を計算
        Vector3 targetPos = transform.position + new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime;

        // 移動できるか判定（ObjctUIにぶつかるなら移動しない）
        if (!IsWalkable(targetPos))
        {
            animator.SetBool("IsMoving", false); // 移動できなければアニメーションも止める
            return;                               // 移動処理終了
        }

        // 実際に移動
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime, Space.World);

        // アニメーション更新
        bool IsMoving = moveInput.sqrMagnitude > 0f;  // 入力があるかで判定
        animator.SetFloat("InputX", moveInput.x);    // AnimatorにX方向入力を渡す
        animator.SetFloat("InputY", moveInput.y);    // AnimatorにY方向入力を渡す
        animator.SetBool("IsMoving", IsMoving);      // Animatorに移動中かどうかを渡す
    }

    // 入力イベント
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();  // 入力された方向を取得
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;                  // 入力が離れたら方向をリセット
    }

    // 衝突判定関数
    private bool IsWalkable(Vector3 targetPos)
    {
        // targetPosを中心に半径0.1のOverlapCircleを作成
        // ObjctUIレイヤーにぶつかる場合はfalseを返す
        return Physics2D.OverlapCircle(targetPos, 0.1f, ObjctUI) == false;
    }
}
