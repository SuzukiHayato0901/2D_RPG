using UnityEngine;

/// <summary>
/// �v���C���[��ǂ�������J��������X�N���v�g
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // �Ǐ]�Ώہi�ʏ�̓v���C���[�j
    [SerializeField] private float followSpeed = 5f; // �J�����̒Ǐ]���x

    private void LateUpdate()
    {
        // �^�[�Q�b�g���ݒ肳��Ă��Ȃ���Ή������Ȃ�
        if (target == null) return;

        // �Ǐ]����ʒu�iZ���W�̓J�����̂܂܁j
        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        // ���`��ԂŃX���[�Y�ɒǏ]
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// �O������^�[�Q�b�g�i�Ǐ]��j��ݒ�ł���悤�ɂ���
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
