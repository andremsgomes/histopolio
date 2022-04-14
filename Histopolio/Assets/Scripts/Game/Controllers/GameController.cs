using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Player[] players;
    private Color[] playerColors;
    private BoardController boardController;
    private CameraController cameraController;
    private GameUI gameUI;
    private Player currentPlayer;

    [Header("Controllers")]
    [SerializeField] private QuestionController questionController;
    [SerializeField] private CardController cardController;
    [SerializeField] private DiceController dice;
    [SerializeField] private MainMenuController mainMenuController;

    [Header("Prefabs")]
    [SerializeField] private Player playerPrefab;

    [Header("Number of Players")]
    [SerializeField] private int numPlayers;

    [Header("Players' Colors")]
    [SerializeField] private Color player1Color;
    [SerializeField] private Color player2Color;
    [SerializeField] private Color player3Color;
    [SerializeField] private Color player4Color;
    [SerializeField] private Color player5Color;
    [SerializeField] private Color player6Color;


    // Start is called before the first frame update
    void Start()
    {
        boardController = this.GetComponent<BoardController>();
        cameraController = this.GetComponent<CameraController>();
        gameUI = this.GetComponent<GameUI>();

        boardController.SetGameController(this);
        boardController.LoadBoard();

        questionController.SetGameController(this);
        questionController.SetQuestionComponents();
        questionController.LoadQuestions("TestQuestions.json");

        cardController.SetGameController(this);
        cardController.SetCardComponents();
        cardController.LoadCards("TestCards.json");

        cameraController.SetGameController(this);
        cameraController.SetBoardCamera();

        gameUI.SetGameController(this);

        dice.SetGameController(this);
        dice.SetDiceComponents();

        mainMenuController.SetGameController(this);
        mainMenuController.SetMainMenuComponents();

        SetColors();

        SpawnPlayers();
    }
    
    // Spawn players on GO Tile
    void SpawnPlayers() {
        players = new Player[numPlayers];

        Tile firstTile = boardController.GetTile(0);

        for (int i = 0; i < numPlayers; i++) {
            players[i] = Instantiate(playerPrefab, new Vector3(firstTile.transform.position.x, firstTile.transform.position.y, -3), Quaternion.identity);
            players[i].name = $"Player {i+1}";
            players[i].SetGameController(this);
            players[i].SetTile(firstTile);
            players[i].SetId(i);
            players[i].SetColor(playerColors[i]);
            players[i].SetName($"Player {i+1}");    // TODO: nome será introduzido pelo utilizador
            players[i].SetScore(20);
        }

        SetCurrentPlayer(players[0]);
    }

    // Change camera
    public void ChangeCamera() {
        cameraController.ToggleCamera();
    }

    // Get player position
    public Vector3 GetPlayerPosition() {
        return currentPlayer.transform.position;
    }

    // Get current tile
    public Tile GetCurrentTile() {
        return currentPlayer.GetTile();
    }

    // Get next tile
    public Tile GetNextTile() {
        int currenTileId = GetCurrentTile().GetId();
        
        return GetTile(currenTileId+1);
    }

    // Move player after rolled dice
    public void MovePlayer(int diceResult) {
        currentPlayer.Move(diceResult);
    }

    // Get tile with tile id
    public Tile GetTile(int tileId) {
        if (tileId >= 40)
            tileId = tileId-40;

        return boardController.GetTile(tileId);
    }

    // Change current player
    public void ChangeCurrentPlayer() {
        int newId = currentPlayer.GetId()+1;

        if (newId >= numPlayers)
            newId = 0;

        SetCurrentPlayer(players[newId]);
    }

    // Set colors array
    void SetColors() {
        playerColors = new Color[6];

        playerColors[0] = player1Color;
        playerColors[1] = player2Color;
        playerColors[2] = player3Color;
        playerColors[3] = player4Color;
        playerColors[4] = player5Color;
        playerColors[5] = player6Color;
        // TODO: add more colors
    }

    // Set current player
    void SetCurrentPlayer(Player player) {
        currentPlayer = player;

        gameUI.SetPlayerNameText(currentPlayer.GetPlayerName());
        gameUI.SetPlayerColor(currentPlayer.GetColor());
        gameUI.SetPlayerScore(currentPlayer.GetScore());
    }

    // Give current player points
    public void GiveCurrentPlayerPoints(int points) {
        currentPlayer.AddPoints(points);
        gameUI.SetPlayerScore(currentPlayer.GetScore());
    }

    // Display finish turn button and hide dice button
    public void FinishTurn() {
        gameUI.DisplayFinishTurn();
        dice.AllowCoroutine();
    }

    // FinishQuestion is called after player answers question
    public void FinishQuestion(bool receivePoints) {
        if (receivePoints) {
            currentPlayer.ReceivePointsFromTile();
            gameUI.SetPlayerScore(currentPlayer.GetScore());
        }

        FinishTurn();
    }

    // Add card to tile
    public void AddCard(CardData card) {
        ((CardTile)boardController.GetTile(card.tileId)).AddCard(card);
    }

    // Add question to tile
    public void AddQuestion(QuestionData question) {
        ((GroupPropertyTile)boardController.GetTile(question.tileId)).AddQuestion(question);
    }

    // Show question menu
    public void PrepareQuestion(QuestionData question) {
        questionController.LoadQuestion(question);
        questionController.ShowQuestionMenu();
        questionController.GetAnswerFromServer();
    }

    // Show card menu
    public void PrepareCard(CardData card) {
        cardController.LoadCard(card);
        cardController.ShowCardMenu();
    }

    // Show dice
    public void ShowDice() {
        dice.ShowDice();
    }

    // Hide dice
    public void HideDice() {
        dice.HideDice();
    }
}
