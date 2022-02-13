using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile groupPropertyTilePrefab;
    [SerializeField] private Tile cornerTilePrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generate all tiles
    void GenerateGrid() {
        // Bottom tiles
        for (int i = 0; i < 8; i++) {
            var spawnedTile = Instantiate(groupPropertyTilePrefab, new Vector3(i, 0), Quaternion.Euler(0,0,0));
            spawnedTile.name = $"Tile {i+1} 0";
        }

        // Top tiles
        for (int i = 0; i < 8; i++) {
            var spawnedTile = Instantiate(groupPropertyTilePrefab, new Vector3(i, 9.6f), Quaternion.Euler(0,0,180));
            spawnedTile.name = $"Tile {i+1} 9";
        }

        // Left tiles
        for (int i = 0; i < 8; i++) {
            var spawnedTile = Instantiate(groupPropertyTilePrefab, new Vector3(-1.3f, i+1.3f), Quaternion.Euler(0,0,-90));
            spawnedTile.name = $"Tile 0 {i+1}";
        }

        // Right tiles
        for (int i = 0; i < 8; i++) {
            var spawnedTile = Instantiate(groupPropertyTilePrefab, new Vector3(8.3f, i+1.3f), Quaternion.Euler(0,0,90));
            spawnedTile.name = $"Tile 9 {i+1}";
        }

        // Corners
        var spawnedCornerTile1 = Instantiate(cornerTilePrefab, new Vector3(-1.3f, 0), Quaternion.Euler(0,0,0));
        spawnedCornerTile1.name = $"Tile 0 0";

        var spawnedCornerTile2 = Instantiate(cornerTilePrefab, new Vector3(-1.3f, 9.6f), Quaternion.Euler(0,0,-90));
        spawnedCornerTile2.name = $"Tile 0 9";

        var spawnedCornerTile3 = Instantiate(cornerTilePrefab, new Vector3(8.3f, 9.6f), Quaternion.Euler(0,0,180));
        spawnedCornerTile3.name = $"Tile 9 9";

        var spawnedCornerTile4 = Instantiate(cornerTilePrefab, new Vector3(8.3f, 0), Quaternion.Euler(0,0,90));
        spawnedCornerTile4.name = $"Tile 9 0";
    }
}
