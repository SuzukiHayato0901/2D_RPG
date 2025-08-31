using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharacterView : MonoBehaviour
{
    [Header("データとUI")]
    [SerializeField] private CharacterData characterData;   // 敵の初期データ
    [SerializeField] private Image portraitImage;           // 敵の画像

    public Actor Actor { get; private set; } // ロジッククラス

    private void Awake()
    {
        // データからActorを生成
        Actor = ActorFactory.Create(characterData);

        // 画像設定（nullチェック付き）
        if (portraitImage != null && characterData.portrait != null)
        {
            portraitImage.sprite = characterData.portrait;
        }
    }
}