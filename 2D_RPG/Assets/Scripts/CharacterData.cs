using UnityEngine;

/// <summary>
/// キャラクターの初期ステータスを定義するデータクラス
/// インスペクターで設定可能
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "ROG/キャラクターデータ")]
public class CharacterData : ScriptableObject
{
    [Header("基本ステータス")]
    public string characterName;
    public int maxHp;
    public int atk;
    public int def;
    public int speed;

    [Header("見た目")]
    public Sprite portrait;
}
