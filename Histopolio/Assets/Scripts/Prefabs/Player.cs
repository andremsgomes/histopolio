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
    private string imageURL;
    private int score;
    private int position;
    private int moveSpaces;
    private int playOrder;

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

                // TODO: mudar
                if (GetTile().GetId() % 10 == 0)
                    gameController.SendInfoShownMessageToServer();
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

    // Ser play order
    public void SetPlayOrder(int playOrder)
    {
        this.playOrder = playOrder;
    }

    // Get play order
    public int GetPlayOrder()
    {
        return playOrder;
    }

    // Set player color
    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
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

    // Get player color
    public Color GetColor()
    {
        return GetComponent<SpriteRenderer>().color;
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
        score += ((GroupPropertyTile)tile).GetPoints();
    }

    // Set points
    public void SetScore(int score)
    {
        this.score = score;
    }

    // Set image avatar
    public void SetImage(string image) {
        this.imageURL = image;

        StartCoroutine("ChangeSprite");
    }

    // Change sprite of the player object
    IEnumerator ChangeSprite() {
        WWW www = new WWW(this.imageURL);
        yield return www;
        img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
