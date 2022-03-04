using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GridManager : MonoBehaviour
{
    private Tile[] tiles;

    [SerializeField] private GroupPropertyTile groupPropertyTilePrefab;
    [SerializeField] private GoTile goTilePrefab;
    [SerializeField] private PrisonTile prisonTilePrefab;
    [SerializeField] private ParkingTile parkingTilePrefab;
    [SerializeField] private GoToPrisonTile goToPrisonTilePrefab;
    

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
            tiles[groupPropertyTileData.id].SetTileName(groupPropertyTileData.tileName);
            
            ((GroupPropertyTile)tiles[groupPropertyTileData.id]).SetPoints(groupPropertyTileData.points);
            ((GroupPropertyTile)tiles[groupPropertyTileData.id]).SetGroupColor(groupPropertyTileData.groupColor);
        }

        tiles[boardData.goTileData.id] = Instantiate(goTilePrefab, boardData.goTileData.position, boardData.goTileData.rotation);
        tiles[boardData.goTileData.id].name = $"Tile {boardData.goTileData.id}";
        tiles[boardData.goTileData.id].SetId(boardData.goTileData.id);
        tiles[boardData.goTileData.id].SetTileName(boardData.goTileData.tileName);

        tiles[boardData.prisonTileData.id] = Instantiate(prisonTilePrefab, boardData.prisonTileData.position, boardData.prisonTileData.rotation);
        tiles[boardData.prisonTileData.id].name = $"Tile {boardData.prisonTileData.id}";
        tiles[boardData.prisonTileData.id].SetId(boardData.prisonTileData.id);
        tiles[boardData.prisonTileData.id].SetTileName(boardData.prisonTileData.tileName);

        tiles[boardData.parkingTileData.id] = Instantiate(parkingTilePrefab, boardData.parkingTileData.position, boardData.parkingTileData.rotation);
        tiles[boardData.parkingTileData.id].name = $"Tile {boardData.parkingTileData.id}";
        tiles[boardData.parkingTileData.id].SetId(boardData.parkingTileData.id);
        tiles[boardData.parkingTileData.id].SetTileName(boardData.parkingTileData.tileName);

        tiles[boardData.goToPrisonTileData.id] = Instantiate(goToPrisonTilePrefab, boardData.goToPrisonTileData.position, boardData.goToPrisonTileData.rotation);
        tiles[boardData.goToPrisonTileData.id].name = $"Tile {boardData.goToPrisonTileData.id}";
        tiles[boardData.goToPrisonTileData.id].SetId(boardData.goToPrisonTileData.id);
        tiles[boardData.goToPrisonTileData.id].SetTileName(boardData.goToPrisonTileData.tileName);
    }
}
