using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharacterView : MonoBehaviour
{
    [Header("�f�[�^��UI")]
    [SerializeField] private CharacterData characterData;   // �G�̏����f�[�^
    [SerializeField] private Image portraitImage;           // �G�̉摜

    public Actor Actor { get; private set; } // ���W�b�N�N���X

    private void Awake()
    {
        // �f�[�^����Actor�𐶐�
        Actor = ActorFactory.Create(characterData);

        // �摜�ݒ�inull�`�F�b�N�t���j
        if (portraitImage != null && characterData.portrait != null)
        {
            portraitImage.sprite = characterData.portrait;
        }
    }
}