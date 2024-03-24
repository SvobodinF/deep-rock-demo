using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OreInstaller : Installer<OreInitialData>
{
    private TileHandler _tileHandler;
    private List<OreLevelConfiguration> _oreLevelConfigurations;

    private Dictionary<Vector3Int, Vector3> _cells;
    private Dictionary<Vector3Int, Ore> _cellsOres;

    private int _oreAmount;

    public override void Install(OreInitialData configuration)
    {
        _cellsOres = new Dictionary<Vector3Int, Ore>();
        _cells = new Dictionary<Vector3Int, Vector3>();

        _tileHandler = configuration.TileHandler;
        List<OreLevelConfiguration> main = configuration.OreLevelConfiguration.ToList();
        _oreLevelConfigurations = new List<OreLevelConfiguration>();

        foreach (OreLevelConfiguration oreLevelConfiguration in main)
        {
            _oreLevelConfigurations.Add(new OreLevelConfiguration(oreLevelConfiguration));

            _oreAmount += oreLevelConfiguration.Count;
        }

        _tileHandler.OnTileAddedEvent += AddCell;
        _tileHandler.OnTileRemovedEvent += RemoveOre;
    }

    public void Generate()
    {
        for (int i = 0; i < _oreAmount; i++)
        {
            CreateOre();
        }
    }

    private void AddCell(Vector3 worldPosition, Vector3Int coordinates)
    {
        _cells.Add(coordinates, worldPosition);
    }

    private void CreateOre()
    {
        KeyValuePair<Vector3Int, Vector3> cell = GetCell();

        int randomOre = Random.Range(0, _oreLevelConfigurations.Count);

        if (_oreLevelConfigurations.Count == 0)
            return;

        OreLevelConfiguration configuration = _oreLevelConfigurations[randomOre];
        configuration.Count--;

        if (configuration.Count <= 0)
        {
            _oreLevelConfigurations.RemoveAt(randomOre);
        }

        Ore prefab = configuration.OreConfiguration.OrePrefab;
        Ore ore = Instantiate(prefab, transform);
        ore.transform.position = cell.Value;

        ore.SetLinkedTile(cell.Key);
        _cellsOres.Add(cell.Key, ore);
    }

    private KeyValuePair<Vector3Int, Vector3> GetCell()
    {
        int randomCell = Random.Range(0, _cells.Count);
        KeyValuePair<Vector3Int, Vector3> result = _cells.ElementAt(randomCell);
        _cells.Remove(result.Key);

        return result;
    }

    private void RemoveOre(Vector3Int coordinates)
    {
        if (_cellsOres.TryGetValue(coordinates, out Ore ore))
        {
            ore.SetLinkedTile(null);
        }
    }
}

public struct OreInitialData : IConfiguration
{
    public readonly TileHandler TileHandler;
    public readonly OreLevelConfiguration[] OreLevelConfiguration;

    public OreInitialData(TileHandler tileHandler, OreLevelConfiguration[] oreLevelConfiguration)
    {
        TileHandler = tileHandler;
        OreLevelConfiguration = oreLevelConfiguration;
    }
}