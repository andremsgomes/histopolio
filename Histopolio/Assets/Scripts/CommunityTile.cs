using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityTile : Tile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Perform action when player arrives to tile
    public override void PerformAction() {
        return;
    }

    // Get type
    public override string GetTileType()
    {
        return "communityTile";
    }
}
