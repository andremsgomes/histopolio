using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PayTileData : TileData {
    public int points;

    
    public PayTileData(int id, string tileName, Vector3 position, Quaternion rotation, int points) : base(id, tileName, position, rotation) {
        this.points = points;
    }
}