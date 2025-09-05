using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Scene", menuName = "自作データ/シーン管理")]
public class SceneReference : ScriptableObject
{
    // タグ判定用
    public string triggerTag;

    // 実行時に使うシーン名（Build Settingsに登録されている必要あり）
    [SerializeField, HideInInspector] public string sceneName;
    public string SceneName => sceneName;

    // エディタ専用のシーン参照（ビルドには含まれない）
    [SerializeField] private SceneAsset sceneAsset;

    // シーンアセットが設定されたら、その名前を保存
    private void OnValidate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;
    }
}
