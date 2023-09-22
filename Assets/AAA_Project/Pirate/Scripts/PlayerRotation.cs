using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Vector2 Direction => _direction;

    [SerializeField] private float _speedRotate;
    [SerializeField] private LayerMask _layerMask;

    private Vector2 _direction;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 200, _layerMask);
        Rotate(hit);
    }

    private void Rotate(RaycastHit hit)
    {
        var targetPos = hit.point;
        this.transform.LookAt(new Vector3(targetPos.x, this.transform.position.y, targetPos.z));

        var pos = this.transform.position;
        _direction = (new Vector2(targetPos.x, targetPos.z) - new Vector2(pos.x, pos.z)).normalized;
    }
}