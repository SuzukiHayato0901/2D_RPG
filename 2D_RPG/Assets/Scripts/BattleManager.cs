using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

/// <summary>
/// �^�[�����o�g�����Ǘ�����N���X�i�{�^������{�^�[���J�n���b�Z�[�W�j
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("�L�����r���[")]
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
            Debug.LogError("BattleManager�̎Q�Ƃ��ݒ肳��Ă��܂���B");
            return;
        }

        isBattleActive = true;
        await ShowLogAsync("�o�g���J�n�I");

        while (isBattleActive)
        {
            await PlayerTurnAsync();
            if (!isBattleActive || !enemyView.Actor.IsAlive) break;

            await EnemyTurnAsync();
            if (!isBattleActive || !playerView.Actor.IsAlive) break;
        }

        await ShowLogAsync("�o�g���I���I");
        isBattleActive = false;
    }

    private async UniTask PlayerTurnAsync()
    {
        await ShowLogAsync($"{playerView.Actor.Name} �̃^�[��");

        var tcs = new UniTaskCompletionSource<string>();
        buttonManager.GenerateCommandButtons(cmd => tcs.TrySetResult(cmd));
        string selectedCommand = await tcs.Task;

        await ShowLogAsync($"{selectedCommand} ��I���I");

        int damage = 0;

        if (selectedCommand == "��������")
        {
            damage = Mathf.Max(1, playerView.Actor.Atk - enemyView.Actor.Def);
            enemyView.Actor.TakeDamage(damage);
            await ShowLogAsync($"{enemyView.Actor.Name} �� {damage} �_���[�W�I");
        }
        else if (selectedCommand == "�܂ق�")
        {
            damage = playerView.Actor.Atk + 5; // �h�䖳��
            enemyView.Actor.TakeDamage(damage);
            await ShowLogAsync($"{enemyView.Actor.Name} �� {damage} �_���[�W�I�i���@�j");
        }
        else if (selectedCommand == "�ɂ���")
        {
            await ShowLogAsync("�������I");
            isBattleActive = false;
            return;
        }
    }


    private async UniTask EnemyTurnAsync()
    {
        await ShowLogAsync($"{enemyView.Actor.Name} �̃^�[��", 1f);

        int damage = enemyView.Actor.Atk;
        playerView.Actor.TakeDamage(damage);
        await ShowLogAsync($"{playerView.Actor.Name} �� {damage} �_���[�W���󂯂��I");
    }

    private async UniTask ShowLogAsync(string message, float waitSeconds = 1f)
    {
        Debug.Log(message);
        if (battleLogText != null) battleLogText.text = message;
        await UniTask.Delay(System.TimeSpan.FromSeconds(waitSeconds));
    }
}
