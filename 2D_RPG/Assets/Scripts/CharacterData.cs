using UnityEngine;

/// <summary>
/// �L�����N�^�[�̏����X�e�[�^�X���`����f�[�^�N���X
/// �C���X�y�N�^�[�Őݒ�\
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "ROG/�L�����N�^�[�f�[�^")]
public class CharacterData : ScriptableObject
{
    [Header("��{�X�e�[�^�X")]
    public string characterName;
    public int maxHp;
    public int atk;
    public int def;
    public int speed;

    [Header("������")]
    public Sprite portrait;
}
