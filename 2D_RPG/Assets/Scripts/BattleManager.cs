using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;

/// <summary>
/// �^�[�����o�g���̐i�s�Ǘ��N���X�B
/// - CharacterData�̃X�e�[�^�X���g���ă_���[�W�v�Z
/// - BattleMessageData�̃e���v���[�g���g���ĕ��͐���
/// - BattleLogManager�ŕ����s����\��
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("�L�����f�[�^")]
    [SerializeField] private CharacterData playerData; // �v���C���[�̃X�e�[�^�X
    [SerializeField] private CharacterData enemyData;  // �G�̃X�e�[�^�X

    [Header("UI")]
    [SerializeField] private ButtonManager buttonManager; // �R�}���h����UI
    [SerializeField] private BattleLogManager logManager; // ���O�\���Ǘ�

    [Header("���b�Z�[�W�f�[�^")]
    [SerializeField] private BattleMessageData selectCommandMsg; // "{0} �� {1} ��I���I"
    [SerializeField] private BattleMessageData damageMsg;        // "{0} �� {1} �_���[�W�I"
    [SerializeField] private BattleMessageData enemyTurnMsg;     // "�G�̃^�[���c"



    private bool isBattleActive;

    private async void Start()
    {
        isBattleActive = true;
        await logManager.AddLogAsync("�o�g���J�n�I");

        // �o�g�����[�v
        while (isBattleActive)
        {
            await PlayerTurnAsync();
            if (!enemyData.IsAlive)
            {
                await logManager.AddLogAsync($"{enemyData.characterName} ��|�����I");
                break;
            }

            await EnemyTurnAsync();
            if (!playerData.IsAlive)
            {
                await logManager.AddLogAsync($"{playerData.characterName} ���|���ꂽ�c");
                break;
            }
        }

        await logManager.AddLogAsync("�o�g���I���I");
        isBattleActive = false;
    }

    /// <summary>
    /// �v���C���[�̃^�[������
    /// </summary>
    private async UniTask PlayerTurnAsync()
    {
        await logManager.AddLogAsync($"{playerData.characterName} �̃^�[��");

        // �R�}���h���͑҂�
        var tcs = new UniTaskCompletionSource<string>();
        buttonManager.GenerateCommandButtons(cmd => tcs.TrySetResult(cmd));

        string selectedCommand = await tcs.Task;
        await logManager.AddLogAsync(string.Format(selectCommandMsg.messageTemplate, playerData.characterName, selectedCommand));

        // �R�}���h�ʏ���
        if (selectedCommand == "��������")
        {
            int damage = CalcDamage(playerData, enemyData);
            enemyData.TakeDamage(damage);
            await logManager.AddLogAsync(string.Format(damageMsg.messageTemplate, enemyData.characterName, damage));
        }
        else if (selectedCommand == "�܂ق�")
        {
            int damage = CalcDamage(playerData, enemyData) + 5;
            enemyData.TakeDamage(damage);
            await logManager.AddLogAsync(string.Format(damageMsg.messageTemplate, enemyData.characterName, damage));
        }
        else if (selectedCommand == "�ɂ���")
        {
            await logManager.AddLogAsync("�������I");
            isBattleActive = false;
        }
    }

    /// <summary>
    /// �G�̃^�[������
    /// </summary>
    private async UniTask EnemyTurnAsync()
    {
        await logManager.AddLogAsync(enemyTurnMsg.messageTemplate);

        int damage = CalcDamage(enemyData, playerData);
        playerData.TakeDamage(damage);
        await logManager.AddLogAsync(string.Format(damageMsg.messageTemplate, playerData.characterName, damage));
    }

    /// <summary>
    /// �_���[�W�v�Z�i�Œ�1�_���[�W�ۏ؁j
    /// </summary>
    private int CalcDamage(CharacterData attacker, CharacterData defender)
    {
        return Mathf.Max(1, attacker.atk - defender.def + UnityEngine.Random.Range(-2, 3));
    }
}
