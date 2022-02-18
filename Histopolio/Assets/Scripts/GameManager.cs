using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player[] players;
    private GridManager gridManager;
    private CameraManager cameraManager;
    private UIManager uiManager;
    private Player currentPlayer;

    [SerializeField] private Player playerPrefab;
    [SerializeField] private int numPlayers;


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
            // TODO: change players colors
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

        changeCurrentPlayer();
    }

    // Get tile with tile id
    public Tile GetTile(int tileId) {
        if (tileId >= 40)
            tileId = tileId-40;

        return gridManager.GetTile(tileId);
    }

    // Change current player
    void changeCurrentPlayer() {
        int newId = currentPlayer.GetId()+1;

        if (newId >= numPlayers)
            newId = 0;

        currentPlayer = players[newId];
    }
}
