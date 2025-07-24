using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BulletPoolInjector))]
public class BulletPool : MonoBehaviour, IBulletProvider
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _poolSize = 20;

    private Queue<Bullet> _pool;
    private Transform _poolContainer;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        _poolContainer = new GameObject("BulletPoolContainer").transform;
        _poolContainer.SetParent(transform);
        _pool = new Queue<Bullet>();

        for (int i = 0; i < _poolSize; i++)
        {
            CreateBullet();
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _poolContainer);

        bullet.gameObject.SetActive(false);
        bullet.Initialize(this);
        _pool.Enqueue(bullet);

        return bullet;
    }

    public Bullet GetBullet()
    {
        foreach (Bullet bullet in _pool)
        {
            if (bullet.gameObject.activeInHierarchy == false)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        Debug.LogWarning("All bullets are active! Returning null.");

        return null;
    }

    public void ReturnBullet(Bullet bullet)
    {
        if (bullet == null)
             return;

        bullet.transform.SetParent(_poolContainer);
        bullet.transform.localPosition = Vector3.zero;
        bullet.transform.localScale = _bulletPrefab.transform.localScale;

        if (bullet.TryGetComponent<SpriteRenderer>(out var renderer))
        {
            renderer.flipX = false;
        }

        bullet.gameObject.SetActive(false);
    }
}