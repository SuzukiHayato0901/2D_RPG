using UnityEngine;

/// <summary>
/// プレイヤーを追いかけるカメラ制御スクリプト
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // 追従対象（通常はプレイヤー）
    [SerializeField] private float followSpeed = 5f; // カメラの追従速度

    private void LateUpdate()
    {
        // ターゲットが設定されていなければ何もしない
        if (target == null) return;

        // 追従する位置（Z座標はカメラのまま）
        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        // 線形補間でスムーズに追従
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// 外部からターゲット（追従先）を設定できるようにする
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
