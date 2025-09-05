using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// プレイヤーキャラのUI表示とActorの接続
/// HP変化を監視してUIを更新
/// </summary>
public class CharacterView : MonoBehaviour
{
    [Header("データとUI")]
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text hpText;

    public Actor Actor { get; private set; }

    private void Awake()
    {
        // データからActorを生成
        Actor = ActorFactory.Create(characterData);

        // UI初期化
        nameText.text = Actor.Name;
        hpText.text = $"HP: {Actor.Hp.Value} / {Actor.MaxHp}";

        // HPが変化したらUIを更新
        Actor.Hp.Subscribe(hp =>
        {
            hpText.text = $"HP: {hp} / {Actor.MaxHp}";
        }).AddTo(this);
    }
}
