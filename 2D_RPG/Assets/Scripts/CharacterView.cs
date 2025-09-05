using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// �v���C���[�L������UI�\����Actor�̐ڑ�
/// HP�ω����Ď�����UI���X�V
/// </summary>
public class CharacterView : MonoBehaviour
{
    [Header("�f�[�^��UI")]
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text hpText;

    public Actor Actor { get; private set; }

    private void Awake()
    {
        // �f�[�^����Actor�𐶐�
        Actor = ActorFactory.Create(characterData);

        // UI������
        nameText.text = Actor.Name;
        hpText.text = $"HP: {Actor.Hp.Value} / {Actor.MaxHp}";

        // HP���ω�������UI���X�V
        Actor.Hp.Subscribe(hp =>
        {
            hpText.text = $"HP: {hp} / {Actor.MaxHp}";
        }).AddTo(this);
    }
}
