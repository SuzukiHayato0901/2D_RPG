using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/// <summary>
/// ScriptableObjectベースの階層型コマンドボタン生成クラス
/// - CommandData[] を元にボタンを動的生成
/// - 子コマンドがある場合は階層遷移
/// - 戻るボタンで上位階層に戻れる
/// </summary>
public class ButtonManager : MonoBehaviour
{
    [Header("UI設定")]
    public GameObject buttonPrefab;   // ボタンのプレハブ（Text付き）
    public Transform buttonContainer; // ボタンを並べる親オブジェクト
    public Font customFont;           // 任意のフォント（日本語対応など）

    [Header("コマンド階層データ")]
    [SerializeField] private CommandData[] rootCommands; // 最上位階層のコマンド群（SO参照）

    private Action<string> onCommandSelected; // コマンド確定時に呼び出すコールバック
    private Stack<CommandData[]> menuStack = new Stack<CommandData[]>(); // 階層履歴（戻る用）

    /// <summary>
    /// コマンドボタン生成のエントリーポイント
    /// </summary>
    /// <param name="callback">末端コマンド選択時に呼び出す処理</param>
    public void GenerateCommandButtons(Action<string> callback)
    {
        onCommandSelected = callback;

        // ターン開始時に階層履歴をリセット
        menuStack.Clear();

        ShowCommands(rootCommands); // ルート階層を表示
    }

    /// <summary>
    /// 指定された階層のコマンドを表示
    /// </summary>
    /// <param name="commands">表示するコマンド配列</param>
    private void ShowCommands(CommandData[] commands)
    {
        // 既存ボタンを全削除（階層遷移時に前の階層のボタンを消す）
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        // 戻るボタン（ルート階層以外の場合のみ表示）
        if (menuStack.Count > 0)
        {
            CreateButton("＜ 戻る", () =>
            {
                // 1つ上の階層に戻る
                ShowCommands(menuStack.Pop());
            });
        }

        // 各コマンドボタンを生成
        foreach (var cmd in commands)
        {
            CreateButton(cmd.label, () =>
            {
                // 子コマンドがある場合は階層遷移
                if (cmd.subCommands != null && cmd.subCommands.Length > 0)
                {
                    menuStack.Push(commands); // 現在の階層を履歴に保存
                    ShowCommands(cmd.subCommands);
                }
                else
                {
                    // 末端コマンド → コールバック実行
                    OnCommandSelected(cmd.label);
                }
            });
        }
    }

    /// <summary>
    /// ボタン生成共通処理
    /// </summary>
    /// <param name="label">ボタンに表示する文字列</param>
    /// <param name="onClick">クリック時の処理</param>
    private void CreateButton(string label, Action onClick)
    {
        // プレハブからボタン生成
        GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
        Button button = buttonObj.GetComponent<Button>();
        Text buttonText = buttonObj.GetComponentInChildren<Text>();

        // 表示テキスト設定
        buttonText.text = label;
        if (customFont) buttonText.font = customFont;

        // クリックイベント登録
        button.onClick.AddListener(() => onClick());
    }

    /// <summary>
    /// コマンド確定時の処理
    /// </summary>
    /// <param name="commandName">選択されたコマンド名</param>
    private void OnCommandSelected(string commandName)
    {
        // 外部に通知
        onCommandSelected?.Invoke(commandName);

        // 全ボタンを無効化（連打防止）
        foreach (Transform child in buttonContainer)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null) btn.interactable = false;
        }
    }
}
