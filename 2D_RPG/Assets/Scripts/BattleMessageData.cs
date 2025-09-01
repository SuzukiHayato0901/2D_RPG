using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "RPG/バトルメッセージデータ")]
public class BattleMessageData : ScriptableObject
{
    [TextArea]
    public string messageTemplate;

    // 例："{0} は {1} を選択"
    //     "{0} は {1} ダメージ"
}
