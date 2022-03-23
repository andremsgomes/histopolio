using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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