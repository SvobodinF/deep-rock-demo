using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _parent;
    private List<T> _objects;

    public ObjectPool(T prefab, Transform parent, int prewarmObjects)
    {
        _prefab = prefab;
        _parent = parent;
        _objects = new List<T>();

        for (int i = 0; i < prewarmObjects; i++)
        {
            Create();
        }
    }

    public T Get()
    {
        T obj = _objects.Find(x => x.isActiveAndEnabled == false);

        if (obj == null)
        {
            obj = Create();
        }

        obj.gameObject.SetActive(true);

        return obj;
    }

    public void Release(T obj) => obj.gameObject.SetActive(false);

    private T Create()
    {
        T obj = GameObject.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(false);
        _objects.Add(obj);

        return obj;
    }
}
