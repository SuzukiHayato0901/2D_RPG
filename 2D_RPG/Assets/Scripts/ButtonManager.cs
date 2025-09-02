using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject�x�[�X�̊K�w�^�R�}���h�{�^�������N���X
/// - CommandData[] �����Ƀ{�^���𓮓I����
/// - �q�R�}���h������ꍇ�͊K�w�J��
/// - �߂�{�^���ŏ�ʊK�w�ɖ߂��
/// </summary>
public class ButtonManager : MonoBehaviour
{
    [Header("UI�ݒ�")]
    public GameObject buttonPrefab;   // �{�^���̃v���n�u�iText�t���j
    public Transform buttonContainer; // �{�^������ׂ�e�I�u�W�F�N�g
    public Font customFont;           // �C�ӂ̃t�H���g�i���{��Ή��Ȃǁj

    [Header("�R�}���h�K�w�f�[�^")]
    [SerializeField] private CommandData[] rootCommands; // �ŏ�ʊK�w�̃R�}���h�Q�iSO�Q�Ɓj

    private Action<string> onCommandSelected; // �R�}���h�m�莞�ɌĂяo���R�[���o�b�N
    private Stack<CommandData[]> menuStack = new Stack<CommandData[]>(); // �K�w�����i�߂�p�j

    /// <summary>
    /// �R�}���h�{�^�������̃G���g���[�|�C���g
    /// </summary>
    /// <param name="callback">���[�R�}���h�I�����ɌĂяo������</param>
    public void GenerateCommandButtons(Action<string> callback)
    {
        onCommandSelected = callback;

        // �^�[���J�n���ɊK�w���������Z�b�g
        menuStack.Clear();

        ShowCommands(rootCommands); // ���[�g�K�w��\��
    }

    /// <summary>
    /// �w�肳�ꂽ�K�w�̃R�}���h��\��
    /// </summary>
    /// <param name="commands">�\������R�}���h�z��</param>
    private void ShowCommands(CommandData[] commands)
    {
        // �����{�^����S�폜�i�K�w�J�ڎ��ɑO�̊K�w�̃{�^���������j
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        // �߂�{�^���i���[�g�K�w�ȊO�̏ꍇ�̂ݕ\���j
        if (menuStack.Count > 0)
        {
            CreateButton("�� �߂�", () =>
            {
                // 1��̊K�w�ɖ߂�
                ShowCommands(menuStack.Pop());
            });
        }

        // �e�R�}���h�{�^���𐶐�
        foreach (var cmd in commands)
        {
            CreateButton(cmd.label, () =>
            {
                // �q�R�}���h������ꍇ�͊K�w�J��
                if (cmd.subCommands != null && cmd.subCommands.Length > 0)
                {
                    menuStack.Push(commands); // ���݂̊K�w�𗚗��ɕۑ�
                    ShowCommands(cmd.subCommands);
                }
                else
                {
                    // ���[�R�}���h �� �R�[���o�b�N���s
                    OnCommandSelected(cmd.label);
                }
            });
        }
    }

    /// <summary>
    /// �{�^���������ʏ���
    /// </summary>
    /// <param name="label">�{�^���ɕ\�����镶����</param>
    /// <param name="onClick">�N���b�N���̏���</param>
    private void CreateButton(string label, Action onClick)
    {
        // �v���n�u����{�^������
        GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
        Button button = buttonObj.GetComponent<Button>();
        Text buttonText = buttonObj.GetComponentInChildren<Text>();

        // �\���e�L�X�g�ݒ�
        buttonText.text = label;
        if (customFont) buttonText.font = customFont;

        // �N���b�N�C�x���g�o�^
        button.onClick.AddListener(() => onClick());
    }

    /// <summary>
    /// �R�}���h�m�莞�̏���
    /// </summary>
    /// <param name="commandName">�I�����ꂽ�R�}���h��</param>
    private void OnCommandSelected(string commandName)
    {
        // �O���ɒʒm
        onCommandSelected?.Invoke(commandName);

        // �S�{�^���𖳌����i�A�Ŗh�~�j
        foreach (Transform child in buttonContainer)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null) btn.interactable = false;
        }
    }
}
