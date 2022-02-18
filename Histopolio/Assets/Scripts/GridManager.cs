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
        tiles = new Tile[40];

        // 1st corner tile
        tiles[0] = Instantiate(cornerTilePrefab, new Vector3(9.3f, 0), Quaternion.Euler(0,0,90));
        tiles[0].name = "Tile 0";
        tiles[0].SetId(0);

        // Bottom tiles
        for (int i = 0; i < 9; i++) {
            tiles[i+1] = Instantiate(groupPropertyTilePrefab, new Vector3(8-i, 0), Quaternion.Euler(0,0,0));
            tiles[i+1].name = $"Tile {i+1}";
            tiles[i+1].SetId(i+1);
        }

        // 2nd corner tile
        tiles[10] = Instantiate(cornerTilePrefab, new Vector3(-1.3f, 0), Quaternion.Euler(0,0,0));
        tiles[10].name = "Tile 10";
        tiles[10].SetId(10);

        // Left tiles
        for (int i = 0; i < 9; i++) {
            tiles[i+11] = Instantiate(groupPropertyTilePrefab, new Vector3(-1.3f, i+1.3f), Quaternion.Euler(0,0,-90));
            tiles[i+11].name = $"Tile {i+11}";
            tiles[i+11].SetId(i+11);
        }

        // 3rd corner tile
        tiles[20] = Instantiate(cornerTilePrefab, new Vector3(-1.3f, 10.6f), Quaternion.Euler(0,0,-90));
        tiles[20].name = "Tile 20";
        tiles[20].SetId(20);

        // Top tiles
        for (int i = 0; i < 9; i++) {
            tiles[i+21] = Instantiate(groupPropertyTilePrefab, new Vector3(i, 10.6f), Quaternion.Euler(0,0,180));
            tiles[i+21].name = $"Tile {i+21}";
            tiles[i+21].SetId(i+21);
        }

        // 4th corner tile
        tiles[30]= Instantiate(cornerTilePrefab, new Vector3(9.3f, 10.6f), Quaternion.Euler(0,0,180));
        tiles[30].name = "Tile 30";
        tiles[30].SetId(30);

        // Right tiles
        for (int i = 0; i < 9; i++) {
            tiles[i+31] = Instantiate(groupPropertyTilePrefab, new Vector3(9.3f, 9.3f-i), Quaternion.Euler(0,0,90));
            tiles[i+31].name = $"Tile {i+31}";
            tiles[i+31].SetId(i+31);
        }
    }

    // Get tile from tile number
    public Tile GetTile(int tileId) {
        return tiles[tileId];
    }
}
