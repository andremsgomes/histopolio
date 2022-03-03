using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tile : MonoBehaviour
{
    private int id;
    private string tileType;
    private string tileName;

    [SerializeField] private Text tileNameText;

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
    public string GetTileType() {
        return tileType;
    }

    // Set type
    public void SetTileType(string tileType) {
        this.tileType = tileType;
    }

    // Get name
    public string GetTileName() {
        return tileName;
    }

    // Set name
    public void SetTileName(string tileName) {
        this.tileName = tileName;
        tileNameText.text = tileName;
    }
}
