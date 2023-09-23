using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Vector2 Direction => _direction;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _speedRotate;
    [SerializeField] private LayerMask _layerMask;

    private Vector2 _direction;

    private void Update()
    {
        if (_playerController.Health.CurrentHealth == 0) return;

        Rotate(_playerController.MouseWorldOnGroundPos);
    }

    private void Rotate(Vector3 targetPos)
    {
        this.transform.LookAt(new Vector3(targetPos.x, this.transform.position.y, targetPos.z));

        var pos = this.transform.position;
        _direction = (new Vector2(targetPos.x, targetPos.z) - new Vector2(pos.x, pos.z)).normalized;
    }
}