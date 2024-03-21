using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TileConnector : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;

    [SerializeField] private Tile[] _tiles;
    [SerializeField] private Tile _blrokenTile;

    private Vector2Int[] _validationDirections => new Vector2Int[]
    {
        new Vector2Int(1,0),
        new Vector2Int(-1,0),
        new Vector2Int(0,1),
        new Vector2Int(0,-1),
        new Vector2Int(1,1),
        new Vector2Int(-1,-1),
        new Vector2Int(-1,1),
        new Vector2Int(1,-1),
    };

    [SerializeField] private Transform[] _indexes;

    private List<int[]> _connectionPatterns => new List<int[]>()
    {
        {new int[]{ 1, 0, 1, 0, 1, 0, 0, 0 } },
        {new int[]{ 1, 0, 1, 1, 1, 0, 0, 1 } },
        {new int[]{ 1, 0, 0, 1, 0, 0, 0, 1 } },
        {new int[]{ 1, 1, 1, 0, 1, 0, 1, 0 } },
        {new int[]{ 1, 1, 1, 1, 1, 1, 1, 1 } },
        {new int[]{ 1, 1, 0, 1, 0, 1, 0, 1 } },
        {new int[]{ 0, 1, 1, 0, 0, 0, 1, 0 } },
        {new int[]{ 0, 1, 1, 1, 0, 1, 1, 0 } },
        {new int[]{ 0, 1, 0, 1, 0, 1, 0, 0 } },
        {new int[]{ 1, 0, 1, 0, 1, 0, 0, 1 } },
        {new int[]{ 1, 0, 0, 1, 1, 0, 0, 1 } },
        {new int[]{ 1, 0, 1, 0, 1, 0, 1, 0 } },
        {new int[]{ 1, 1, 1, 1, 1, 0, 1, 1 } },
        {new int[]{ 1, 1, 1, 1, 1, 1, 0, 1 } },
        {new int[]{ 1, 0, 0, 1, 0, 1, 0, 1 } },
        {new int[]{ 0, 1, 1, 0, 1, 0, 1, 0 } },
        {new int[]{ 1, 1, 1, 1, 1, 1, 1, 0 } },
        {new int[]{ 1, 1, 1, 1, 0, 1, 1, 1 } },
        {new int[]{ 0, 1, 0, 1, 0, 1, 0, 1 } },
        {new int[]{ 0, 1, 1, 0, 0, 1, 1, 0 } },
        {new int[]{ 0, 1, 0, 1, 0, 1, 1, 0 } },
        {new int[]{ 1, 1, 1, 1, 1, 1, 0, 0 } },
        {new int[]{ 1, 0, 0, 1, 1, 1, 0, 1 } },
        {new int[]{ 0, 1, 1, 0, 1, 1, 1, 0 } },
        {new int[]{ 1, 1, 0, 1, 1, 1, 0, 1 } },
        {new int[]{ 1, 1, 1, 0, 1, 0, 1, 1 } },
        {new int[]{ 1, 1, 1, 0, 1, 1, 1, 0 } },
        {new int[]{ 1, 1, 0, 1, 0, 1, 1, 1 } },
        {new int[]{ 1, 0, 1, 1, 1, 0, 1, 1 } },
        {new int[]{ 1, 0, 1, 1, 1, 1, 0, 1 } },
        {new int[]{ 0, 1, 1, 1, 1, 1, 1, 0 } },
        {new int[]{ 0, 1, 1, 1, 0, 1, 1, 1 } },
        {new int[]{ 1, 0, 1, 0, 1, 0, 1, 1 } },
        {new int[]{ 0, 1, 0, 1, 0, 1, 1, 1 } },
    };

    [NaughtyAttributes.Button]
    public void Clear()
    {
        _tilemap.ClearAllTiles();
    }

    public void Update()
    {
        if (Application.isPlaying) return;

        BoundsInt boundsInt = _tilemap.cellBounds;
        TileBase[] tileBases = GetTiles(boundsInt);

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = tileBases[x + y * boundsInt.size.x];
                if (tile != null)
                {
                    int index = HasNeighbours(new Vector2Int(x, y));
                    if (index != -1)
                    {
                        _tilemap.SetTile(new Vector3Int(x, y, 0), _tiles[index]);
                    }
                    else
                    {
                        _tilemap.SetTile(new Vector3Int(x, y, 0), _blrokenTile);
                    }

                    _tilemap.ResizeBounds();
                }                
            }
        }
    }

    [NaughtyAttributes.Button]
    private void GetNewPatterns()
    {
        BoundsInt boundsInt = _tilemap.cellBounds;
        TileBase[] tileBases = GetTiles(boundsInt);

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = tileBases[x + y * boundsInt.size.x];

                if (tile == _blrokenTile)
                {
                    HasNeighbours(new Vector2Int(x, y), true);
                }
            }
        }
    }

    private TileBase[] GetTiles(BoundsInt boundsInt)
    {
        return _tilemap.GetTilesBlock(boundsInt);
    }

    private int HasNeighbours(Vector2Int origin, bool debugPattern = false)
    {
        int[] pattern = GetConnectionPattern(origin);

        if (debugPattern)
            Debug.Log($"Connection pattern { string.Join(", ", pattern)}");

        for (int i = 0; i < _connectionPatterns.Count; i++)
        {
            int counter = 0;

            for (int j = 0; j < pattern.Length; j++)
            {
                if (pattern[j] == _connectionPatterns[i][j])
                    counter++;
                else
                    break;
            }

            if (counter == pattern.Length)
                return i;
        }
        return -1;
    }

    private int[] GetConnectionPattern(Vector2Int origin)
    {
        List<int> result = new List<int>();

        foreach (Vector2Int direction in _validationDirections)
        {
            TileBase tile = _tilemap.GetTile(new Vector3Int(origin.x + direction.x, origin.y + direction.y));
            result.Add(tile == null ? 0 : 1);
        }

        return result.ToArray();
    }
}
