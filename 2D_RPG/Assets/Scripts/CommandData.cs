using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �K�w�^�R�}���h�̃f�[�^��`
/// �e�K�w��ʃA�Z�b�g�Ƃ��ĊǗ����邱�ƂŁA
///  Unity�̃V���A���C�Y�[�x������z�Q�Ƃ̃��X�N�����
/// </summary>

[CreateAssetMenu(fileName = "CommandData", menuName = "RPG/�R�}���h�f�[�^")]
public class CommandData : ScriptableObject
{
    public string label;                // �\����
    public CommandData[] subCommands;   // �q�R�}���h
}
