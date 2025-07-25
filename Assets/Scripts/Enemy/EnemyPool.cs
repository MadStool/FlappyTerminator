using UnityEngine;

public class EnemyPool : Pool<Enemy>
{
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private Vector2 _spawnAreaSize = new Vector2(5f, 3f);
    [SerializeField] private BulletPool _bulletPool;

    private float _timer;

    protected override void Awake()
    {
        base.Awake();
        InitializeEnemies();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            TrySpawnEnemy();
            _timer = 0f;
        }
    }

    private void InitializeEnemies()
    {
        foreach (Enemy enemy in _pool)
        {
            EnemyShooting shooting = enemy.GetComponent<EnemyShooting>();
            if (shooting != null)
                 shooting.Initialize(_bulletPool);
        }
    }

    private void TrySpawnEnemy()
    {
        Enemy enemy = GetItem();

        if (enemy != null)
        {
            enemy.transform.position = GetRandomSpawnPosition();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-_spawnAreaSize.x / 2f, _spawnAreaSize.x / 2f);
        float y = Random.Range(-_spawnAreaSize.y / 2f, _spawnAreaSize.y / 2f);

        return transform.position + new Vector3(x, y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(_spawnAreaSize.x, _spawnAreaSize.y, 0));
    }
}