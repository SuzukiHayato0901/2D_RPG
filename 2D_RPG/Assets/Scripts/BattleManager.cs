using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// ターン制バトルを管理するバトルマネージャー
/// ButtonManagerでプレイヤー入力を受け取り、CharacterViewでHPを表示
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("キャラビュー（UIとActorを持つ）")]
    [SerializeField] private CharacterView playerView;
    [SerializeField] private CharacterView enemyView;

    [Header("UI")]
    [SerializeField] private ButtonManager buttonManager;

    private bool isBattleActive;

    private async void Start()
    {
        isBattleActive = true;
        Debug.Log("バトル開始！");

        // プレイヤーターンから開始
        while (isBattleActive)
        {
            await PlayerTurnAsync();
            if (!enemyView.Actor.IsAlive)
            {
                Debug.Log($"{enemyView.Actor.Name} を倒した！");
                break;
            }

            await EnemyTurnAsync();
            if (!playerView.Actor.IsAlive)
            {
                Debug.Log($"{playerView.Actor.Name} が倒された…");
                break;
            }
        }

        Debug.Log("バトル終了！");
        isBattleActive = false;
    }

    /// <summary>
    /// プレイヤーのターン処理
    /// </summary>
    private async UniTask PlayerTurnAsync()
    {
        Debug.Log($"{playerView.Actor.Name} のターン");

        // プレイヤーの入力待ち
        var tcs = new UniTaskCompletionSource<string>();
        buttonManager.GenerateCommandButtons(cmd => tcs.TrySetResult(cmd));

        string selectedCommand = await tcs.Task;
        Debug.Log($"選択されたコマンド: {selectedCommand}");

        // コマンドに応じた処理
        if (selectedCommand == "たたかう")
        {
            enemyView.Actor.TakeDamage(playerView.Actor.Atk);
        }
        else if (selectedCommand == "まほう")
        {
            enemyView.Actor.TakeDamage(playerView.Actor.Atk + 5);
        }
        else if (selectedCommand == "にげる")
        {
            Debug.Log("逃げた！");
            isBattleActive = false;
        }

        await UniTask.Delay(500); // 演出用
    }

    /// <summary>
    /// 敵のターン処理
    /// </summary>
    private async UniTask EnemyTurnAsync()
    {
        Debug.Log($"{enemyView.Actor.Name} のターン");

        await UniTask.Delay(1000); // 思考時間

        // 単純に攻撃
        playerView.Actor.TakeDamage(enemyView.Actor.Atk);

        await UniTask.Delay(500); // 演出用
    }
}
