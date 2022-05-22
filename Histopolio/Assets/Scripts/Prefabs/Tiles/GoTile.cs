using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTile : CardTile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Resize players to fit tile
    public override void ResizePlayers()
    {
        List<Player> playersList = new List<Player>(players.Values);

        if (playersList.Count == 0) return;

        if (playersList.Count < 2)
        {
            playersList[0].SetScale(new Vector3(1, 1, 1));
            playersList[0].SetPosition(new Vector3(transform.position.x, transform.position.y, -3));
        }
    }

    // Get rotation for the camera
    public override Quaternion GetCameraRotation() {
        return Quaternion.Euler(0, 0, 45);
    }
}
