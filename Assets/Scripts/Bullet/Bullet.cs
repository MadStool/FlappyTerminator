using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private int _damage = 100;
    [SerializeField] private LayerMask _targetLayer;

    private Vector3 _originalScale;
    private Vector2 _direction;
    private IBulletProvider _bulletProvider;
    private float _deathTime;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        _deathTime = Time.time + _lifetime;
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (Time.time >= _deathTime || IsVisibleFromCamera() == false)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _targetLayer) != 0)
        {
            if (other.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(_damage);
                ReturnToPool();
            }
        }
    }

    public void Initialize(IBulletProvider provider)
    {
        _bulletProvider = provider;
    }

    public void Configure(LayerMask targetLayer, Vector2 direction)
    {
        _targetLayer = targetLayer;
        _direction = direction.normalized;
    }

    private bool IsVisibleFromCamera()
    {
        if (_mainCamera == null)
            return false;

        var viewportPos = _mainCamera.WorldToViewportPoint(transform.position);

        return viewportPos.x >= -0.1f && viewportPos.x <= 1.1f &&
               viewportPos.y >= -0.1f && viewportPos.y <= 1.1f;
    }

    private void ReturnToPool()
    {
        transform.localScale = _originalScale;

        if (TryGetComponent<SpriteRenderer>(out var renderer))
        {
            renderer.flipX = false;
        }

        _bulletProvider?.ReturnBullet(this);
    }
}