using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tile : MonoBehaviour
{
    private int id;
    private string tileName;
    protected GameController gameController;
    protected Dictionary<int, Player> players = new Dictionary<int, Player>();

    [SerializeField] private Text tileNameText;     // TODO: meter só no group

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Set id
    public void SetId(int id)
    {
        this.id = id;
    }

    // Get id
    public int GetId()
    {
        return id;
    }

    // Perform tile action when player arrives at tile
    public abstract void PerformAction();

    // Get name
    public string GetTileName()
    {
        return tileName;
    }

    // Set name
    public void SetTileName(string tileName)
    {
        this.tileName = tileName;
        tileNameText.text = tileName;
    }

    // Set game manager
    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
    }

    // Get rotation for the camera
    public virtual Quaternion GetCameraRotation()
    {
        return transform.rotation;
    }

    // Resize players to fit tile
    public virtual void ResizePlayers()
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
            Vector3 scale = new Vector3(0.7f, 0.7f, 1);

            playersList[0].SetScale(scale);
            playersList[1].SetScale(scale);

            if (transform.rotation.z == 0)
            {
                playersList[0].SetPosition(new Vector3(transform.position.x, transform.position.y + 0.42f, -3));
                playersList[1].SetPosition(new Vector3(transform.position.x, transform.position.y - 0.42f, -3));
            }
            else if (transform.rotation.z == 0.7071068f)
            {
                playersList[0].SetPosition(new Vector3(transform.position.x - 0.42f, transform.position.y, -3));
                playersList[1].SetPosition(new Vector3(transform.position.x + 0.42f, transform.position.y, -3));
            }
            else if (transform.rotation.z == 1)
            {
                playersList[0].SetPosition(new Vector3(transform.position.x, transform.position.y - 0.42f, -3));
                playersList[1].SetPosition(new Vector3(transform.position.x, transform.position.y + 0.42f, -3));
            }
            else if (transform.rotation.z == -0.7071068f)
            {
                playersList[0].SetPosition(new Vector3(transform.position.x + 0.42f, transform.position.y, -3));
                playersList[1].SetPosition(new Vector3(transform.position.x - 0.42f, transform.position.y, -3));
            }
        }
        else if (playersList.Count < 5)
        {
            Vector3 scale = new Vector3(0.45f, 0.45f, 1);

            for (int i = 0; i < players.Count; i++)
            {
                playersList[i].SetScale(scale);
            }

            centerFour(playersList, 0, transform.position.x, transform.position.y, 0.25f, transform.rotation.z);
        }
        else if (playersList.Count < 9)
        {
            Vector3 scale = new Vector3(0.38f, 0.38f, 1);

            for (int i = 0; i < players.Count; i++)
            {
                playersList[i].SetScale(scale);
            }

            if (transform.rotation.z == 0)
            {
                centerFour(playersList, 0, transform.position.x, transform.position.y + 0.4f, 0.2f, transform.rotation.z);
                centerFour(playersList, 4, transform.position.x, transform.position.y - 0.4f, 0.2f, transform.rotation.z);
            }
            else if (transform.rotation.z == 0.7071068f)
            {
                centerFour(playersList, 0, transform.position.x - 0.4f, transform.position.y, 0.2f, transform.rotation.z);
                centerFour(playersList, 4, transform.position.x + 0.4f, transform.position.y, 0.2f, transform.rotation.z);
            }
            else if (transform.rotation.z == 1)
            {
                centerFour(playersList, 0, transform.position.x, transform.position.y - 0.4f, 0.2f, transform.rotation.z);
                centerFour(playersList, 4, transform.position.x, transform.position.y + 0.4f, 0.2f, transform.rotation.z);
            }
            else if (transform.rotation.z == -0.7071068f)
            {
                centerFour(playersList, 0, transform.position.x + 0.4f, transform.position.y, 0.2f, transform.rotation.z);
                centerFour(playersList, 4, transform.position.x - 0.4f, transform.position.y, 0.2f, transform.rotation.z);
            }
        }
        else if (playersList.Count < 17)
        {
            Vector3 scale = new Vector3(0.25f, 0.25f, 1);

            for (int i = 0; i < players.Count; i++)
            {
                playersList[i].SetScale(scale);
            }

            if (transform.rotation.z == 0)
            {
                centerFour(playersList, 0, transform.position.x-0.25f, transform.position.y+0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 4, transform.position.x+0.25f, transform.position.y+0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 8, transform.position.x-0.25f, transform.position.y-0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 12, transform.position.x+0.25f, transform.position.y-0.25f, 0.125f, transform.rotation.z);
            }
            else if (transform.rotation.z == 0.7071068f)
            {
                centerFour(playersList, 0, transform.position.x-0.25f, transform.position.y-0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 4, transform.position.x-0.25f, transform.position.y+0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 8, transform.position.x+0.25f, transform.position.y-0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 12, transform.position.x+0.25f, transform.position.y+0.25f, 0.125f, transform.rotation.z);
            }
            else if (transform.rotation.z == 1)
            {
                centerFour(playersList, 0, transform.position.x+0.25f, transform.position.y-0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 4, transform.position.x-0.25f, transform.position.y-0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 8, transform.position.x+0.25f, transform.position.y+0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 12, transform.position.x-0.25f, transform.position.y+0.25f, 0.125f, transform.rotation.z);
            }
            else if (transform.rotation.z == -0.7071068f)
            {
                centerFour(playersList, 0, transform.position.x+0.25f, transform.position.y+0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 4, transform.position.x+0.25f, transform.position.y-0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 8, transform.position.x-0.25f, transform.position.y+0.25f, 0.125f, transform.rotation.z);
                centerFour(playersList, 12, transform.position.x-0.25f, transform.position.y-0.25f, 0.125f, transform.rotation.z);
            }
            else {
                // TODO: mudar
                foreach (Player player in playersList)
                {
                    player.SetScale(new Vector3(1, 1, 1));
                    player.SetPosition(new Vector3(transform.position.x, transform.position.y, -3));
                }
            }
        }
    }

    // Center four players
    protected void centerFour(List<Player> playersList, int i, float x, float y, float offset, float rotation)
    {
        if (rotation == 0)
        {
            if (i < playersList.Count) playersList[i].SetPosition(new Vector3(x - offset, y + offset, -3));
            if (i + 1 < playersList.Count) playersList[i + 1].SetPosition(new Vector3(x + offset, y + offset, -3));
            if (i + 2 < playersList.Count) playersList[i + 2].SetPosition(new Vector3(x - offset, y - offset, -3));
            if (i + 3 < playersList.Count) playersList[i + 3].SetPosition(new Vector3(x + offset, y - offset, -3));
        }
        else if (rotation == 0.7071068f)
        {
            if (i < playersList.Count) playersList[i].SetPosition(new Vector3(x - offset, y - offset, -3));
            if (i + 1 < playersList.Count) playersList[i + 1].SetPosition(new Vector3(x - offset, y + offset, -3));
            if (i + 2 < playersList.Count) playersList[i + 2].SetPosition(new Vector3(x + offset, y - offset, -3));
            if (i + 3 < playersList.Count) playersList[i + 3].SetPosition(new Vector3(x + offset, y + offset, -3));
        }
        else if (rotation == 1)
        {
            if (i < playersList.Count) playersList[i].SetPosition(new Vector3(x + offset, y - offset, -3));
            if (i + 1 < playersList.Count) playersList[i + 1].SetPosition(new Vector3(x - offset, y - offset, -3));
            if (i + 2 < playersList.Count) playersList[i + 2].SetPosition(new Vector3(x + offset, y + offset, -3));
            if (i + 3 < playersList.Count) playersList[i + 3].SetPosition(new Vector3(x - offset, y + offset, -3));
        }
        else if (rotation == -0.7071068f)
        {
            if (i < playersList.Count) playersList[i].SetPosition(new Vector3(x + offset, y + offset, -3));
            if (i + 1 < playersList.Count) playersList[i + 1].SetPosition(new Vector3(x + offset, y - offset, -3));
            if (i + 2 < playersList.Count) playersList[i + 2].SetPosition(new Vector3(x - offset, y + offset, -3));
            if (i + 3 < playersList.Count) playersList[i + 3].SetPosition(new Vector3(x - offset, y - offset, -3));
        }
    }

    // Add player to tile
    public void AddPlayer(Player player)
    {
        players[player.GetId()] = player;
        player.SetOrder(0);
        ResizePlayers();
    }

    // Remove player from tile
    public void RemovePlayer(Player player)
    {
        players.Remove(player.GetId());
        
        player.SetScale(new Vector3(1, 1, 1));
        player.SetPosition(new Vector3(transform.position.x, transform.position.y, -3));
        player.SetOrder(1);

        ResizePlayers();
    }
}
