using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private Vector2 _spawnAreaSize = new Vector2(5f, 3f);
    [SerializeField] private BulletPool _bulletPool;

    private List<Enemy> _enemyPool;
    private float _timer;

    private void Awake()
    {
        _enemyPool = new List<Enemy>();

        for (int i = 0; i < _poolSize; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            enemy.gameObject.SetActive(false);

            EnemyShooting shooting = enemy.GetComponent<EnemyShooting>();

            if (shooting != null)
            {
                shooting.Initialize(_bulletPool);
            }
            else
            {
                Debug.LogError("Enemy prefab missing EnemyShooting component!", enemy);
            }

            _enemyPool.Add(enemy);
        }
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

    private void TrySpawnEnemy()
    {
        foreach (Enemy enemy in _enemyPool)
        {
            if (enemy.gameObject.activeInHierarchy == false)
            {
                enemy.transform.position = GetRandomSpawnPosition();
                enemy.gameObject.SetActive(true);

                return;
            }
        }

        Debug.Log("Пул переполнен! Все враги активны.");
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