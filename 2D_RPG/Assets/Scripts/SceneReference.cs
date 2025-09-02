using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Scene", menuName = "自作データ/シーン管理")]
public class SceneReference : ScriptableObject
{

    // エディタ専用のシーン参照（ビルドには含まれない）
    [SerializeField] private UnityEditor.SceneAsset sceneAsset;

    // SceneAssetが設定されたら、その名前を保存
    private void OnValidate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;
    }

    // 実行時に使うシーン名（直接編集不可）
    [SerializeField, HideInInspector] private string sceneName;

    // 外部から読み取り専用で取得
    public string SceneName => sceneName;
}
