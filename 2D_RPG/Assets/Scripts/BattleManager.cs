using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// �^�[�����o�g�����Ǘ�����o�g���}�l�[�W���[
/// ButtonManager�Ńv���C���[���͂��󂯎��ACharacterView��HP��\��
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("�L�����r���[�iUI��Actor�����j")]
    [SerializeField] private CharacterView playerView;
    [SerializeField] private CharacterView enemyView;

    [Header("UI")]
    [SerializeField] private ButtonManager buttonManager;

    private bool isBattleActive;

    private async void Start()
    {
        isBattleActive = true;
        Debug.Log("�o�g���J�n�I");

        // �v���C���[�^�[������J�n
        while (isBattleActive)
        {
            await PlayerTurnAsync();
            if (!enemyView.Actor.IsAlive)
            {
                Debug.Log($"{enemyView.Actor.Name} ��|�����I");
                break;
            }

            await EnemyTurnAsync();
            if (!playerView.Actor.IsAlive)
            {
                Debug.Log($"{playerView.Actor.Name} ���|���ꂽ�c");
                break;
            }
        }

        Debug.Log("�o�g���I���I");
        isBattleActive = false;
    }

    /// <summary>
    /// �v���C���[�̃^�[������
    /// </summary>
    private async UniTask PlayerTurnAsync()
    {
        Debug.Log($"{playerView.Actor.Name} �̃^�[��");

        // �v���C���[�̓��͑҂�
        var tcs = new UniTaskCompletionSource<string>();
        buttonManager.GenerateCommandButtons(cmd => tcs.TrySetResult(cmd));

        string selectedCommand = await tcs.Task;
        Debug.Log($"�I�����ꂽ�R�}���h: {selectedCommand}");

        // �R�}���h�ɉ���������
        if (selectedCommand == "��������")
        {
            enemyView.Actor.TakeDamage(playerView.Actor.Atk);
        }
        else if (selectedCommand == "�܂ق�")
        {
            enemyView.Actor.TakeDamage(playerView.Actor.Atk + 5);
        }
        else if (selectedCommand == "�ɂ���")
        {
            Debug.Log("�������I");
            isBattleActive = false;
        }

        await UniTask.Delay(500); // ���o�p
    }

    /// <summary>
    /// �G�̃^�[������
    /// </summary>
    private async UniTask EnemyTurnAsync()
    {
        Debug.Log($"{enemyView.Actor.Name} �̃^�[��");

        await UniTask.Delay(1000); // �v�l����

        // �P���ɍU��
        playerView.Actor.TakeDamage(enemyView.Actor.Atk);

        await UniTask.Delay(500); // ���o�p
    }
}
