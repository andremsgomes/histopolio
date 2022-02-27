using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Tile tile;
    private GameManager gameManager;
    private int id;
    private string name;

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

    // Set Player id
    public void SetId(int id) {
        this.id = id;
    }

    // Get player id
    public int GetId() {
        return id;
    }

    // Set player color
    public void SetColor(Color color) {
        GetComponent<SpriteRenderer>().color = color;
    }

    // Set player name
    public void SetName(string name) {
        this.name = name;
    }

    // Get player name
    public string GetName() {
        return name;
    }

    // Get player color
    public Color GetColor() {
        return GetComponent<SpriteRenderer>().color;
    }
}
