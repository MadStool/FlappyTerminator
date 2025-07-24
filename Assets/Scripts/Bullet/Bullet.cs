using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private int _damage = 100;

    private Vector2 _direction;
    private IBulletProvider _bulletProvider;
    private float _deathTime;
    private bool _isPlayerBullet;

    public void Initialize(IBulletProvider provider)
    {
        _bulletProvider = provider;
    }

    public void Configure(bool playerBullet, Vector2 direction)
    {
        _isPlayerBullet = playerBullet;
        _direction = direction.normalized;
        _deathTime = Time.time + _lifetime;
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

    private bool IsVisibleFromCamera()
    {
        var camera = Camera.main;

        if (camera == null) 
            return false;

        var viewportPos = camera.WorldToViewportPoint(transform.position);

        return viewportPos.x >= -0.1f && viewportPos.x <= 1.1f &&
               viewportPos.y >= -0.1f && viewportPos.y <= 1.1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isPlayerBullet && other.TryGetComponent<Player>(out var playerComponent))
            return;

        if (_isPlayerBullet == false && other.TryGetComponent<Enemy>(out var enemyComponent))
            return;

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage, transform);
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        transform.localScale = Vector3.one;

        if (TryGetComponent<SpriteRenderer>(out var renderer))
        {
            renderer.flipX = false;
        }

        _bulletProvider?.ReturnBullet(this);
    }
}