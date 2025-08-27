using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Text mainText;              // �I�����ʕ\���p
    public GameObject buttonPrefab;    // �{�^���̃v���n�u
    public GameObject buttonSelection; // �z�u����e�I�u�W�F�N�g

    // �R�}���h�̎�ނ�enum�Œ�`
    public enum CommandType
    {
        ��������,
        �ɂ���
    }

    void Start()
    {
        // enum �̑S�v�f���
        foreach (CommandType cmd in Enum.GetValues(typeof(CommandType)))
        {
            // �{�^������
            GameObject newButton = Instantiate(buttonPrefab, buttonSelection.transform, false);

            // ���x���ݒ�ienum�̒l�𕶎��񉻁j
            Text label = newButton.GetComponentInChildren<Text>();
            label.text = cmd.ToString();

            // �N���b�N���C�x���g�o�^
            newButton.GetComponent<Button>().onClick.AddListener(() => OnCommandSelected(cmd));
        }
    }

    void OnCommandSelected(CommandType command)
    {
        mainText.text = command.ToString() + " ��I�т܂���";
    }
}
