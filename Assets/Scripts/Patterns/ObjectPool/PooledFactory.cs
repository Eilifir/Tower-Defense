using UnityEngine;

public abstract class PooledFactory<T> : FactoryBase where T : Component, IFactoryProduct
{
    [SerializeField] protected ObjectPool<T> pool;

    public T CreateTyped(Vector3 position, Quaternion rotation)
    {
        var instance = pool.Get(position, rotation);
        if (instance == null) return null;
        instance.SetReturnToPool(p => pool.Retrun((T)p));
        instance.OnCreated();
        return instance;
    }

    public override IFactoryProduct Create(Vector3 position, Quaternion rotation)
    {
        return CreateTyped(position, rotation);
    }
}
