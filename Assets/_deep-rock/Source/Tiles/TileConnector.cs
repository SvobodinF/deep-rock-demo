using System.Collections.Generic;
using UnityEditor;
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
        {new int[]{ 1, 0, 1, 0, 0, 0, 0, 0 } },
        {new int[]{ 0, 0, 1, 1, 1, 0, 0, 1 } },
        {new int[]{ 1, 0, 0, 1, 0, 0, 0, 0 } },
        {new int[]{ 1, 1, 0, 0, 1, 0, 1, 0 } },
        {new int[]{ 1, 1, 0, 0, 0, 1, 0, 1 } },
        {new int[]{ 0, 1, 1, 0, 0, 0, 0, 0 } },
        {new int[]{ 0, 0, 1, 1, 0, 1, 1, 0 } },
        {new int[]{ 0, 1, 0, 1, 0, 0, 0, 0 } },
        {new int[]{ 0, 1, 1, 1, 1, 1, 1, 1 } },
        {new int[]{ 1, 1, 0, 1, 1, 1, 1, 1 } },
        {new int[]{ 1, 1, 1, 0, 1, 1, 1, 1 } },
        {new int[]{ 1, 0, 1, 1, 1, 1, 1, 1 } },
        {new int[]{ 0, 1, 1, 0, 1, 0, 1, 1 } },
        {new int[]{ 1, 0, 0, 1, 0, 0, 1, 1 } },
        {new int[]{ 1, 0, 0, 0, 0, 0, 0, 1 } },
        {new int[]{ 0, 0, 1, 0, 0, 0, 1, 0 } },
        {new int[]{ 0, 1, 0, 1, 1, 0, 0, 0 } },
        {new int[]{ 1, 0, 1, 0, 1, 1, 1, 0 } },
    };

    [NaughtyAttributes.Button]
    public void Clear()
    {
        _tilemap.ClearAllTiles();
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += EditTileset;
        Debug.Log("Follow");
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= EditTileset;
        Debug.Log("Unfollow");
    }

    [NaughtyAttributes.Button]
    public void UpdateAllTilesPattern()
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
                    CalculateTileConnection(new Vector2Int(x, y));
                }                
            }
        }
    }

    private void EditTileset(SceneView sceneView)
    {
        if (Application.isPlaying) return;

        Vector3Int? cellPosition = GetMousePosition();

        if (cellPosition == null)
            return;

        CalculateTileConnection(new Vector2Int(cellPosition.Value.x, cellPosition.Value.y));
        CalculateNeighboursConnection(new Vector2Int(cellPosition.Value.x, cellPosition.Value.y));
    }

    public Vector3Int? GetMousePosition()
    {
        Event e = Event.current;
        if (e != null)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                Vector3Int position = Vector3Int.FloorToInt(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin);
                Vector3Int cellPos = _tilemap.WorldToCell(position);

                return cellPos;
            }
        }

        return null;
    }

    public void CalculateNeighboursConnection(Vector2Int origin)
    {
        foreach (Vector2Int direction in _validationDirections)
        {
            Vector2Int coordinates = new Vector2Int(origin.x + direction.x, origin.y + direction.y);

            TileBase tile = _tilemap.GetTile(new Vector3Int(coordinates.x, coordinates.y, 0));

            if (tile == null)
                continue;

            CalculateTileConnection(coordinates);
        }
    }

    public void CalculateTileConnection(Vector2Int pos)
    {
        int index = HasNeighbours(new Vector2Int(pos.x, pos.y));
        Debug.Log(index);
        if (index != -1)
        {
            _tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), _tiles[index]);
        }
        else
        {
            _tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), _blrokenTile);
        }

        _tilemap.ResizeBounds();
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

    public TileBase[] GetTiles(BoundsInt boundsInt)
    {
        return _tilemap.GetTilesBlock(boundsInt);
    }

    private int HasNeighbours(Vector2Int origin, bool debugPattern = false)
    {
        int[] pattern = GetConnectionPattern(origin);

        int result = ValidatePattern(pattern);

        if (debugPattern)
        {
            if (result == -1)
                Debug.Log($"Connection pattern { string.Join(", ", pattern)}");
        }

        return result;
    }

    private int ValidatePattern(int[] pattern)
    {
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
