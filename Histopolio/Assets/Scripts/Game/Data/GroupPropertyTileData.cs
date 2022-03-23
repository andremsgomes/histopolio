using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroupPropertyTileData : TileData {
    public int points;
    public Color groupColor;

    
    public GroupPropertyTileData(int id, string tileName, Vector3 position, Quaternion rotation, int points, Color groupColor) : base(id, tileName, position, rotation) {
        this.points = points;
        this.groupColor = groupColor;
    }
}