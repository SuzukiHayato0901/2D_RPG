using UnityEngine;
using UnityEngine.UI; 
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// �^�[�����o�g�����Ǘ�����N���X�i�{�^������{�^�[���J�n���b�Z�[�W�j
/// - �^�[���J�n����Debug.Log��UI�e�L�X�g�����Ƀ��b�Z�[�W��\��
/// - �u�ɂ���v�͑��I��
/// - HP���c���Ă�����胋�[�v
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("�L�����r���[")]
    [SerializeField] private CharacterView playerView;         // �v���C���[UI�iHP�e�L�X�g�t���j
    [SerializeField] private EnemyCharacterView enemyView;     // �GUI�i�����G�̂݁j

    [Header("UI")]
    [SerializeField] private ButtonManager buttonManager;      // �R�}���h�{�^�������E�Ǘ�
    [SerializeField] private Text battleLogText;               // �W��Text�R���|�[�l���g

    private bool isBattleActive; // �o�g�������ǂ����̃t���O

    private async void Start()
    {
        // Inspector�ł̐ݒ�R��`�F�b�N
        if (playerView == null || enemyView == null || buttonManager == null)
        {
            Debug.LogError("BattleManager�̎Q�Ƃ��ݒ肳��Ă��܂���BInspector���m�F���Ă��������B");
            return;
        }

        isBattleActive = true;
        await ShowLogAsync("�o�g���J�n�I");

        // �o�g�����[�v�i�v���C���[���G���v���C���[�c�j
        while (isBattleActive)
        {
            // �v���C���[�̃^�[��
            await PlayerTurnAsync();

            // �u�ɂ���v�ȂǂŃo�g���I���t���O���������瑦�I��
            if (!isBattleActive) break;

            // �G���|�ꂽ��I��
            if (!enemyView.Actor.IsAlive)
            {
                await ShowLogAsync($"{enemyView.Actor.Name} ��|�����I");
                break;
            }

            // �G�̃^�[��
            await EnemyTurnAsync();

            // �v���C���[���|�ꂽ��I��
            if (!isBattleActive) break;
            if (!playerView.Actor.IsAlive)
            {
                await ShowLogAsync($"{playerView.Actor.Name} ���|���ꂽ�c");
                break;
            }
        }

        await ShowLogAsync("�o�g���I���I");
        isBattleActive = false;
    }

    /// <summary>
    /// �v���C���[�̃^�[������
    /// - �^�[���J�n���b�Z�[�W��\��
    /// - �{�^�����͂�҂��čs��
    /// - �u�ɂ���v�͑��I��
    /// </summary>
    private async UniTask PlayerTurnAsync()
    {
        // �^�[���J�n���b�Z�[�W
        await ShowLogAsync($"{playerView.Actor.Name} �̃^�[��");

        // �v���C���[�̓��͑҂�
        var tcs = new UniTaskCompletionSource<string>();
        buttonManager.GenerateCommandButtons(cmd => tcs.TrySetResult(cmd));

        // �I�����I���܂őҋ@
        string selectedCommand = await tcs.Task;
        await ShowLogAsync($"{selectedCommand} ��I���I");

        // �R�}���h�ɉ���������
        if (selectedCommand == "��������")
        {
            int damage = playerView.Actor.Atk;
            enemyView.Actor.TakeDamage(damage);
            await ShowLogAsync($"{enemyView.Actor.Name} �� {damage} �_���[�W�I");
        }
        else if (selectedCommand == "�܂ق�")
        {
            int damage = playerView.Actor.Atk + 5;
            enemyView.Actor.TakeDamage(damage);
            await ShowLogAsync($"{enemyView.Actor.Name} �� {damage} �_���[�W�I�i���@�j");
        }
        else if (selectedCommand == "�ɂ���")
        {
            await ShowLogAsync("�������I");
            isBattleActive = false; // ���I��
            return; // �G�^�[���ɍs����������
        }
    }

    /// <summary>
    /// �G�̃^�[������
    /// - �^�[���J�n���b�Z�[�W��\��
    /// - �P���ɍU�����ă_���[�W��^����
    /// </summary>
    private async UniTask EnemyTurnAsync()
    {
        // �^�[���J�n���b�Z�[�W
        await ShowLogAsync($"{enemyView.Actor.Name} �̃^�[��", 1f);

        int damage = enemyView.Actor.Atk;
        playerView.Actor.TakeDamage(damage);
        await ShowLogAsync($"{playerView.Actor.Name} �� {damage} �_���[�W���󂯂��I");
    }

    /// <summary>
    /// ���O��UI�ƃR���\�[�������ɏo��
    /// - UI�e�L�X�g���ݒ肳��Ă����UI�ɂ��\��
    /// - waitSeconds�ŕ\����̑ҋ@���Ԃ𒲐�
    /// </summary>
    private async UniTask ShowLogAsync(string message, float waitSeconds = 1f)
    {
        Debug.Log(message); // �R���\�[���o��
        if (battleLogText != null) battleLogText.text = message; // �W��Text�ɕ\��
        await UniTask.Delay(System.TimeSpan.FromSeconds(waitSeconds));
    }
}
