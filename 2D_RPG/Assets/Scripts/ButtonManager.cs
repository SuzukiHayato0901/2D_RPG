using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Text mainText;              // 選択結果表示用
    public GameObject buttonPrefab;    // ボタンのプレハブ
    public GameObject buttonSelection; // 配置する親オブジェクト

    // コマンドの種類をenumで定義
    public enum CommandType
    {
        たたかう,
        にげる
    }

    void Start()
    {
        // enum の全要素を列挙
        foreach (CommandType cmd in Enum.GetValues(typeof(CommandType)))
        {
            // ボタン生成
            GameObject newButton = Instantiate(buttonPrefab, buttonSelection.transform, false);

            // ラベル設定（enumの値を文字列化）
            Text label = newButton.GetComponentInChildren<Text>();
            label.text = cmd.ToString();

            // クリック時イベント登録
            newButton.GetComponent<Button>().onClick.AddListener(() => OnCommandSelected(cmd));
        }
    }

    void OnCommandSelected(CommandType command)
    {
        mainText.text = command.ToString() + " を選びました";
    }
}
