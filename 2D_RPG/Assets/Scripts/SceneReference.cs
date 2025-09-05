using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Scene", menuName = "����f�[�^/�V�[���Ǘ�")]
public class SceneReference : ScriptableObject
{
    // �^�O����p
    public string triggerTag;

    // ���s���Ɏg���V�[�����iBuild Settings�ɓo�^����Ă���K�v����j
    [SerializeField, HideInInspector] public string sceneName;
    public string SceneName => sceneName;

    // �G�f�B�^��p�̃V�[���Q�Ɓi�r���h�ɂ͊܂܂�Ȃ��j
    [SerializeField] private SceneAsset sceneAsset;

    // �V�[���A�Z�b�g���ݒ肳�ꂽ��A���̖��O��ۑ�
    private void OnValidate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;
    }
}
