using UnityEngine;
using UniRx;

/// <summary>
/// バトル中のステータスと処理を管理するロジッククラス（UI非依存）
/// </summary>
public class Actor
{
    public string Name { get; private set; }
    public int MaxHp { get; private set; }
    public ReactiveProperty<int> Hp { get; private set; } // HPのリアクティブプロパティ
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int Speed { get; private set; }

    public bool IsAlive => Hp.Value > 0; // HPが0より大きければ生存

    public Actor(string name, int maxHp, int atk, int def, int speed)
    {
        Name = name;
        MaxHp = maxHp;
        Hp = new ReactiveProperty<int>(maxHp); // 初期HPを設定
        Atk = atk;
        Def = def;
        Speed = speed;
    }

    /// <summary>
    /// 計算済みのダメージを受け取ってHPを減らす
    /// </summary>
    public void TakeDamage(int finalDamage)
    {
        Hp.Value = Mathf.Max(0, Hp.Value - finalDamage);
    }

    /// <summary>
    /// HPを回復（最大HPを超えない）
    /// </summary>
    public void Heal(int amount)
    {
        Hp.Value = Mathf.Min(MaxHp, Hp.Value + amount);
    }

    /// <summary>
    /// HPを全回復
    /// </summary>
    public void Reset()
    {
        Hp.Value = MaxHp;
    }
}
