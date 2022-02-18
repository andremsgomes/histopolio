using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player[] players;
    private Color[] playerColors;
    private GridManager gridManager;
    private CameraManager cameraManager;
    private UIManager uiManager;
    private Player currentPlayer;

    [SerializeField] private Player playerPrefab;

    [Header("Number of Players")]
    [SerializeField] private int numPlayers;

    [Header("Players' Colors")]
    [SerializeField] private Color player1Color;
    [SerializeField] private Color player2Color;
    [SerializeField] private Color player3Color;
    [SerializeField] private Color player4Color;
    [SerializeField] private Color player5Color;
    [SerializeField] private Color player6Color;


    // Start is called before the first frame update
    void Start()
    {
        gridManager = this.GetComponent<GridManager>();
        cameraManager = this.GetComponent<CameraManager>();
        uiManager = this.GetComponent<UIManager>();

        gridManager.GenerateGrid();

        cameraManager.SetGameManager(this);
        cameraManager.SetBoardCamera();

        uiManager.SetGameManager(this);

        SetColors();

        SpawnPlayers();
    }
    
    // Spawn players on GO Tile
    void SpawnPlayers() {
        players = new Player[numPlayers];

        Tile firstTile = gridManager.GetTile(0);

        for (int i = 0; i < numPlayers; i++) {
            players[i] = Instantiate(playerPrefab, new Vector3(firstTile.transform.position.x, firstTile.transform.position.y, -3), Quaternion.identity);
            players[i].name = $"Player {i+1}";
            players[i].SetGameManager(this);
            players[i].SetTile(firstTile);
            players[i].SetId(i);
            players[i].SetColor(playerColors[i]);
        }

        currentPlayer = players[0];
    }

    // Change camera
    public void ChangeCamera() {
        cameraManager.ToggleCamera();
    }

    // Get player position
    public Vector3 GetPlayerPosition() {
        return currentPlayer.transform.position;
    }

    // Move player after rolled dice
    public void MovePlayer() {
        int diceResult = Random.Range(1,7);
        Debug.Log(diceResult);
        currentPlayer.Move(diceResult);

        ChangeCurrentPlayer();
    }

    // Get tile with tile id
    public Tile GetTile(int tileId) {
        if (tileId >= 40)
            tileId = tileId-40;

        return gridManager.GetTile(tileId);
    }

    // Change current player
    void ChangeCurrentPlayer() {
        int newId = currentPlayer.GetId()+1;

        if (newId >= numPlayers)
            newId = 0;

        currentPlayer = players[newId];

        cameraManager.ChangePlayerCamera(currentPlayer.transform.position);
    }

    void SetColors() {
        playerColors = new Color[6];

        playerColors[0] = player1Color;
        playerColors[1] = player2Color;
        playerColors[2] = player3Color;
        playerColors[3] = player4Color;
        playerColors[4] = player5Color;
        playerColors[5] = player6Color;
    }
}
