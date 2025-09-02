using UnityEngine;
using UnityEngine.UI; 
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// ターン制バトルを管理するクラス（ボタンあり＋ターン開始メッセージ）
/// - ターン開始時にDebug.LogとUIテキスト両方にメッセージを表示
/// - 「にげる」は即終了
/// - HPが残っている限りループ
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("キャラビュー")]
    [SerializeField] private CharacterView playerView;         // プレイヤーUI（HPテキスト付き）
    [SerializeField] private EnemyCharacterView enemyView;     // 敵UI（立ち絵のみ）

    [Header("UI")]
    [SerializeField] private ButtonManager buttonManager;      // コマンドボタン生成・管理
    [SerializeField] private Text battleLogText;               // 標準Textコンポーネント

    private bool isBattleActive; // バトル中かどうかのフラグ

    private async void Start()
    {
        // Inspectorでの設定漏れチェック
        if (playerView == null || enemyView == null || buttonManager == null)
        {
            Debug.LogError("BattleManagerの参照が設定されていません。Inspectorを確認してください。");
            return;
        }

        isBattleActive = true;
        await ShowLogAsync("バトル開始！");

        // バトルループ（プレイヤー→敵→プレイヤー…）
        while (isBattleActive)
        {
            // プレイヤーのターン
            await PlayerTurnAsync();

            // 「にげる」などでバトル終了フラグが立ったら即終了
            if (!isBattleActive) break;

            // 敵が倒れたら終了
            if (!enemyView.Actor.IsAlive)
            {
                await ShowLogAsync($"{enemyView.Actor.Name} を倒した！");
                break;
            }

            // 敵のターン
            await EnemyTurnAsync();

            // プレイヤーが倒れたら終了
            if (!isBattleActive) break;
            if (!playerView.Actor.IsAlive)
            {
                await ShowLogAsync($"{playerView.Actor.Name} が倒された…");
                break;
            }
        }

        await ShowLogAsync("バトル終了！");
        isBattleActive = false;
    }

    /// <summary>
    /// プレイヤーのターン処理
    /// - ターン開始メッセージを表示
    /// - ボタン入力を待って行動
    /// - 「にげる」は即終了
    /// </summary>
    private async UniTask PlayerTurnAsync()
    {
        // ターン開始メッセージ
        await ShowLogAsync($"{playerView.Actor.Name} のターン");

        // プレイヤーの入力待ち
        var tcs = new UniTaskCompletionSource<string>();
        buttonManager.GenerateCommandButtons(cmd => tcs.TrySetResult(cmd));

        // 選択が終わるまで待機
        string selectedCommand = await tcs.Task;
        await ShowLogAsync($"{selectedCommand} を選択！");

        // コマンドに応じた処理
        if (selectedCommand == "たたかう")
        {
            int damage = playerView.Actor.Atk;
            enemyView.Actor.TakeDamage(damage);
            await ShowLogAsync($"{enemyView.Actor.Name} に {damage} ダメージ！");
        }
        else if (selectedCommand == "まほう")
        {
            int damage = playerView.Actor.Atk + 5;
            enemyView.Actor.TakeDamage(damage);
            await ShowLogAsync($"{enemyView.Actor.Name} に {damage} ダメージ！（魔法）");
        }
        else if (selectedCommand == "にげる")
        {
            await ShowLogAsync("逃げた！");
            isBattleActive = false; // 即終了
            return; // 敵ターンに行かず抜ける
        }
    }

    /// <summary>
    /// 敵のターン処理
    /// - ターン開始メッセージを表示
    /// - 単純に攻撃してダメージを与える
    /// </summary>
    private async UniTask EnemyTurnAsync()
    {
        // ターン開始メッセージ
        await ShowLogAsync($"{enemyView.Actor.Name} のターン", 1f);

        int damage = enemyView.Actor.Atk;
        playerView.Actor.TakeDamage(damage);
        await ShowLogAsync($"{playerView.Actor.Name} は {damage} ダメージを受けた！");
    }

    /// <summary>
    /// ログをUIとコンソール両方に出す
    /// - UIテキストが設定されていればUIにも表示
    /// - waitSecondsで表示後の待機時間を調整
    /// </summary>
    private async UniTask ShowLogAsync(string message, float waitSeconds = 1f)
    {
        Debug.Log(message); // コンソール出力
        if (battleLogText != null) battleLogText.text = message; // 標準Textに表示
        await UniTask.Delay(System.TimeSpan.FromSeconds(waitSeconds));
    }
}
