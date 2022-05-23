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
    private int totalAnswers;
    private int correctAnswers;
    private List<Sprite> badges = new List<Sprite>();
    private int multiplier;
    private bool finishedBoard;

    [SerializeField] private float speed;
    [SerializeField] private Image img;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!finishedBoard && moveSpaces != 0)
        {
            if (moveSpaces > 0)
                MoveTo(gameController.GetTile(position + 1));
            else
                MoveTo(gameController.GetTile(position - 1));

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

            if (moveSpaces > 0)
            {
                SetPosition(position + 1);
                moveSpaces--;
            }
            else
            {
                SetPosition(position - 1);
                moveSpaces++;
            }

            if (tile.GetId() == 0) {
                finishedBoard = true;
                moveSpaces = 0;
            }
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
    public void SetPosition(int position)
    {
        this.position = position;
    }

    // Get position
    public int GetPosition()
    {
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
        int points = ((PointsTile)tile).GetPoints();
        if (points > 0) points *= multiplier;

        score += points;
    }

    // Set points
    public void SetScore(int score)
    {
        this.score = score;
    }

    // Set image avatar
    public void SetAvatar(Sprite avatar)
    {
        img.sprite = avatar;
    }

    // Get avatar
    public Sprite GetAvatar()
    {
        return img.sprite;
    }

    // Set number of turns played
    public void SetNumTurns(int numTurns)
    {
        this.numTurns = numTurns;
    }

    // Get number of turns played
    public int GetNumTurns()
    {
        return numTurns;
    }

    // Add turn played
    public void AddTurn()
    {
        numTurns += 1;
    }

    // Set total answers
    public void SetTotalAnswers(int totalAnswers)
    {
        this.totalAnswers = totalAnswers;
    }

    // Add answer
    public void AddAnswer()
    {
        totalAnswers += 1;
    }

    // Get total answers
    public int GetTotalAnswers()
    {
        return totalAnswers;
    }

    // Set correct answers
    public void SetCorrectAnswers(int correctAnswers)
    {
        this.correctAnswers = correctAnswers;
    }

    // Add correct answer
    public void AddCorrectAnswer()
    {
        correctAnswers += 1;
    }

    // Get correct answers
    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    // Set badges
    public void SetBadges(List<Sprite> badges)
    {
        this.badges = badges;
    }

    // Add a badge
    public void AddBadge(Sprite badge)
    {
        badges.Add(badge);
    }

    // Get badges
    public List<Sprite> GetBadges()
    {
        return badges;
    }

    // Set multiplier
    public void SetMultiplier(int multiplier)
    {
        this.multiplier = multiplier;
    }

    // Get multiplier
    public int GetMultiplier()
    {
        return multiplier;
    }

    // Set scale
    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    // Set position
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    // Set finished board
    public void SetFinishedBoard(bool finishedBoard)
    {
        this.finishedBoard = finishedBoard;
    }

    // Get finished board
    public bool GetFinishedBoard()
    {
        return finishedBoard;
    }
}
