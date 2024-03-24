using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHandler : MonoBehaviour, IDiggable
{
    public event Action<Vector3, Vector3Int> OnTileAddedEvent;
    public event Action<Vector3Int> OnTileRemovedEvent;

    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileConnector _tileConnector;
    [SerializeField] private int _maxHealth;

    private Dictionary<Vector3Int, int> _cellsHealth;

    private Vector3Int _cellDebug;

    public void Init()
    {
        _cellsHealth = new Dictionary<Vector3Int, int>();

        BoundsInt boundsInt = _tilemap.cellBounds;
        TileBase[] tileBases = GetAllTiles();

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = tileBases[x + y * boundsInt.size.x];
                if (tile != null)
                {
                    Vector3Int pos = new Vector3Int(x, y, boundsInt.z);
                    _cellsHealth.Add(pos, _maxHealth);
                    OnTileAddedEvent?.Invoke(_tilemap.CellToWorld(pos) + _tilemap.cellSize / 2, pos);
                }
            }
        }
    }

    private TileBase[] GetAllTiles()
    {
        BoundsInt boundsInt = _tilemap.cellBounds;
        return _tileConnector.GetTiles(boundsInt);
    }

    public void Dig(Vector3 position, int damage)
    {
        Vector3Int cellIndexes = _tilemap.WorldToCell(position);
        _cellDebug = cellIndexes;

        if (_cellsHealth.TryGetValue(cellIndexes, out int currentHealth))
        {
            if (currentHealth <= 0)
            {
                _tilemap.SetTile(cellIndexes, null);
                _tileConnector.CalculateNeighboursConnection(new Vector2Int(cellIndexes.x, cellIndexes.y));
                OnTileRemovedEvent?.Invoke(cellIndexes);

                return;
            }

            _cellsHealth[cellIndexes] -= damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_cellDebug, 0.2f);
    }
}
