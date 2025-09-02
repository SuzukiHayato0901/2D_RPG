using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Scene", menuName = "����f�[�^/�V�[���Ǘ�")]
public class SceneReference : ScriptableObject
{

    // �G�f�B�^��p�̃V�[���Q�Ɓi�r���h�ɂ͊܂܂�Ȃ��j
    [SerializeField] private UnityEditor.SceneAsset sceneAsset;

    // SceneAsset���ݒ肳�ꂽ��A���̖��O��ۑ�
    private void OnValidate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;
    }

    // ���s���Ɏg���V�[�����i���ڕҏW�s�j
    [SerializeField, HideInInspector] private string sceneName;

    // �O������ǂݎ���p�Ŏ擾
    public string SceneName => sceneName;
}
