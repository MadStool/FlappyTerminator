using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _shootCooldown = 0.5f;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private bool _continuousFire = true;
    [SerializeField] private LayerMask _bulletTargetLayer;

    private IBulletProvider _bulletProvider;
    private float _lastShootTime;

    private void Awake()
    {
        _bulletProvider = _bulletPool;

        if (_firePoint == null)
        {
            _firePoint = transform;
            Debug.LogWarning("FirePoint not assigned, using player position", this);
        }
    }

    private void Update()
    {
        if (_bulletProvider == null)
            return;

        bool fireInput = _continuousFire ?
            Input.GetKey(KeyCode.E) :
            Input.GetKeyDown(KeyCode.E);

        if (fireInput)
            TryShoot();
    }

    private void TryShoot()
    {
        if (Time.time - _lastShootTime < _shootCooldown)
            return;

        _lastShootTime = Time.time;
        Shoot();
    }

    private void Shoot()
    {
        Bullet bullet = _bulletProvider.GetBullet();

        if (bullet == null)
            return;

        bullet.transform.SetPositionAndRotation(
            _firePoint.position,
            _firePoint.rotation
        );

        bullet.Configure(_bulletTargetLayer, transform.right * transform.localScale.x);
    }
}