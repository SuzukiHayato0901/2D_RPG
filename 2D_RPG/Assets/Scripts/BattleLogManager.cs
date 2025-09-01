using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// バトルログの履歴表示を管理するクラス。
/// </summary>
public class BattleLogManager : MonoBehaviour
{
    [SerializeField] private Transform logContainer; // ログを並べる親（Content）
    [SerializeField] private GameObject logTextPrefab; // 1行分のテキストプレハブ
    [SerializeField] private ScrollRect scrollRect; // 自動スクロール用

    /// <summary>
    /// ログを1行追加して指定秒数待機
    /// </summary>
    public async UniTask AddLogAsync(string message, float waitSeconds = 1f)
    {
        // 新しい行を生成
        var logObj = Instantiate(logTextPrefab, logContainer);
        var textComp = logObj.GetComponent<Text>();
        textComp.text = message;

        // スクロールを一番下に
        await UniTask.Yield();
        scrollRect.verticalNormalizedPosition = 0f;

        // 演出用の待機
        await UniTask.Delay(TimeSpan.FromSeconds(waitSeconds));
    }
}
