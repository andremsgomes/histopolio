using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Tile[] tiles;

    [SerializeField] private Tile groupPropertyTilePrefab;
    [SerializeField] private Tile cornerTilePrefab;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generate all tiles
    public void GenerateGrid() {
        tiles = new Tile[36];

        // 1st corner tile
        tiles[0] = Instantiate(cornerTilePrefab, new Vector3(8.3f, 0), Quaternion.Euler(0,0,90));
        tiles[0].name = $"Tile 0";

        // Bottom tiles
        for (int i = 0; i < 8; i++) {
            tiles[i+1] = Instantiate(groupPropertyTilePrefab, new Vector3(7-i, 0), Quaternion.Euler(0,0,0));
            tiles[i+1].name = $"Tile {i+1}";
        }

        // 2nd corner tile
        tiles[9] = Instantiate(cornerTilePrefab, new Vector3(-1.3f, 0), Quaternion.Euler(0,0,0));
        tiles[9].name = $"Tile 9";

        // Left tiles
        for (int i = 0; i < 8; i++) {
            tiles[i+10] = Instantiate(groupPropertyTilePrefab, new Vector3(-1.3f, i+1.3f), Quaternion.Euler(0,0,-90));
            tiles[i+10].name = $"Tile {i+10}";
        }

        // 3rd corner tile
        tiles[18] = Instantiate(cornerTilePrefab, new Vector3(-1.3f, 9.6f), Quaternion.Euler(0,0,-90));
        tiles[18].name = $"Tile 18";

        // Top tiles
        for (int i = 0; i < 8; i++) {
            tiles[i+19] = Instantiate(groupPropertyTilePrefab, new Vector3(i, 9.6f), Quaternion.Euler(0,0,180));
            tiles[i+19].name = $"Tile {i+19}";
        }

        // 4th corner tile
        tiles[27]= Instantiate(cornerTilePrefab, new Vector3(8.3f, 9.6f), Quaternion.Euler(0,0,180));
        tiles[27].name = $"Tile 27";

        // Right tiles
        for (int i = 0; i < 8; i++) {
            tiles[i+28] = Instantiate(groupPropertyTilePrefab, new Vector3(8.3f, 8.3f-i), Quaternion.Euler(0,0,90));
            tiles[i+28].name = $"Tile {i+28}";
        }
    }
}
