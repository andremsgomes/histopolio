using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Tile tile;
    private GameManager gameManager;
    private int id;
    private string playerName;
    private int score;
    private int moveSpaces;

    [SerializeField] private float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSpaces > 0) {
            MoveTo(gameManager.GetTile(tile.GetId()+1));

            if (moveSpaces == 0)
                gameManager.FinishTurn();
        }
    }

    private void MoveTo(Tile tile) {
        float step = speed * Time.deltaTime;
        Vector3 target = new Vector3(tile.transform.position.x, tile.transform.position.y, -3);

        this.transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (transform.position == target) {
            SetTile(tile);
            moveSpaces--;
        }
    }

    // Move is called after a dice is rolled
    public void Move(int spaces) {
        moveSpaces = spaces;
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
    public void SetName(string playerName) {
        this.playerName = playerName;
    }

    // Get player name
    public string GetPlayerName() {
        return playerName;
    }

    // Get player color
    public Color GetColor() {
        return GetComponent<SpriteRenderer>().color;
    }

    // Add points to score
    public void AddPoints(int points) {
        score += points;
    }

    // Get score
    public int GetScore() {
        return score;
    }

    // Get tile
    public Tile GetTile() {
        return tile;
    }
}
