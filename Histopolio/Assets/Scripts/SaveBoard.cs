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

        for (int i = 0; i < boardData.tiles.Length; i++) {
            TileData tileData = new TileData();
            
            tileData.id = tiles[i].GetId();
            tileData.tileType = tiles[i].GetTileType();
            tileData.tileName = "testName";

            boardData.tiles[i] = tileData;
        }

        string board = JsonUtility.ToJson(boardData);
        System.IO.File.WriteAllText(Application.dataPath + "/BoardData.json", board);
    }
}

[System.Serializable]
public class BoardData {
    public TileData[] tiles = new TileData[40];
}

[System.Serializable]
public class TileData {
    public int id;
    public string tileType;
    public string tileName;
    // public Vector3 position;
    // public Quaternion rotation;
}

[System.Serializable]
public class GroupPropertyTileData : TileData {
    public int points;
    public Color groupColor;
}
