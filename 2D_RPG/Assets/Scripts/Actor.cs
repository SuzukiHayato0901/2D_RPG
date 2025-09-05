using UnityEngine;
using UniRx;

/// <summary>
/// �o�g�����̃X�e�[�^�X�Ə������Ǘ����郍�W�b�N�N���X�iUI��ˑ��j
/// </summary>
public class Actor
{
    public string Name { get; private set; }
    public int MaxHp { get; private set; }
    public ReactiveProperty<int> Hp { get; private set; } // HP�̃��A�N�e�B�u�v���p�e�B
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int Speed { get; private set; }

    public bool IsAlive => Hp.Value > 0; // HP��0���傫����ΐ���

    public Actor(string name, int maxHp, int atk, int def, int speed)
    {
        Name = name;
        MaxHp = maxHp;
        Hp = new ReactiveProperty<int>(maxHp); // ����HP��ݒ�
        Atk = atk;
        Def = def;
        Speed = speed;
    }

    /// <summary>
    /// �v�Z�ς݂̃_���[�W���󂯎����HP�����炷
    /// </summary>
    public void TakeDamage(int finalDamage)
    {
        Hp.Value = Mathf.Max(0, Hp.Value - finalDamage);
    }

    /// <summary>
    /// HP���񕜁i�ő�HP�𒴂��Ȃ��j
    /// </summary>
    public void Heal(int amount)
    {
        Hp.Value = Mathf.Min(MaxHp, Hp.Value + amount);
    }

    /// <summary>
    /// HP��S��
    /// </summary>
    public void Reset()
    {
        Hp.Value = MaxHp;
    }
}
