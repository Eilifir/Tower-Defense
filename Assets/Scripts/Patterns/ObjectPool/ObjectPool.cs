using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component, IFactoryProduct
{
    [SerializeField] protected T prefab;
    [SerializeField] protected int initialSize = 10;
    [SerializeField] protected bool expand = true;
    private Stack<T> pool;

    protected void Awake()
    {
        pool = new Stack<T>(initialSize);
        for (int i = 0; i < initialSize; i++)
        {
            var inst = Instantiate(prefab, transform);
            inst.gameObject.SetActive(false);
            pool.Push(inst);
        }
    }

    public virtual T Get(Vector3 position, Quaternion rotation)
    {
        T item = pool.Count > 0 ? pool.Pop() : (expand ? Instantiate(prefab, transform) : null);
        if (item == null) return null;
        item.transform.SetPositionAndRotation(position, rotation);
        item.gameObject.SetActive(true);
        return item;
    }

    public virtual void Retrun(T item)
    {
        if(item == null) return;
        item.gameObject.SetActive(false);
        item.transform.SetParent(transform, false);
        pool.Push(item);
    }
}
