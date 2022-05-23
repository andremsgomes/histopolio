using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonTile : Tile
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
        gameController.SendInfoShownMessageToServer();
        gameController.FinishTurn("");
    }

    // Get rotation for the camera
    public override Quaternion GetCameraRotation() {
        return Quaternion.Euler(0, 0, -45);
    }
}
