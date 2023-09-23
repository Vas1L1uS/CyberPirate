using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterHealth Health;
    public PlayerAttackController MeleeAttack;
    public PlayerShootAttack ShootAttack;


    public PlayerInput Input => _playerInput;
    public Vector3 MouseWorldOnGroundPos;

    [SerializeField] private LayerMask _groundLayerMask;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 200, _groundLayerMask);
        MouseWorldOnGroundPos = hit.point;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}