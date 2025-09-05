using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

/// <summary>
/// ターン制バトルを管理するクラス（ボタンあり＋ターン開始メッセージ）
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("キャラビュー")]
    [SerializeField] private CharacterView playerView;
    [SerializeField] private EnemyCharacterView enemyView;

    [Header("UI")]
    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private Text battleLogText;

    private bool isBattleActive;

    private async void Start()
    {
        if (playerView == null || enemyView == null || buttonManager == null)
        {
            Debug.LogError("BattleManagerの参照が設定されていません。");
            return;
        }

        isBattleActive = true;
        await ShowLogAsync("バトル開始！");

        while (isBattleActive)
        {
            await PlayerTurnAsync();
            if (!isBattleActive || !enemyView.Actor.IsAlive) break;

            await EnemyTurnAsync();
            if (!isBattleActive || !playerView.Actor.IsAlive) break;
        }

        await ShowLogAsync("バトル終了！");
        isBattleActive = false;
    }

    private async UniTask PlayerTurnAsync()
    {
        await ShowLogAsync($"{playerView.Actor.Name} のターン");

        var tcs = new UniTaskCompletionSource<string>();
        buttonManager.GenerateCommandButtons(cmd => tcs.TrySetResult(cmd));
        string selectedCommand = await tcs.Task;

        await ShowLogAsync($"{selectedCommand} を選択！");

        int damage = 0;

        if (selectedCommand == "たたかう")
        {
            damage = Mathf.Max(1, playerView.Actor.Atk - enemyView.Actor.Def);
            enemyView.Actor.TakeDamage(damage);
            await ShowLogAsync($"{enemyView.Actor.Name} に {damage} ダメージ！");
        }
        else if (selectedCommand == "まほう")
        {
            damage = playerView.Actor.Atk + 5; // 防御無視
            enemyView.Actor.TakeDamage(damage);
            await ShowLogAsync($"{enemyView.Actor.Name} に {damage} ダメージ！（魔法）");
        }
        else if (selectedCommand == "にげる")
        {
            await ShowLogAsync("逃げた！");
            isBattleActive = false;
            return;
        }
    }


    private async UniTask EnemyTurnAsync()
    {
        await ShowLogAsync($"{enemyView.Actor.Name} のターン", 1f);

        int damage = enemyView.Actor.Atk;
        playerView.Actor.TakeDamage(damage);
        await ShowLogAsync($"{playerView.Actor.Name} は {damage} ダメージを受けた！");
    }

    private async UniTask ShowLogAsync(string message, float waitSeconds = 1f)
    {
        Debug.Log(message);
        if (battleLogText != null) battleLogText.text = message;
        await UniTask.Delay(System.TimeSpan.FromSeconds(waitSeconds));
    }
}
