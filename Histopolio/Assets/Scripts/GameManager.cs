using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;
    private GridManager gridManager;
    private CameraManager cameraManager;
    private UIManager uiManager;

    [SerializeField] private Player playerPrefab;


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

        SpawnPlayer();
    }
    
    // Spawn player on GO Tile
    void SpawnPlayer() {
        Tile firstTile = gridManager.GetTile(0);

        player = Instantiate(playerPrefab, new Vector3(firstTile.transform.position.x, firstTile.transform.position.y, -3), Quaternion.identity);
        player.name = "Player";
        player.SetGameManager(this);
        player.SetTile(firstTile);
    }

    // Change camera
    public void ChangeCamera() {
        cameraManager.ToggleCamera();
    }

    // Get player position
    public Vector3 GetPlayerPosition() {
        return player.transform.position;
    }

    // Move player after rolled dice
    public void MovePlayer() {
        int diceResult = Random.Range(1,7);
        Debug.Log(diceResult);
        player.Move(diceResult);
    }

    // Get tile with tile id
    public Tile GetTile(int tileId) {
        if (tileId >= 40)
            tileId = tileId-40;

        return gridManager.GetTile(tileId);
    }
}
