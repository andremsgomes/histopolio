using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StationTileData : TileData {
    public int points;

    
    public StationTileData(int id, string tileName, Vector3 position, Quaternion rotation, int points) : base(id, tileName, position, rotation) {
        this.points = points;
    }
}