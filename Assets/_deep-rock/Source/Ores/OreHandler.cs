using System.Collections.Generic;
using System;

public class OreHandler
{
    public event Action<Dictionary<Type, OreData>> OnOreChangedEvent;

    private readonly Collector<Ore> _collector;

    private Dictionary<Type, OreData> _oreStorage;

    public OreHandler(Collector<Ore> collector)
    {
        _collector = collector;
        _oreStorage = new Dictionary<Type, OreData>();

        _collector.OnCollectedEvent += Add;
    }

    private void Add(Ore ore)
    {
        Type type = ore.GetType();

        if (_oreStorage.TryGetValue(type, out OreData data))
        {
            _oreStorage[type].Count++;
        }
        else
        {
            _oreStorage.Add(type, new OreData(ore, 1));
        }

        OnOreChangedEvent?.Invoke(_oreStorage);
    }
}

public class OreData
{
    public readonly Ore Ore;
    public int Count;

    public OreData(Ore ore, int count)
    {
        Ore = ore;
        Count = count;
    }
}