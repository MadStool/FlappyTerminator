using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _shootInterval = 2f;
    [SerializeField] private float _initialDelay = 1f;
    [SerializeField] private bool _flipBulletSprite = true;
    [SerializeField] private LayerMask _bulletTargetLayer;

    private IBulletProvider _bulletProvider;
    private float _timer;

    public void Initialize(IBulletProvider provider)
    {
        _bulletProvider = provider;
    }

    private void Awake()
    {
        _timer = _shootInterval - _initialDelay;
    }

    private void Update()
    {
        if (enabled == false || _bulletProvider == null)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _shootInterval)
        {
            Shoot();
            _timer = 0f;
        }
    }

    private void Shoot()
    {
        Bullet bullet = _bulletProvider.GetBullet();

        if (bullet == null)
            return;

        bullet.transform.SetPositionAndRotation(
            _firePoint.position,
            Quaternion.identity
        );

        if (_flipBulletSprite && bullet.TryGetComponent<SpriteRenderer>(out var renderer))
        {
            renderer.flipX = true;
        }

        bullet.Configure(_bulletTargetLayer, Vector2.left);
    }
}