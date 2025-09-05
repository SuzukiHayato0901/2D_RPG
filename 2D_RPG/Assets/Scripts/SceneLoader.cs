using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// UniTask�ŃV�[���J�ڂ��Ǘ�����N���X�B
/// </summary>

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference sceneReference;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �^�O����v������V�[���J��
        if (other.CompareTag(sceneReference.triggerTag))
        {
            SceneManager.LoadScene(sceneReference.sceneName);
        }
    }
}
