using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardController : MonoBehaviour
{
    private Tile[] tiles;
    private Dictionary<string, Tile> tilesDictionary = new Dictionary<string, Tile>();
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

    // Get tile from tile id
    public Tile GetTile(string tileId)
    {
        return tilesDictionary[tileId];
    }

    public Tile GetTileFromPosition(int position)
    {
        return tiles[position];
    }

    // Get all tiles
    public Tile[] GetTiles()
    {
        return tiles;
    }

    // // Save tiles' info into a json file
    // public void SaveBoardIntoJson() {
    //     BoardData boardData = new BoardData();

    //     for (int i = 0; i < tiles.Length; i++) {
    //         if (tiles[i].GetType().Name.Equals("GroupPropertyTile")) {
    //             GroupPropertyTile groupPropertyTile = (GroupPropertyTile)tiles[i];
    //             boardData.groupPropertyTiles.Add(new GroupPropertyTileData(groupPropertyTile.GetId(), groupPropertyTile.GetTileName(), groupPropertyTile.transform.position, groupPropertyTile.transform.rotation, groupPropertyTile.GetPoints(), groupPropertyTile.GetGroupColor()));
    //         }
    //         else if (tiles[i].GetType().Name.Equals("GoTile"))
    //             boardData.goTileData = new GoTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
    //         else if (tiles[i].GetType().Name.Equals("PrisonTile"))
    //             boardData.prisonTileData = new PrisonTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
    //         else if (tiles[i].GetType().Name.Equals("ParkingTile"))
    //             boardData.parkingTileData = new ParkingTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
    //         else if (tiles[i].GetType().Name.Equals("GoToPrisonTile"))
    //             boardData.goToPrisonTileData = new GoToPrisonTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
    //         else if (tiles[i].GetType().Name.Equals("CommunityTile"))
    //             boardData.communityTiles.Add(new CommunityTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation));
    //         else if (tiles[i].GetType().Name.Equals("PayTile"))
    //             boardData.payTiles.Add(new PayTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation, ((PayTile)tiles[i]).GetPoints()));
    //         else if (tiles[i].GetType().Name.Equals("StationTile"))
    //             boardData.stationTiles.Add(new StationTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation, ((StationTile)tiles[i]).GetPoints()));
    //         else if (tiles[i].GetType().Name.Equals("ChanceTile"))
    //             boardData.chanceTiles.Add(new ChanceTileData(tiles[i].GetId(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation));
    //     }

    //     string board = JsonUtility.ToJson(boardData);
    //     System.IO.File.WriteAllText(Application.dataPath + "/BoardData.json", board);
    // }

    // Load existing board
    public void LoadBoard(List<TileData> tilesData)
    {
        tiles = new Tile[40];

        foreach (TileData tile in tilesData)
        {
            switch (tile.type)
            {
                case "go":
                    tiles[tile.boardPosition] = Instantiate(goTilePrefab, tile.position, tile.rotation);
                    break;
                case "groupProperty":
                    tiles[tile.boardPosition] = Instantiate(groupPropertyTilePrefab, tile.position, tile.rotation);
                    break;
                case "community":
                    tiles[tile.boardPosition] = Instantiate(communityTilePrefab, tile.position, tile.rotation);
                    break;
                case "pay":
                    tiles[tile.boardPosition] = Instantiate(payTilePrefab, tile.position, tile.rotation);
                    break;
                case "train":
                    tiles[tile.boardPosition] = Instantiate(stationTilePrefab, tile.position, tile.rotation);
                    break;
                case "chance":
                    tiles[tile.boardPosition] = Instantiate(chanceTilePrefab, tile.position, tile.rotation);
                    break;
                case "prison":
                    tiles[tile.boardPosition] = Instantiate(prisonTilePrefab, tile.position, tile.rotation);
                    break;
                case "parking":
                    tiles[tile.boardPosition] = Instantiate(parkingTilePrefab, tile.position, tile.rotation);
                    break;
                case "goToPrison":
                    tiles[tile.boardPosition] = Instantiate(goToPrisonTilePrefab, tile.position, tile.rotation);
                    break;
                default:
                    Debug.LogError("Unknown tile: " + tile.type);
                    break;
            }

            tiles[tile.boardPosition].name = $"Tile {tile.boardPosition}";
            tiles[tile.boardPosition].SetId(tile.boardPosition);
            tiles[tile.boardPosition].SetTileName(tile.name);
            tiles[tile.boardPosition].SetGameController(gameController);

            if (tile.type == "groupProperty")
            {
                ((GroupPropertyTile)tiles[tile.boardPosition]).SetPoints(tile.points);
                ((GroupPropertyTile)tiles[tile.boardPosition]).SetGroupColor(tile.groupColor);
            }
            else if (tile.type == "pay")
                ((PayTile)tiles[tile.boardPosition]).SetPoints(tile.points);
            else if (tile.type == "train")
                ((StationTile)tiles[tile.boardPosition]).SetPoints(tile.points);
            else if (tile.type == "prison")
                ((PrisonTile)tiles[tile.boardPosition]).SetPoints(20);     // TODO: mudar!!
            else if (tile.type == "parking")
                ((ParkingTile)tiles[tile.boardPosition]).SetPoints(20);     // TODO: mudar!!
            else if (tile.type == "goToPrison")
                ((GoToPrisonTile)tiles[tile.boardPosition]).SetPoints(0);     // TODO: mudar!!

            tilesDictionary.Add(tile._id, tiles[tile.boardPosition]);
        }

        BoardBase boardBase = Instantiate(boardBasePrefab, new Vector3(4, 5.3f), Quaternion.identity);
        boardBase.name = "Board base";

        Debug.Log("Board loaded");
    }

    // Set game manager
    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
    }
}
