using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    private Color[] playerColors;
    private BoardController boardController;
    private CameraController cameraController;
    private WebSocketClientController webSocketClientController;
    private GameUI gameUI;
    private Player currentPlayer;
    private bool gameLoaded = false;

    [Header("Controllers")]
    [SerializeField] private QuestionController questionController;
    [SerializeField] private CardController cardController;
    [SerializeField] private DiceController dice;
    [SerializeField] private MainMenuController mainMenuController;

    [Header("Prefabs")]
    [SerializeField] private Player playerPrefab;

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
        webSocketClientController = this.GetComponent<WebSocketClientController>();
        gameUI = this.GetComponent<GameUI>();

        boardController.SetGameController(this);

        questionController.SetGameController(this);
        questionController.SetQuestionComponents();

        cardController.SetGameController(this);
        cardController.SetCardComponents();

        cameraController.SetGameController(this);
        cameraController.SetBoardCamera();

        webSocketClientController.SetGameController(this);

        gameUI.SetGameController(this);

        dice.SetGameController(this);
        dice.SetDiceComponents();

        mainMenuController.SetGameController(this);
        mainMenuController.SetMainMenuComponents();

        SetColors();
    }
    
    // Spawn players on GO Tile
    void SpawnPlayers() {
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

        if (newId >= players.Count)
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

    // Load question to send to server
    public void PrepareQuestion(QuestionData question) {
        questionController.LoadQuestion(question);
        // questionController.ShowQuestionMenu();
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

    // Send message to the server
    public void SendMessageToServer(string message) {
        webSocketClientController.SendMessage(message);
    }

    // Check answer received from server
    public void CheckAnswerFromServer(int answer) {
        questionController.CheckAnswer(answer);
    }

    // Request board data from server
    public void RequestBoardData() {
        webSocketClientController.RequestBoardData("Histopolio");
    }

    // Load board received from server
    public void LoadBoardReceived(BoardData boardData) {
        boardController.LoadBoard(boardData);
    }

    // Load questions received from server
    public void LoadQuestionsReceived(QuestionsData questionsData) {
        questionController.LoadQuestions(questionsData);
    }

    // Load cards received from server
    public void LoadCardsReceived(CardsData cardsData) {
        cardController.LoadCards(cardsData);
    }

    // Start new game
    public void StartNewGame() {
        SpawnPlayers();
        gameUI.ShowHUD();

        Debug.Log("New Game Started");
    }

    // Check if game is loaded
    public bool GetGameLoaded() {
        return gameLoaded;
    }

    // Set game loaded
    public void SetGameLoaded(bool gameLoaded) {
        this.gameLoaded = gameLoaded;
    }

    public void AddPlayer(int id, string name) {
        Tile firstTile = boardController.GetTile(0);

        Player newPlayer = Instantiate(playerPrefab, new Vector3(firstTile.transform.position.x, firstTile.transform.position.y, -3), Quaternion.identity);

        newPlayer.name = name;
        newPlayer.SetGameController(this);
        newPlayer.SetTile(firstTile);
        newPlayer.SetId(id);
        newPlayer.SetColor(playerColors[players.Count]);
        newPlayer.SetName(name);    // TODO: nome será introduzido pelo utilizador
        newPlayer.SetScore(20);

        mainMenuController.ShowNewPlayer(players.Count, name, playerColors[players.Count]);

        players.Add(newPlayer);
    }
}
