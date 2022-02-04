using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Generate all tiles
    void GenerateGrid() {
        for (int i = 0; i < 8; i++) {
            var spawnedTile = Instantiate(tilePrefab, new Vector3(i, 0), Quaternion.Euler(0,0,0));
            spawnedTile.name = $"Tile {i} 0";
        }

        for (int i = 0; i < 8; i++) {
            var spawnedTile = Instantiate(tilePrefab, new Vector3(i, 9.6f), Quaternion.Euler(0,0,180));
            spawnedTile.name = $"Tile {i} 8";
        }

        for (int i = 0; i < 8; i++) {
            var spawnedTile = Instantiate(tilePrefab, new Vector3(-1.3f, i+1.3f), Quaternion.Euler(0,0,-90));
            spawnedTile.name = $"Tile 0 {i}";
        }

        for (int i = 0; i < 8; i++) {
            var spawnedTile = Instantiate(tilePrefab, new Vector3(8.3f, i+1.3f), Quaternion.Euler(0,0,90));
            spawnedTile.name = $"Tile 8 {i}";
        }

        camera.transform.position = new Vector3(3.5f, 4.8f, -10);
        camera.orthographicSize = 6;
    }
}
