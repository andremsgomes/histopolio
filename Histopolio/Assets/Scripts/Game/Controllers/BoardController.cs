using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardController : MonoBehaviour
{
    private Tile[] tiles;
    private GameController gameController;

    [SerializeField] private GroupPropertyTile groupPropertyTilePrefab;
    [SerializeField] private GoTile goTilePrefab;
    [SerializeField] private PrisonTile prisonTilePrefab;
    [SerializeField] private ParkingTile parkingTilePrefab;
    [SerializeField] private GoToPrisonTile goToPrisonTilePrefab;
    [SerializeField] private CommunityTile communityTilePrefab;
    [SerializeField] private PayTile payTilePrefab;
    [SerializeField] private StationTile stationTilePrefab;
    [SerializeField] private ChanceTile chanceTilePrefab;
    [SerializeField] private BoardBase boardBasePrefab;
    

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

    // Save tiles' info into a json file
    public void SaveBoardIntoJson() {
        BoardData boardData = new BoardData();

        for (int i = 0; i < tiles.Length; i++) {
            if (tiles[i].GetType().Name.Equals("GroupPropertyTile")) {
                GroupPropertyTile groupPropertyTile = (GroupPropertyTile)tiles[i];
                boardData.groupPropertyTiles.Add(new GroupPropertyTileData(groupPropertyTile.GetId(), groupPropertyTile.GetTileName(), groupPropertyTile.transform.position, groupPropertyTile.transform.rotation, groupPropertyTile.GetPoints(), groupPropertyTile.GetGroupColor()));
            }
            else if (tiles[i].GetType().Name.Equals("GoTile"))
                boardData.goTileData = new GoTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
            else if (tiles[i].GetType().Name.Equals("PrisonTile"))
                boardData.prisonTileData = new PrisonTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
            else if (tiles[i].GetType().Name.Equals("ParkingTile"))
                boardData.parkingTileData = new ParkingTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
            else if (tiles[i].GetType().Name.Equals("GoToPrisonTile"))
                boardData.goToPrisonTileData = new GoToPrisonTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
            else if (tiles[i].GetType().Name.Equals("CommunityTile"))
                boardData.communityTiles.Add(new CommunityTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation));
            else if (tiles[i].GetType().Name.Equals("PayTile"))
                boardData.payTiles.Add(new PayTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation, ((PayTile)tiles[i]).GetPoints()));
            else if (tiles[i].GetType().Name.Equals("StationTile"))
                boardData.stationTiles.Add(new StationTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation, ((StationTile)tiles[i]).GetPoints()));
            else if (tiles[i].GetType().Name.Equals("ChanceTile"))
                boardData.chanceTiles.Add(new ChanceTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation));
        }

        string board = JsonUtility.ToJson(boardData);
        System.IO.File.WriteAllText(Application.dataPath + "/BoardData.json", board);
    }

    // Load existing board
    public void LoadBoard(BoardData boardData) {
        tiles = new Tile[40];

        foreach(GroupPropertyTileData groupPropertyTileData in boardData.groupPropertyTiles) {
            tiles[groupPropertyTileData.id] = Instantiate(groupPropertyTilePrefab, groupPropertyTileData.position, groupPropertyTileData.rotation);
            tiles[groupPropertyTileData.id].name = $"Tile {groupPropertyTileData.id}";
            tiles[groupPropertyTileData.id].SetId(groupPropertyTileData.id);
            tiles[groupPropertyTileData.id].SetTileName(groupPropertyTileData.tileName);
            tiles[groupPropertyTileData.id].SetGameController(gameController);
            
            ((GroupPropertyTile)tiles[groupPropertyTileData.id]).SetPoints(groupPropertyTileData.points);
            ((GroupPropertyTile)tiles[groupPropertyTileData.id]).SetGroupColor(groupPropertyTileData.groupColor);
        }

        foreach(CommunityTileData communityTileData in boardData.communityTiles) {
            tiles[communityTileData.id] = Instantiate(communityTilePrefab, communityTileData.position, communityTileData.rotation);
            tiles[communityTileData.id].name = $"Tile {communityTileData.id}";
            tiles[communityTileData.id].SetId(communityTileData.id);
            tiles[communityTileData.id].SetTileName(communityTileData.tileName);
            tiles[communityTileData.id].SetGameController(gameController);
        }

        foreach(PayTileData payTileData in boardData.payTiles) {
            tiles[payTileData.id] = Instantiate(payTilePrefab, payTileData.position, payTileData.rotation);
            tiles[payTileData.id].name = $"Tile {payTileData.id}";
            tiles[payTileData.id].SetId(payTileData.id);
            tiles[payTileData.id].SetTileName(payTileData.tileName);
            tiles[payTileData.id].SetGameController(gameController);

            ((PayTile)tiles[payTileData.id]).SetPoints(payTileData.points);
        }

        foreach(StationTileData stationTileData in boardData.stationTiles) {
            tiles[stationTileData.id] = Instantiate(stationTilePrefab, stationTileData.position, stationTileData.rotation);
            tiles[stationTileData.id].name = $"Tile {stationTileData.id}";
            tiles[stationTileData.id].SetId(stationTileData.id);
            tiles[stationTileData.id].SetTileName(stationTileData.tileName);
            tiles[stationTileData.id].SetGameController(gameController);

            ((StationTile)tiles[stationTileData.id]).SetPoints(stationTileData.points);
        }

        foreach(ChanceTileData chanceTileData in boardData.chanceTiles) {
            tiles[chanceTileData.id] = Instantiate(chanceTilePrefab, chanceTileData.position, chanceTileData.rotation);
            tiles[chanceTileData.id].name = $"Tile {chanceTileData.id}";
            tiles[chanceTileData.id].SetId(chanceTileData.id);
            tiles[chanceTileData.id].SetTileName(chanceTileData.tileName);
            tiles[chanceTileData.id].SetGameController(gameController);
        }

        tiles[boardData.goTileData.id] = Instantiate(goTilePrefab, boardData.goTileData.position, boardData.goTileData.rotation);
        tiles[boardData.goTileData.id].name = $"Tile {boardData.goTileData.id}";
        tiles[boardData.goTileData.id].SetId(boardData.goTileData.id);
        tiles[boardData.goTileData.id].SetTileName(boardData.goTileData.tileName);
        tiles[boardData.goTileData.id].SetGameController(gameController);


        tiles[boardData.prisonTileData.id] = Instantiate(prisonTilePrefab, boardData.prisonTileData.position, boardData.prisonTileData.rotation);
        tiles[boardData.prisonTileData.id].name = $"Tile {boardData.prisonTileData.id}";
        tiles[boardData.prisonTileData.id].SetId(boardData.prisonTileData.id);
        tiles[boardData.prisonTileData.id].SetTileName(boardData.prisonTileData.tileName);
        tiles[boardData.prisonTileData.id].SetGameController(gameController);

        tiles[boardData.parkingTileData.id] = Instantiate(parkingTilePrefab, boardData.parkingTileData.position, boardData.parkingTileData.rotation);
        tiles[boardData.parkingTileData.id].name = $"Tile {boardData.parkingTileData.id}";
        tiles[boardData.parkingTileData.id].SetId(boardData.parkingTileData.id);
        tiles[boardData.parkingTileData.id].SetTileName(boardData.parkingTileData.tileName);
        tiles[boardData.parkingTileData.id].SetGameController(gameController);

        tiles[boardData.goToPrisonTileData.id] = Instantiate(goToPrisonTilePrefab, boardData.goToPrisonTileData.position, boardData.goToPrisonTileData.rotation);
        tiles[boardData.goToPrisonTileData.id].name = $"Tile {boardData.goToPrisonTileData.id}";
        tiles[boardData.goToPrisonTileData.id].SetId(boardData.goToPrisonTileData.id);
        tiles[boardData.goToPrisonTileData.id].SetTileName(boardData.goToPrisonTileData.tileName);
        tiles[boardData.goToPrisonTileData.id].SetGameController(gameController);

        BoardBase boardBase = Instantiate(boardBasePrefab, new Vector3(4, 5.3f), Quaternion.identity);
        boardBase.name = "Board base";
    }

    // Set game manager
    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }
}
