using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CharacterView : MonoBehaviour
{
    [Header("�f�[�^��UI")]
    [SerializeField] private CharacterData characterData;   // �L�����̏����f�[�^
    [SerializeField] private Image portraitImage;           // �摜
    [SerializeField] private Text nameText;                 // ���O�\��
    [SerializeField] private Text hpText;                   // HP�\��

    public Actor Actor { get; private set; }        // ���W�b�N�N���X

    private void Awake()
    {
        // �f�[�^����Actor�𐶐�
        Actor = ActorFactory.Create(characterData);

        // UI������
        nameText.text = Actor.Name;
        portraitImage.sprite = characterData.portrait;
        hpText.text = $"HP: {Actor.Hp.Value} / {Actor.MaxHp}";


        // HP���ω�������e�L�X�g���X�V
        Actor.Hp.Subscribe(hp =>
        {
            hpText.text = $"HP: {hp} / {Actor.MaxHp}";
        })
            .AddTo(this);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
