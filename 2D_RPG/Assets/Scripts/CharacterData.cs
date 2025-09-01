using UnityEngine;

/// <summary>
/// �L�����N�^�[�̃X�e�[�^�X�ƃo�g���p�̊�{�������`����f�[�^�N���X
/// �C���X�y�N�^�[�Őݒ�\
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
    public Sprite portrait;      // �����G

    // ����HP�i�o�g�����ɕϓ��j
    [HideInInspector] public int currentHp;

    /// <summary>
    /// ��������iHP��0���傫����ΐ����j
    /// </summary>
    public bool IsAlive => currentHp > 0;

    /// <summary>
    /// �_���[�W���󂯂�HP�����炷�i�Œ�0�܂Łj
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    /// <summary>
    /// HP��S�񕜁i�o�g���J�n���ȂǂɎg�p�j
    /// </summary>
    public void HealFull()
    {
        currentHp = maxHp;
    }
}
