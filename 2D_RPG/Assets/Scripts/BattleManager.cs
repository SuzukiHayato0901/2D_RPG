using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;

/// <summary>
/// ターン制バトルの進行管理クラス。
/// - CharacterDataのステータスを使ってダメージ計算
/// - BattleMessageDataのテンプレートを使って文章生成
/// - BattleLogManagerで複数行履歴表示
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("キャラデータ")]
    [SerializeField] private CharacterData playerData; // プレイヤーのステータス
    [SerializeField] private CharacterData enemyData;  // 敵のステータス

    [Header("UI")]
    [SerializeField] private ButtonManager buttonManager; // コマンド入力UI
    [SerializeField] private BattleLogManager logManager; // ログ表示管理

    [Header("メッセージデータ")]
    [SerializeField] private BattleMessageData selectCommandMsg; // "{0} は {1} を選択！"
    [SerializeField] private BattleMessageData damageMsg;        // "{0} に {1} ダメージ！"
    [SerializeField] private BattleMessageData enemyTurnMsg;     // "敵のターン…"



    private bool isBattleActive;

    private async void Start()
    {
        isBattleActive = true;
        await logManager.AddLogAsync("バトル開始！");

        // バトルループ
        while (isBattleActive)
        {
            await PlayerTurnAsync();
            if (!enemyData.IsAlive)
            {
                await logManager.AddLogAsync($"{enemyData.characterName} を倒した！");
                break;
            }

            await EnemyTurnAsync();
            if (!playerData.IsAlive)
            {
                await logManager.AddLogAsync($"{playerData.characterName} が倒された…");
                break;
            }
        }

        await logManager.AddLogAsync("バトル終了！");
        isBattleActive = false;
    }

    /// <summary>
    /// プレイヤーのターン処理
    /// </summary>
    private async UniTask PlayerTurnAsync()
    {
        await logManager.AddLogAsync($"{playerData.characterName} のターン");

        // コマンド入力待ち
        var tcs = new UniTaskCompletionSource<string>();
        buttonManager.GenerateCommandButtons(cmd => tcs.TrySetResult(cmd));

        string selectedCommand = await tcs.Task;
        await logManager.AddLogAsync(string.Format(selectCommandMsg.messageTemplate, playerData.characterName, selectedCommand));

        // コマンド別処理
        if (selectedCommand == "たたかう")
        {
            int damage = CalcDamage(playerData, enemyData);
            enemyData.TakeDamage(damage);
            await logManager.AddLogAsync(string.Format(damageMsg.messageTemplate, enemyData.characterName, damage));
        }
        else if (selectedCommand == "まほう")
        {
            int damage = CalcDamage(playerData, enemyData) + 5;
            enemyData.TakeDamage(damage);
            await logManager.AddLogAsync(string.Format(damageMsg.messageTemplate, enemyData.characterName, damage));
        }
        else if (selectedCommand == "にげる")
        {
            await logManager.AddLogAsync("逃げた！");
            isBattleActive = false;
        }
    }

    /// <summary>
    /// 敵のターン処理
    /// </summary>
    private async UniTask EnemyTurnAsync()
    {
        await logManager.AddLogAsync(enemyTurnMsg.messageTemplate);

        int damage = CalcDamage(enemyData, playerData);
        playerData.TakeDamage(damage);
        await logManager.AddLogAsync(string.Format(damageMsg.messageTemplate, playerData.characterName, damage));
    }

    /// <summary>
    /// ダメージ計算（最低1ダメージ保証）
    /// </summary>
    private int CalcDamage(CharacterData attacker, CharacterData defender)
    {
        return Mathf.Max(1, attacker.atk - defender.def + UnityEngine.Random.Range(-2, 3));
    }
}
