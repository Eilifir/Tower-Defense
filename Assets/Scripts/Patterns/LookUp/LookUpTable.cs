using System.Collections.Generic;
using UnityEngine;

public class LookUpTable<T1, T2>
{
    public delegate T2 Ecuacion(T1 keyToReturn);
    Dictionary<T1, T2> _table;
    Ecuacion _factoryMethod;
    public LookUpTable(Ecuacion newFactory)
    {
        _factoryMethod = newFactory;
        _table = new Dictionary<T1, T2>();
    }

    public T2 ReturnValue(T1 myKey)
    {
        if (_table.ContainsKey(myKey))
        {
            return _table[myKey];
        }
        else
        {
            var value = _factoryMethod(myKey);
            _table[myKey] = value;
            return value;
        }
    }
}
