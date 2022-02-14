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

    // Change camera settings
    void SetCamera() {
        camera.transform.position = new Vector3(3.5f, 4.8f, -10);
        camera.orthographicSize = 6;

        // camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        // camera.orthographicSize = 3.4f;
    }
}
