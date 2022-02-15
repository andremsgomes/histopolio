using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private bool playerCamera;
    private GameManager gameManager;

    [SerializeField] private Camera camera;

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
        camera.transform.position = new Vector3(3.5f, 4.8f, -10);
        camera.orthographicSize = 6;

        playerCamera = false;
    }

    // Set camera to player
    private void setPlayerCamera(Vector3 playerPosition) {
        camera.transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
        camera.orthographicSize = 3.4f;

        playerCamera = true;
    }


    // Change between player camera and board camera
    public void ToggleCamera() {
        if (playerCamera)
            SetBoardCamera();
        else
            setPlayerCamera(gameManager.GetPlayerPosition());
    }
}
