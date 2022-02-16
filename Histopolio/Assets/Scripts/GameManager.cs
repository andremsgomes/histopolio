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
        player = Instantiate(playerPrefab, new Vector3(8.3f, 0, -3), Quaternion.identity);
        player.name = "Player";
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
        player.Move(1);
    }
}
