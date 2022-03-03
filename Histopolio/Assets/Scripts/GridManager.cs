using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GridManager : MonoBehaviour
{
    private Tile[] tiles;

    [SerializeField] private GroupPropertyTile groupPropertyTilePrefab;
    [SerializeField] private CornerTile cornerTilePrefab;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get tile from tile number
    public Tile GetTile(int tileId) {
        return tiles[tileId];
    }

    // Get all tiles
    public Tile[] GetTiles() {
        return tiles;
    }

    // Load existing board
    public void LoadBoard() {
        tiles = new Tile[40];

        string jsonString = File.ReadAllText(Application.dataPath + "/BoardData.json");
        BoardData boardData = JsonUtility.FromJson<BoardData>(jsonString);

        foreach(GroupPropertyTileData groupPropertyTileData in boardData.groupPropertyTiles) {
            tiles[groupPropertyTileData.id] = Instantiate(groupPropertyTilePrefab, groupPropertyTileData.position, groupPropertyTileData.rotation);
            tiles[groupPropertyTileData.id].name = $"Tile {groupPropertyTileData.id}";
            tiles[groupPropertyTileData.id].SetId(groupPropertyTileData.id);
            tiles[groupPropertyTileData.id].SetTileType(groupPropertyTileData.tileType);
            tiles[groupPropertyTileData.id].SetTileName(groupPropertyTileData.tileName);
            
            ((GroupPropertyTile)tiles[groupPropertyTileData.id]).SetPoints(groupPropertyTileData.points);
            ((GroupPropertyTile)tiles[groupPropertyTileData.id]).SetGroupColor(groupPropertyTileData.groupColor);
        }

        foreach(CornerTileData cornerTileData in boardData.cornerTiles) {
            tiles[cornerTileData.id] = Instantiate(cornerTilePrefab, cornerTileData.position, cornerTileData.rotation);
            tiles[cornerTileData.id].name = $"Tile {cornerTileData.id}";
            tiles[cornerTileData.id].SetId(cornerTileData.id);
            tiles[cornerTileData.id].SetTileType(cornerTileData.tileType);
            tiles[cornerTileData.id].SetTileName(cornerTileData.tileName);
        }
    }
}
