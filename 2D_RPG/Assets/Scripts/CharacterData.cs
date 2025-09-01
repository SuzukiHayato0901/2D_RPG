using UnityEngine;

/// <summary>
/// キャラクターのステータスとバトル用の基本処理を定義するデータクラス
/// インスペクターで設定可能
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "RPG/キャラクターデータ")]
public class CharacterData : ScriptableObject
{
    [Header("基本ステータス")]
    public string characterName; // キャラ名
    public int maxHp;            // 最大HP
    public int atk;              // 攻撃力
    public int def;              // 防御力
    public int speed;            // 素早さ

    [Header("見た目")]
    public Sprite portrait;      // 立ち絵

    // 現在HP（バトル中に変動）
    [HideInInspector] public int currentHp;

    /// <summary>
    /// 生存判定（HPが0より大きければ生存）
    /// </summary>
    public bool IsAlive => currentHp > 0;

    /// <summary>
    /// ダメージを受けてHPを減らす（最低0まで）
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    /// <summary>
    /// HPを全回復（バトル開始時などに使用）
    /// </summary>
    public void HealFull()
    {
        currentHp = maxHp;
    }
}
