using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// �o�g�����O�̗���\�����Ǘ�����N���X�B
/// </summary>
public class BattleLogManager : MonoBehaviour
{
    [SerializeField] private Transform logContainer; // ���O����ׂ�e�iContent�j
    [SerializeField] private GameObject logTextPrefab; // 1�s���̃e�L�X�g�v���n�u
    [SerializeField] private ScrollRect scrollRect; // �����X�N���[���p

    /// <summary>
    /// ���O��1�s�ǉ����Ďw��b���ҋ@
    /// </summary>
    public async UniTask AddLogAsync(string message, float waitSeconds = 1f)
    {
        // �V�����s�𐶐�
        var logObj = Instantiate(logTextPrefab, logContainer);
        var textComp = logObj.GetComponent<Text>();
        textComp.text = message;

        // �X�N���[������ԉ���
        await UniTask.Yield();
        scrollRect.verticalNormalizedPosition = 0f;

        // ���o�p�̑ҋ@
        await UniTask.Delay(TimeSpan.FromSeconds(waitSeconds));
    }
}
