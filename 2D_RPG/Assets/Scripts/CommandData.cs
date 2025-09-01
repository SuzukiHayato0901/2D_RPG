using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 階層型コマンドのデータ定義
/// 各階層を別アセットとして管理することで、
///  Unityのシリアライズ深度制限や循環参照のリスクを回避
/// </summary>

[CreateAssetMenu(fileName = "CommandData", menuName = "RPG/コマンドデータ")]
public class CommandData : ScriptableObject
{
    public string label;                // 表示名
    public CommandData[] subCommands;   // 子コマンド
}
