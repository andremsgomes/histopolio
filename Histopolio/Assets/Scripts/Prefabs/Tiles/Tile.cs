using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tile : MonoBehaviour
{
    private int id;
    private string tileName;
    protected GameController gameController;

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
    public void SetId(int id) {
        this.id = id;
    }

    // Get id
    public int GetId() {
        return id;
    }

    // Perform tile action when player arrives at tile
    public abstract void PerformAction();

    // Get name
    public string GetTileName() {
        return tileName;
    }

    // Set name
    public void SetTileName(string tileName) {
        this.tileName = tileName;
        tileNameText.text = tileName;
    }

    // Set game manager
    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    // Get rotation for the camera
    public virtual Quaternion GetCameraRotation() {
        return transform.rotation;
    }
}
