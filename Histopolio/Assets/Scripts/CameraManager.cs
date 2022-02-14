using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera camera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Set camera to show all board
    public void SetBoardCamera() {
        camera.transform.position = new Vector3(3.5f, 4.8f, -10);
        camera.orthographicSize = 6;
    }

    // Set camera to player
    public void setPlayerCamera(Vector3 playerPosition) {
        camera.transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
        camera.orthographicSize = 3.4f;
    }
}
