using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _gravityScale = 2f;
    [SerializeField] private float _maxDownAngle = -30f;
    [SerializeField] private float _maxUpAngle = 30f;
    [SerializeField] private float _rotationSpeed = 5f;

    private bool _isDead = false;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravityScale;
    }

    private void Update()
    {
        if (_isDead) 
             return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        RotateBird();
    }

    public void DisableControl()
    {
        _isDead = true;
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.gravityScale = 0f;
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = Vector2.up * _jumpForce;
    }

    private void RotateBird()
    {
        float targetAngle;

        if (_rigidbody.linearVelocity.y > 0)
        {
            targetAngle = Mathf.Lerp(0, _maxUpAngle, _rigidbody.linearVelocity.y / _jumpForce);
        }
        else
        {
            targetAngle = Mathf.Lerp(0, _maxDownAngle, -_rigidbody.linearVelocity.y / _jumpForce);
        }

        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}