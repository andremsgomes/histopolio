using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Tile tile;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Move is called after a dice is rolled
    public void Move(int spaces) {
        SetTile(gameManager.GetTile(tile.GetId()+spaces));
        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, transform.position.z);
    }

    // Set tile
    public void SetTile(Tile tile) {
        this.tile = tile;
    }

    // Set game manager
    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }
}
