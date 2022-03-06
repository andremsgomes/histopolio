using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tile : MonoBehaviour
{
    private int id;
    private string tileType;
    private string tileName;
    protected GameManager gameManager;

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

    // Get type
    public abstract string GetTileType();

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
    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }
}
