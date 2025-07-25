using UnityEngine;
using System.Collections.Generic;

public abstract class Pool<TItem> : MonoBehaviour where TItem : MonoBehaviour
{
    [SerializeField] protected TItem _prefab;
    [SerializeField] protected int _poolSize = 10;

    protected Queue<TItem> _pool;
    protected Transform _poolContainer;

    protected virtual void Awake()
    {
        InitializePool();
    }

    protected virtual void InitializePool()
    {
        _poolContainer = new GameObject($"{typeof(TItem).Name}PoolContainer").transform;
        _poolContainer.SetParent(transform);
        _pool = new Queue<TItem>();

        for (int i = 0; i < _poolSize; i++)
        {
            CreateItem();
        }
    }

    protected virtual TItem CreateItem()
    {
        TItem item = Instantiate(_prefab, _poolContainer);
        item.gameObject.SetActive(false);
        _pool.Enqueue(item);

        return item;
    }

    public virtual TItem GetItem()
    {
        foreach (TItem item in _pool)
        {
            if (item.gameObject.activeInHierarchy == false)
            {
                item.gameObject.SetActive(true);
                return item;
            }
        }

        Debug.LogWarning($"All {typeof(TItem).Name} items are active! Returning null.");

        return null;
    }

    public virtual void ReturnItem(TItem item)
    {
        if (item == null) 
             return;

        item.transform.SetParent(_poolContainer);
        item.gameObject.SetActive(false);
    }
}