using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardData {
    public List<GroupPropertyTileData> groupPropertyTiles = new List<GroupPropertyTileData>();
    public List<CommunityTileData> communityTiles = new List<CommunityTileData>();
    public List<PayTileData> payTiles = new List<PayTileData>();
    public List<StationTileData> stationTiles = new List<StationTileData>();
    public List<ChanceTileData> chanceTiles = new List<ChanceTileData>();
    public GoTileData goTileData;
    public PrisonTileData prisonTileData;
    public ParkingTileData parkingTileData;
    public GoToPrisonTileData goToPrisonTileData;
}