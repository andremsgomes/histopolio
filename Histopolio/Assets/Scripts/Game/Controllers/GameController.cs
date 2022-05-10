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
    // [SerializeField] private DiceController dice;
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

        // dice.SetGameController(this);
        // dice.SetDiceComponents();

        mainMenuController.SetGameController(this);
        mainMenuController.SetMainMenuComponents();

        SetColors();
    }

    // Spawn players on GO Tile
    void SpawnPlayers()
    {
        foreach (Player player in players)
        {
            Tile tile = boardController.GetTile(player.GetPosition());

            player.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, -3);
            player.SetTile(tile);
        }

        SetCurrentPlayer(players[0]);
    }

    // Change camera
    public void ChangeCamera()
    {
        cameraController.ToggleCamera();
    }

    // Get player position
    public Vector3 GetPlayerPosition()
    {
        return currentPlayer.transform.position;
    }

    // Get current tile
    public Tile GetCurrentTile()
    {
        return currentPlayer.GetTile();
    }

    // Get next tile
    public Tile GetNextTile()
    {
        int currenTileId = GetCurrentTile().GetId();

        return GetTile(currenTileId + 1);
    }

    // Move player after rolled dice
    public void MovePlayer(int diceResult)
    {
        currentPlayer.Move(diceResult);
    }

    // Get tile with tile id
    public Tile GetTile(int tileId)
    {
        if (tileId >= 40)
            tileId = tileId - 40;

        return boardController.GetTile(tileId);
    }

    // Save current player's position and points
    public void SaveCurrentPlayer()
    {
        // TODO: criar documento só de players => enviar com um POST request para evitar conflitos no documento de players OU ter um documento de players por board
        SavePlayerData savePlayerData = new SavePlayerData();
        savePlayerData.board = "Histopolio";
        savePlayerData.userId = currentPlayer.GetId();
        savePlayerData.points = currentPlayer.GetScore();
        savePlayerData.position = currentPlayer.GetTile().GetId();
        string message = JsonUtility.ToJson(savePlayerData);

        SendMessageToServer(message);
    }

    // Change current player
    public void ChangeCurrentPlayer()
    {
        int newPlayOrder = currentPlayer.GetPlayOrder() + 1;

        if (newPlayOrder >= players.Count)
            newPlayOrder = 0;

        SetCurrentPlayer(players[newPlayOrder]);
    }

    // Set colors array
    void SetColors()
    {
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
    void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;

        gameUI.SetPlayerNameText(currentPlayer.GetPlayerName());
        gameUI.SetPlayerColor(currentPlayer.GetColor());
        gameUI.SetPlayerScore(currentPlayer.GetScore());

        PlayerTurnData playerTurnData = new PlayerTurnData();
        playerTurnData.userId = currentPlayer.GetId();
        string message = JsonUtility.ToJson(playerTurnData);

        SendMessageToServer(message);
    }

    // Give current player points
    public void GiveCurrentPlayerPoints(int points)
    {
        currentPlayer.AddPoints(points);
        gameUI.SetPlayerScore(currentPlayer.GetScore());
    }

    // Display finish turn button and hide dice button
    public void FinishTurn()
    {
        gameUI.DisplayFinishTurn();
        // dice.AllowCoroutine();
    }

    // FinishQuestion is called after player answers question
    public void FinishQuestion(bool receivePoints)
    {
        if (receivePoints)
        {
            currentPlayer.ReceivePointsFromTile();
            gameUI.SetPlayerScore(currentPlayer.GetScore());
        }

        FinishTurn();
    }

    // Add card to tile
    public void AddCard(CardData card)
    {
        ((CardTile)boardController.GetTile(card.tileId)).AddCard(card);
    }

    // Add question to tile
    public void AddQuestion(QuestionData question)
    {
        ((GroupPropertyTile)boardController.GetTile(question.tileId)).AddQuestion(question);
    }

    // Load question to send to server
    public void PrepareQuestion(QuestionData question)
    {
        questionController.LoadQuestion(question);
        // questionController.ShowQuestionMenu();
    }

    // Show card menu
    public void PrepareCard(CardData card)
    {
        cardController.LoadCard(card);
        cardController.ShowCardMenu();
    }

    // // Show dice
    // public void ShowDice() {
    //     dice.ShowDice();
    // }

    // // Hide dice
    // public void HideDice() {
    //     dice.HideDice();
    // }

    // Send message to the server
    void SendMessageToServer(string message)
    {
        webSocketClientController.SendMessage(message);
    }

    // Send question send data to server
    public void SendQuestionToServer(QuestionData questionData)
    {
        QuestionSendData questionSendData = new QuestionSendData();
        questionSendData.userId = currentPlayer.GetId();
        questionSendData.questionData = questionData;

        string message = JsonUtility.ToJson(questionSendData);

        SendMessageToServer(message);
    }

    // Check answer received from server
    public void CheckAnswerFromServer(int answer)
    {
        questionController.CheckAnswer(answer);
    }

    // Request board data from server
    public void RequestBoardData()
    {
        webSocketClientController.RequestBoardData("Histopolio");
    }

    // Load board received from server
    public void LoadBoardReceived(BoardData boardData)
    {
        boardController.LoadBoard(boardData);
    }

    // Load questions received from server
    public void LoadQuestionsReceived(QuestionsData questionsData)
    {
        questionController.LoadQuestions(questionsData);
    }

    // Load cards received from server
    public void LoadCardsReceived(CardsData cardsData)
    {
        cardController.LoadCards(cardsData);
    }

    // Start new game
    public void StartGame()
    {
        SpawnPlayers();
        gameUI.ShowHUD();

        Debug.Log("Game Started");
    }

    // Check if game is loaded
    public bool GetGameLoaded()
    {
        return gameLoaded;
    }

    // Set game loaded
    public void SetGameLoaded(bool gameLoaded)
    {
        this.gameLoaded = gameLoaded;
    }

    // Add player to the game
    public void AddPlayer(int id, string name, int points, int position)
    {
        Player newPlayer = Instantiate(playerPrefab, new Vector3(0, 0, -3), Quaternion.identity);

        newPlayer.name = name;
        newPlayer.SetGameController(this);
        newPlayer.SetId(id);
        newPlayer.SetPlayOrder(players.Count);
        newPlayer.SetColor(playerColors[players.Count]);
        newPlayer.SetName(name);
        newPlayer.SetScore(points);
        newPlayer.SetPosition(position);

        mainMenuController.ShowNewPlayer(players.Count, name, playerColors[players.Count]);

        players.Add(newPlayer);
    }

    // Send info shown message
    public void SendInfoShownMessageToServer()
    {
        InfoShownData infoShownData = new InfoShownData();
        infoShownData.userId = currentPlayer.GetId();
        string message = JsonUtility.ToJson(infoShownData);

        SendMessageToServer(message);
    }

    // Send new game message to server
    public void SendNewGameMessage() {
        webSocketClientController.SendBoardRequest("new game", "Histopolio");
    }

    // Send load game message to server
    public void SendLoadGameMessage() {
        webSocketClientController.SendBoardRequest("load game", "Histopolio");
    }
}
