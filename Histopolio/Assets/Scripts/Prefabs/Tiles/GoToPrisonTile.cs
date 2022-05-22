using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPrisonTile : CardTile
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
        gameController.MovePlayerTo(10);
    }

    // Get rotation for the tile
    public override Quaternion GetCameraRotation() {
        return Quaternion.Euler(0, 0, 135);
    }
}
