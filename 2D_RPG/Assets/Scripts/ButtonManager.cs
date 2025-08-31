using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonManager : MonoBehaviour
{
    [Header("UI�ݒ�")]
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public Font customFont;

    private Action<string> onCommandSelected;

    private string[] commands = { "��������",  "�ɂ���" };

    public void GenerateCommandButtons(Action<string> callback)
    {
        onCommandSelected = callback;

        // �����{�^���폜
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // �R�}���h���ƂɃ{�^������
        foreach (string command in commands)
        {
            string cmd = command; // �L���v�`���΍�
            GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
            Button button = buttonObj.GetComponent<Button>();
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            buttonText.text = cmd;
            if (customFont) buttonText.font = customFont;

            button.onClick.AddListener(() => OnCommandSelected(cmd));
        }
    }

    private void OnCommandSelected(string commandName)
    {
        onCommandSelected?.Invoke(commandName);

        // �S�{�^���𖳌���
        foreach (Transform child in buttonContainer)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null) btn.interactable = false;
        }
    }
}
