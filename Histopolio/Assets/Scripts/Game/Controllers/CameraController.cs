using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool playerCamera;
    private GameManager gameManager;

    [SerializeField] private Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera)
            SetPlayerCamera(gameManager.GetPlayerPosition(), gameManager.GetCurrentTile(), gameManager.GetNextTile());
    }

    // Set game manager
    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }

    // Set camera to show all board
    public void SetBoardCamera() {
        gameCamera.transform.position = new Vector3(4, 5.3f, -10);
        gameCamera.transform.rotation = Quaternion.Euler(0,0,0);
        gameCamera.orthographicSize = 7;

        playerCamera = false;
    }

    // Set camera to player
    void SetPlayerCamera(Vector3 playerPosition, Tile currentTile, Tile nextTile) {
        Vector3 currentTilePosition = currentTile.transform.position;
        Vector3 nextTilePosition = nextTile.transform.position;
        
        // Distance between current and next tile's centers
        float tilesDistance = nextTilePosition.x - currentTilePosition.x;
        if (tilesDistance == 0)
            tilesDistance = nextTilePosition.y - currentTilePosition.y;

        // Distance between player and next tile's centers
        float playerTileDistance = nextTilePosition.x - playerPosition.x;
        if (playerTileDistance == 0)
            playerTileDistance = nextTilePosition.y - playerPosition.y;

        // Percentage of distance covered by player between the two tiles
        float perc_path = (tilesDistance-playerTileDistance) / tilesDistance;

        // Camera rotation difference between tiles
        float tilesRotationDiference = nextTile.GetCameraRotation().eulerAngles.z - currentTile.GetCameraRotation().eulerAngles.z;
        if (tilesRotationDiference > 180)
            tilesRotationDiference = tilesRotationDiference - 360;

        // Adjust camera rotation to keep up with player movement
        float zRotation = currentTile.GetCameraRotation().eulerAngles.z + perc_path * tilesRotationDiference;

        gameCamera.transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
        gameCamera.transform.rotation = Quaternion.Euler(0, 0, zRotation);
        gameCamera.orthographicSize = 3.4f;

        playerCamera = true;
    }


    // Change between player camera and board camera
    public void ToggleCamera() {
        if (playerCamera)
            SetBoardCamera();
        else
            SetPlayerCamera(gameManager.GetPlayerPosition(), gameManager.GetCurrentTile(), gameManager.GetNextTile());
    }
}
