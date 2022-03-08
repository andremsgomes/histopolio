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
        }

        string board = JsonUtility.ToJson(boardData);
        System.IO.File.WriteAllText(Application.dataPath + "/BoardData.json", board);
    }
}

[System.Serializable]
public class BoardData {
    public List<GroupPropertyTileData> groupPropertyTiles = new List<GroupPropertyTileData>();
    public List<CommunityTileData> communityTiles = new List<CommunityTileData>();
    public List<PayTileData> payTiles = new List<PayTileData>();
    public List<StationTileData> stationTiles = new List<StationTileData>();
    public GoTileData goTileData;
    public PrisonTileData prisonTileData;
    public ParkingTileData parkingTileData;
    public GoToPrisonTileData goToPrisonTileData;
}

[System.Serializable]
public class TileData {
    public int id;
    public string tileName;
    public Vector3 position;
    public Quaternion rotation;


    public TileData(int id, string tileName, Vector3 position, Quaternion rotation) {
        this.id = id;
        this.tileName = tileName;
        this.position = position;
        this.rotation = rotation;
    }
}

[System.Serializable]
public class GroupPropertyTileData : TileData {
    public int points;
    public Color groupColor;

    
    public GroupPropertyTileData(int id, string tileName, Vector3 position, Quaternion rotation, int points, Color groupColor) : base(id, tileName, position, rotation) {
        this.points = points;
        this.groupColor = groupColor;
    }
}

[System.Serializable]
public class GoTileData : TileData {
    public GoTileData(int id, string tileName, Vector3 position, Quaternion rotation) : base(id, tileName, position, rotation) {

    }
}

[System.Serializable]
public class PrisonTileData : TileData {
    public PrisonTileData(int id, string tileName, Vector3 position, Quaternion rotation) : base(id, tileName, position, rotation) {

    }
}

[System.Serializable]
public class ParkingTileData : TileData {
    public ParkingTileData(int id, string tileName, Vector3 position, Quaternion rotation) : base(id, tileName, position, rotation) {

    }
}

[System.Serializable]
public class GoToPrisonTileData : TileData {
    public GoToPrisonTileData(int id, string tileName, Vector3 position, Quaternion rotation) : base(id, tileName, position, rotation) {

    }
}

[System.Serializable]
public class CommunityTileData : TileData {
    public CommunityTileData(int id, string tileName, Vector3 position, Quaternion rotation) : base(id, tileName, position, rotation) {

    }
}

[System.Serializable]
public class PayTileData : TileData {
    public int points;

    
    public PayTileData(int id, string tileName, Vector3 position, Quaternion rotation, int points) : base(id, tileName, position, rotation) {
        this.points = points;
    }
}

[System.Serializable]
public class StationTileData : TileData {
    public int points;

    
    public StationTileData(int id, string tileName, Vector3 position, Quaternion rotation, int points) : base(id, tileName, position, rotation) {
        this.points = points;
    }
}
