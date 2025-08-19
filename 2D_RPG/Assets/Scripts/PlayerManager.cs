using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region �v���C�x�[�g�ϐ�
    private PlayerAction controls;      // InputSystem�Ő��������A�N�V�����N���X
    private Vector2 moveInput;          // ���͂��ꂽ����
    [SerializeField] private float speed;           // �ړ����x
    [SerializeField] private LayerMask ObjctUI;    // �Փ˔�����s���Ώۃ��C���[

    private Animator animator;          // Animator�R���|�[�l���g
    #endregion

    #region Unity�C�x���g
    private void Awake()
    {
        controls = new PlayerAction();                 // InputAction������
        animator = GetComponent<Animator>();          // Animator�擾
    }

    private void OnEnable()
    {
        controls.Player.Move.Enable();                        // ���͎�t�J�n
        controls.Player.Move.performed += OnMovePerformed;   // ���͎��C�x���g�o�^
        controls.Player.Move.canceled += OnMoveCanceled;     // ���������C�x���g�o�^
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMovePerformed;  // �C�x���g����
        controls.Player.Move.canceled -= OnMoveCanceled;    // �C�x���g����
        controls.Player.Move.Disable();                     // ���͎�t�I��
    }

    private void Update()
    {
        Move();    // ���t���[���ړ�����
    }
    #endregion

    // �ړ�����
    private void Move()
    {
        // �E���D���Y���͂𖳌���
        if (moveInput.x != 0) moveInput.y = 0;

        // �ړ��\��ʒu���v�Z
        Vector3 targetPos = transform.position + new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime;

        // �ړ��ł��邩����iObjctUI�ɂԂ���Ȃ�ړ����Ȃ��j
        if (!IsWalkable(targetPos))
        {
            animator.SetBool("IsMoving", false); // �ړ��ł��Ȃ���΃A�j���[�V�������~�߂�
            return;                               // �ړ������I��
        }

        // ���ۂɈړ�
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime, Space.World);

        // �A�j���[�V�����X�V
        bool IsMoving = moveInput.sqrMagnitude > 0f;  // ���͂����邩�Ŕ���
        animator.SetFloat("InputX", moveInput.x);    // Animator��X�������͂�n��
        animator.SetFloat("InputY", moveInput.y);    // Animator��Y�������͂�n��
        animator.SetBool("IsMoving", IsMoving);      // Animator�Ɉړ������ǂ�����n��
    }

    // ���̓C�x���g
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();  // ���͂��ꂽ�������擾
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;                  // ���͂����ꂽ����������Z�b�g
    }

    // �Փ˔���֐�
    private bool IsWalkable(Vector3 targetPos)
    {
        // targetPos�𒆐S�ɔ��a0.1��OverlapCircle���쐬
        // ObjctUI���C���[�ɂԂ���ꍇ��false��Ԃ�
        return Physics2D.OverlapCircle(targetPos, 0.1f, ObjctUI) == false;
    }
}
