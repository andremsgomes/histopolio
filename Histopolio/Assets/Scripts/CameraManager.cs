using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
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

    }

    // Set game manager
    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }

    // Set camera to show all board
    public void SetBoardCamera() {
        gameCamera.transform.position = new Vector3(4, 5.3f, -10);
        gameCamera.orthographicSize = 7;

        playerCamera = false;
    }

    // Set camera to player
    void SetPlayerCamera(Vector3 playerPosition) {
        gameCamera.transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
        gameCamera.orthographicSize = 3.4f;

        playerCamera = true;
    }


    // Change between player camera and board camera
    public void ToggleCamera() {
        if (playerCamera)
            SetBoardCamera();
        else
            SetPlayerCamera(gameManager.GetPlayerPosition());
    }

    // Change player camera if active
    public void ChangePlayerCamera(Vector3 playerPosition) {
        if (playerCamera)
            SetPlayerCamera(playerPosition);
    }
}
