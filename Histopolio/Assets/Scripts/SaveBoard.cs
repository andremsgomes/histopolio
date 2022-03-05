using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveIntoJson(Tile[] tiles) {
        BoardData boardData = new BoardData();

        for (int i = 0; i < tiles.Length; i++) {
            if (tiles[i].GetTileType().Equals("groupPropertyTile")) {
                GroupPropertyTile groupPropertyTile = (GroupPropertyTile)tiles[i];
                boardData.groupPropertyTiles.Add(new GroupPropertyTileData(groupPropertyTile.GetId(), groupPropertyTile.GetTileType(), groupPropertyTile.GetTileName(), groupPropertyTile.transform.position, groupPropertyTile.transform.rotation, groupPropertyTile.GetPoints(), groupPropertyTile.GetGroupColor()));
            }
            else if (tiles[i].GetTileType().Equals("goTile"))
                boardData.goTileData = new GoTileData(tiles[i].GetId(), tiles[i].GetTileType(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
            else if (tiles[i].GetTileType().Equals("prisonTile"))
                boardData.prisonTileData = new PrisonTileData(tiles[i].GetId(), tiles[i].GetTileType(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
            else if (tiles[i].GetTileType().Equals("parkingTile"))
                boardData.parkingTileData = new ParkingTileData(tiles[i].GetId(), tiles[i].GetTileType(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
            else if (tiles[i].GetTileType().Equals("goToPrisonTile"))
                boardData.goToPrisonTileData = new GoToPrisonTileData(tiles[i].GetId(), tiles[i].GetTileType(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation);
            else if (tiles[i].GetTileType().Equals("communityTile"))
                boardData.communityTiles.Add(new CommunityTileData(tiles[i].GetId(), tiles[i].GetTileType(), tiles[i].GetTileName(), tiles[i].transform.position, tiles[i].transform.rotation));
        }

        string board = JsonUtility.ToJson(boardData);
        System.IO.File.WriteAllText(Application.dataPath + "/BoardData.json", board);
    }
}

[System.Serializable]
public class BoardData {
    public List<GroupPropertyTileData> groupPropertyTiles = new List<GroupPropertyTileData>();
    public List<CommunityTileData> communityTiles = new List<CommunityTileData>();
    public GoTileData goTileData;
    public PrisonTileData prisonTileData;
    public ParkingTileData parkingTileData;
    public GoToPrisonTileData goToPrisonTileData;
    // TODO: meter tiles iguais ao tile original numa lista de others
}

[System.Serializable]
public class TileData {
    public int id;
    public string tileType;
    public string tileName;
    public Vector3 position;
    public Quaternion rotation;


    public TileData(int id, string tileType, string tileName, Vector3 position, Quaternion rotation) {
        this.id = id;
        this.tileType = tileType;
        this.tileName = tileName;
        this.position = position;
        this.rotation = rotation;
    }
}

[System.Serializable]
public class GroupPropertyTileData : TileData {
    public int points;
    public Color groupColor;

    
    public GroupPropertyTileData(int id, string tileType, string tileName, Vector3 position, Quaternion rotation, int points, Color groupColor) : base(id, tileType, tileName, position, rotation) {
        this.points = points;
        this.groupColor = groupColor;
    }
}

[System.Serializable]
public class GoTileData : TileData {
    public GoTileData(int id, string tileType, string tileName, Vector3 position, Quaternion rotation) : base(id, tileType, tileName, position, rotation) {

    }
}

[System.Serializable]
public class PrisonTileData : TileData {
    public PrisonTileData(int id, string tileType, string tileName, Vector3 position, Quaternion rotation) : base(id, tileType, tileName, position, rotation) {

    }
}

[System.Serializable]
public class ParkingTileData : TileData {
    public ParkingTileData(int id, string tileType, string tileName, Vector3 position, Quaternion rotation) : base(id, tileType, tileName, position, rotation) {

    }
}

[System.Serializable]
public class GoToPrisonTileData : TileData {
    public GoToPrisonTileData(int id, string tileType, string tileName, Vector3 position, Quaternion rotation) : base(id, tileType, tileName, position, rotation) {

    }
}

[System.Serializable]
public class CommunityTileData : TileData {
    public CommunityTileData(int id, string tileType, string tileName, Vector3 position, Quaternion rotation) : base(id, tileType, tileName, position, rotation) {

    }
}
