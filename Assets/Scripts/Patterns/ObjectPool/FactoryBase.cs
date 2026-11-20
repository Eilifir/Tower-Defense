using UnityEngine;

public abstract class FactoryBase : MonoBehaviour
{
    public abstract IFactoryProduct Create(Vector3 position, Quaternion rotation);
}
