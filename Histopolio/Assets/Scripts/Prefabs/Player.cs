using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Tile tile;
    private GameController gameController;
    private int id;
    private string playerName;
    private int score;
    private int position;
    private int moveSpaces;
    private int numTurns;

    [SerializeField] private float speed;
    [SerializeField] private Image img;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (moveSpaces > 0)
        {
            MoveTo(gameController.GetTile(position + 1));

            if (moveSpaces == 0)
            {
                tile.PerformAction();
            }
        }
    }

    private void MoveTo(Tile tile)
    {
        float step = speed * Time.deltaTime;
        Vector3 target = new Vector3(tile.transform.position.x, tile.transform.position.y, -3);

        this.transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (transform.position == target)
        {
            SetTile(tile);
            SetPosition(position+1);
            moveSpaces--;
        }
    }

    // Move is called after a dice is rolled
    public void Move(int spaces)
    {
        moveSpaces = spaces;
    }

    // Set tile
    public void SetTile(Tile tile)
    {
        this.tile = tile;
    }

    // Set game manager
    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
    }

    // Set Player id
    public void SetId(int id)
    {
        this.id = id;
    }

    // Get player id
    public int GetId()
    {
        return id;
    }

    // Set player name
    public void SetName(string playerName)
    {
        this.playerName = playerName;
    }

    // Get player name
    public string GetPlayerName()
    {
        return playerName;
    }

    // Add points to score
    public void AddPoints(int points)
    {
        score += points;

        if (score < 0)
            score = 0;
    }

    // Get score
    public int GetScore()
    {
        return score;
    }

    // Set position
    public void SetPosition(int position) {
        this.position = position;
    }

    // Get position
    public int GetPosition() {
        return position;
    }

    // Get tile
    public Tile GetTile()
    {
        return tile;
    }

    // Receive points from current tile
    public void ReceivePointsFromTile()
    {
        score += ((PointsTile)tile).GetPoints();
    }

    // Set points
    public void SetScore(int score)
    {
        this.score = score;
    }

    // Set image avatar
    public void SetAvatar(Sprite avatar) {
        img.sprite = avatar;
    }

    // Get avatar
    public Sprite GetAvatar() {
        return img.sprite;
    }

    // Set number of turns played
    public void SetNumTurns(int numTurns) {
        this.numTurns = numTurns;
    }

    // Get number of turns played
    public int GetNumTurns() {
        return numTurns;
    }

    // Add turn played
    public void AddTurn() {
        numTurns += 1;
    }
}
