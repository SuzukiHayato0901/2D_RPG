using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "RPG/�o�g�����b�Z�[�W�f�[�^")]
public class BattleMessageData : ScriptableObject
{
    [TextArea]
    public string messageTemplate;

    // ��F"{0} �� {1} ��I��"
    //     "{0} �� {1} �_���[�W"
}
