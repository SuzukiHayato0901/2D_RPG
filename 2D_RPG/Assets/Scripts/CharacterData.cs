using UnityEngine;

/// <summary>
/// �L�����N�^�[�̏����X�e�[�^�X��ێ�����f�[�^�N���X�iScriptableObject�j
/// �o�g�����̏�����Actor�Ɉς˂�
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "RPG/�L�����N�^�[�f�[�^")]
public class CharacterData : ScriptableObject
{
    [Header("��{�X�e�[�^�X")]
    public string characterName; // �L������
    public int maxHp;            // �ő�HP
    public int atk;              // �U����
    public int def;              // �h���
    public int speed;            // �f����

    [Header("������")]
    public Sprite portrait;      // �����G�iUI�p�j
}
