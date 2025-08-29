using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CharacterView : MonoBehaviour
{
    [Header("データとUI")]
    [SerializeField] private CharacterData characterData;   // キャラの初期データ
    [SerializeField] private Image portraitImage;           // 画像
    [SerializeField] private Text nameText;                 // 名前表示
    [SerializeField] private Text hpText;                   // HP表示

    public Actor Actor { get; private set; }        // ロジッククラス

    private void Awake()
    {
        // データからActorを生成
        Actor = ActorFactory.Create(characterData);

        // UI初期化
        nameText.text = Actor.Name;
        portraitImage.sprite = characterData.portrait;
        hpText.text = $"HP: {Actor.Hp.Value} / {Actor.MaxHp}";


        // HPが変化したらテキストを更新
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
