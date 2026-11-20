using UnityEngine;

public interface IFactoryProduct
{
    public void OnCreated();
    public void SetReturnToPool(System.Action<IFactoryProduct> returnAction);
}
