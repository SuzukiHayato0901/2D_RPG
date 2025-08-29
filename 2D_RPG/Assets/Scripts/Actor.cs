using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// �퓬���̃X�e�[�^�X�⏈�����Ǘ����郍�W�b�N�N���X
/// UI��Unity�ˑ��Ȃ�
/// </summary>
public class Actor
{
    public string Name { get; private set; }
    public int MaxHp { get; private set; }
    public ReactiveProperty<int> Hp { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int Speed { get; private set; }

    public bool IsAlive => Hp.Value > 0;

    public Actor(string name, int maxHp, int atk, int def, int speed)
    {
        Name = name;
        MaxHp = maxHp;
        Hp = new ReactiveProperty<int>(maxHp);
        Atk = atk;
        Def = def;
        Speed = speed;
    }

    public void TakeDamage(int rawDamage)
    {
        int damage = Mathf.Max(1, rawDamage - Def);
        Hp.Value = Mathf.Max(0, Hp.Value - damage);
    }

    public void Heal(int amount)
    {
        Hp.Value = Mathf.Min(MaxHp, Hp.Value + amount);
    }

    public void BoostAttack(int amount)
    {
        Atk += amount;
    }

    public void BoostDefense(int amount)
    {
        Def += amount;
    }

    public void Reset()
    {
        Hp.Value = MaxHp;
    }
}
