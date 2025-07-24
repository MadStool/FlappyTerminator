using UnityEngine;

[DefaultExecutionOrder(-100)]
public class BulletPoolInjector : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;

    public void InitializePool()
    {
        if (_bulletPool == null)
            _bulletPool = GetComponent<BulletPool>();
    }

    public BulletPool GetBulletPool() => _bulletPool;
}