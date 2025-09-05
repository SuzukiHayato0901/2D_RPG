using UnityEngine;

/// <summary>
/// キャラクターの初期ステータスを保持するデータクラス（ScriptableObject）
/// バトル中の処理はActorに委ねる
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
    public Sprite portrait;      // 立ち絵（UI用）
}
