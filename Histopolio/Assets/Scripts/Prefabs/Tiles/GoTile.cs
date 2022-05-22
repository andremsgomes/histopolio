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
        else if (playersList.Count < 3)
        {
            Vector3 scale = new Vector3(0.8f, 0.8f, 1);

            playersList[0].SetScale(scale);
            playersList[1].SetScale(scale);

            playersList[0].SetPosition(new Vector3(transform.position.x - 0.35f, transform.position.y - 0.35f, -3));
            playersList[1].SetPosition(new Vector3(transform.position.x + 0.35f, transform.position.y + 0.35f, -3));
        }
        else if (playersList.Count < 5) {
            Vector3 scale = new Vector3(0.7f, 0.7f, 1);

            for (int i = 0; i < players.Count; i++)
            {
                playersList[i].SetScale(scale);
            }

            centerFour(playersList, 0, transform.position.x, transform.position.y, 0.4f, 0.7071068f);
        }
    }

    // Get rotation for the camera
    public override Quaternion GetCameraRotation()
    {
        return Quaternion.Euler(0, 0, 45);
    }
}
