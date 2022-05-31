using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData {
    public string _id;
    public int boardPosition;
    public string name;
    public string type;
    public Vector3 position;
    public Quaternion rotation;
    public int points;
    public Color groupColor;
}