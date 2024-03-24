using System;
using System.Collections.Generic;
using UnityEngine;

public class OreContainer : MonoBehaviour
{
    [SerializeField] private OreCounter _oreCounterPrefab;

    private OreHandler _oreHandler;

    private Dictionary<Type, OreCounter> _counters;

    public void Init(OreHandler oreHandler)
    {
        _counters = new Dictionary<Type, OreCounter>();
        _oreHandler = oreHandler;

        _oreHandler.OnOreChangedEvent += ChangeContent;
    }

    private void OnDestroy()
    {
        _oreHandler.OnOreChangedEvent -= ChangeContent;
    }

    private void ChangeContent(Dictionary<Type, OreData> storage)
    {
        foreach (KeyValuePair<Type, OreData> keyValuePair in storage)
        {
            if (_counters.ContainsKey(keyValuePair.Key))
            {
                UpdateCounter(keyValuePair.Key, keyValuePair.Value);
            }
            else
            {
                CreateAndUpdateCounter(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }

    private void UpdateCounter(Type type, OreData data)
    {
        if (_counters.TryGetValue(type, out OreCounter counter))
        {
            counter.SetCount(data.Count);
        }
    }

    private void CreateAndUpdateCounter(Type type, OreData data)
    {
        OreCounter counter = Instantiate(_oreCounterPrefab, transform);
        counter.Init(data.Ore.Sprite);
        _counters.Add(type, counter);

        UpdateCounter(type, data);
    }
}
