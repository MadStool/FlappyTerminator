using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : Pool<Enemy>
{
    [SerializeField] private EnemySpawner _spawner;

    protected override void Awake()
    {
        base.Awake();
        _spawner.Initialize(this);
    }

    public IEnumerable<Enemy> GetAllItems()
    {
        return _pool;
    }
}