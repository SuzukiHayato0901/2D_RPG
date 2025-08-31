using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonManager : MonoBehaviour
{
    [Header("UI設定")]
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public Font customFont;

    private Action<string> onCommandSelected;

    private string[] commands = { "たたかう",  "にげる" };

    public void GenerateCommandButtons(Action<string> callback)
    {
        onCommandSelected = callback;

        // 既存ボタン削除
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // コマンドごとにボタン生成
        foreach (string command in commands)
        {
            string cmd = command; // キャプチャ対策
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

        // 全ボタンを無効化
        foreach (Transform child in buttonContainer)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null) btn.interactable = false;
        }
    }
}
