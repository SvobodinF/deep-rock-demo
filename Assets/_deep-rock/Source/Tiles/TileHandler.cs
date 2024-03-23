using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHandler : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileConnector _tileConnector;
    [SerializeField] private int _maxHealth;

    private Dictionary<Vector3Int, int> _cellsHealth;

    public void Start()
    {
        _cellsHealth = new Dictionary<Vector3Int, int>();

        BoundsInt boundsInt = _tilemap.cellBounds;
        TileBase[] tileBases = _tileConnector.GetTiles(boundsInt);

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = tileBases[x + y * boundsInt.size.x];
                if (tile != null)
                {
                    _cellsHealth.Add(new Vector3Int(x, y, boundsInt.z), _maxHealth);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPosition = _tilemap.WorldToCell(worldPoint);
            DamegeCell(clickPosition, 1);
        }
    }

    public void DamegeCell(Vector3Int position, int damage)
    {
        if (_cellsHealth.TryGetValue(position, out int currentHealth))
        {
            if (currentHealth <= 0)
            {
                _tilemap.SetTile(position, null);
                _tileConnector.CalculateNeighboursConnection(new Vector2Int(position.x, position.y));
            }
            else
            {
                _cellsHealth[position] -= damage;
            }

            Debug.Log($"{position}, {currentHealth}");
        }
    }

    public void OnDamage(float damage)
    {

    }
}
