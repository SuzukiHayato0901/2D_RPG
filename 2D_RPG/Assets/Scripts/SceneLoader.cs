using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// UniTaskでシーン遷移を管理するクラス。
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvas; // フェード用
    [SerializeField] private float fadeDuration = 0.5f;

    /// <summary>
    /// 同期ロード
    /// </summary>
    public void LoadScene(SceneReference sceneRef)
    {
        SceneManager.LoadScene(sceneRef.SceneName);
    }

    /// <summary>
    /// 非同期ロード（フェード付き）
    /// </summary>
    public async UniTaskVoid LoadSceneAsync(SceneReference sceneRef, CancellationToken token = default)
    {
        // フェードアウト
        if (fadeCanvas != null)
            await Fade(1f, token);

        // 非同期ロード開始
        var op = SceneManager.LoadSceneAsync(sceneRef.SceneName);
        op.allowSceneActivation = false;

        // 読み込み完了まで待機
        while (op.progress < 0.9f)
            await UniTask.Yield(PlayerLoopTiming.Update, token);

        // シーン切り替え
        op.allowSceneActivation = true;

        // フェードイン
        if (fadeCanvas != null)
            await Fade(0f, token);
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    private async UniTask Fade(float targetAlpha, CancellationToken token)
    {
        float startAlpha = fadeCanvas.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }
}
