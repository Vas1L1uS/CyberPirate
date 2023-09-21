using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _speedRotate;

    private PlayerInput _playerInput;
    private Vector2 _previousDirection;
    private Coroutine _rotateCoroutine;

    private void Start()
    {
        _playerInput = _playerController.Input;
    }


    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector2 direction = _playerInput.Player.Movement.ReadValue<Vector2>();

        if (direction == Vector2.zero) return;
        if (direction == _previousDirection) return;

        _previousDirection = direction;

        float targetRotationY = Vector2.Angle(Vector2.up, direction);

        if (direction.x < 0) targetRotationY *= -1;

        float myRotationY = this.transform.rotation.eulerAngles.y;
        float angle = GetAngle(myRotationY, targetRotationY);

        if (_rotateCoroutine != null) StopCoroutine(_rotateCoroutine);

        _rotateCoroutine = StartCoroutine(GoRotate(angle, targetRotationY, _speedRotate));
    }

    private float GetAngle(float myRotation, float targetRotation)
    {
        float result = targetRotation - myRotation;

        if (result < -180)
        {
            result = 360 + result;
        }
        else if (result > 180)
        {
            result = (360 - result) * -1;
        }
        
        return result;
    }

    private IEnumerator GoRotate(float angle, float targetRotation, float speedRotate)
    {
        float currentRotate = 0;

        if (angle < 0) speedRotate *= -1;

        while (currentRotate < Mathf.Abs(angle))
        {
            this.transform.Rotate(new Vector3(0, speedRotate, 0) * Time.deltaTime);
            currentRotate += _speedRotate * Time.deltaTime;
            yield return null;
        }

        this.transform.rotation = Quaternion.Euler(0, targetRotation, 0);
    }
}