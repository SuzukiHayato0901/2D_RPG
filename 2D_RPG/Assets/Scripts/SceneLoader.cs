using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// UniTaskでシーン遷移を管理するクラス。
/// </summary>

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference sceneReference;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // タグが一致したらシーン遷移
        if (other.CompareTag(sceneReference.triggerTag))
        {
            SceneManager.LoadScene(sceneReference.sceneName);
        }
    }
}
