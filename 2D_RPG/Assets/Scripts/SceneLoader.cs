using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// UniTask�ŃV�[���J�ڂ��Ǘ�����N���X�B
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvas; // �t�F�[�h�p
    [SerializeField] private float fadeDuration = 0.5f;

    /// <summary>
    /// �������[�h
    /// </summary>
    public void LoadScene(SceneReference sceneRef)
    {
        SceneManager.LoadScene(sceneRef.SceneName);
    }

    /// <summary>
    /// �񓯊����[�h�i�t�F�[�h�t���j
    /// </summary>
    public async UniTaskVoid LoadSceneAsync(SceneReference sceneRef, CancellationToken token = default)
    {
        // �t�F�[�h�A�E�g
        if (fadeCanvas != null)
            await Fade(1f, token);

        // �񓯊����[�h�J�n
        var op = SceneManager.LoadSceneAsync(sceneRef.SceneName);
        op.allowSceneActivation = false;

        // �ǂݍ��݊����܂őҋ@
        while (op.progress < 0.9f)
            await UniTask.Yield(PlayerLoopTiming.Update, token);

        // �V�[���؂�ւ�
        op.allowSceneActivation = true;

        // �t�F�[�h�C��
        if (fadeCanvas != null)
            await Fade(0f, token);
    }

    /// <summary>
    /// �t�F�[�h����
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
