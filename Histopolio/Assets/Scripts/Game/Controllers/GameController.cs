using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Linq;

public class GameController : MonoBehaviour
{
    private Dictionary<int, Player> players = new Dictionary<int, Player>();
    private Dictionary<int, int> playerTurns = new Dictionary<int, int>();
    private Dictionary<int, int> playerScores = new Dictionary<int, int>();
    private Dictionary<int, string> playerNames = new Dictionary<int, string>();
    private Dictionary<int, Sprite> playerSprites = new Dictionary<int, Sprite>();
    private BoardController boardController;
    private CameraController cameraController;
    private WebSocketClientController webSocketClientController;
    private GameUI gameUI;
    private Player currentPlayer;
    private bool gameLoaded = false;
    private string board = "Histopolio";
    private string saveFile = "";

    [Header("Controllers")]
    [SerializeField] private QuestionController questionController;
    [SerializeField] private CardController cardController;
    // [SerializeField] private DiceController dice;
    [SerializeField] private MainMenuController mainMenuController;

    [Header("Prefabs")]
    [SerializeField] private Player playerPrefab;


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
    }

    // Spawn players on GO Tile
    void SpawnPlayers()
    {
        foreach (Player player in players.Values)
        {
            Tile tile = boardController.GetTile(player.GetPosition());

            player.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, -3);
            player.SetTile(tile);
        }

        SetCurrentPlayer(players[playerTurns.OrderBy(kvp => kvp.Value).First().Key]);
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
    void SaveCurrentPlayer()
    {
        SavePlayerData savePlayerData = new SavePlayerData();
        savePlayerData.board = "Histopolio";
        savePlayerData.userId = currentPlayer.GetId();
        savePlayerData.points = currentPlayer.GetScore();
        savePlayerData.position = currentPlayer.GetTile().GetId();
        savePlayerData.numTurns = currentPlayer.GetNumTurns() + 1;
        string message = JsonUtility.ToJson(savePlayerData);

        SendMessageToServer(message);
    }

    // Change current player
    public void ChangeCurrentPlayer()
    {
        currentPlayer.AddTurn();
        playerTurns[currentPlayer.GetId()] = currentPlayer.GetNumTurns();

        SetCurrentPlayer(players[playerTurns.OrderBy(kvp => kvp.Value).First().Key]);
    }

    // Set current player
    void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;

        gameUI.SetPlayerNameText(currentPlayer.GetPlayerName());
        gameUI.SetAvatar(currentPlayer.GetAvatar());
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
        playerScores[currentPlayer.GetId()] = currentPlayer.GetScore();

        gameUI.SetPlayerScore(currentPlayer.GetScore());
        UpdateLeaderboard();
    }

    // Display finish turn button and hide dice button
    public void FinishTurn()
    {
        SaveCurrentPlayer();
        gameUI.DisplayFinishTurn();
        // dice.AllowCoroutine();
    }

    // FinishQuestion is called after player answers question
    public void FinishQuestion(bool receivePoints)
    {
        if (receivePoints)
        {
            currentPlayer.ReceivePointsFromTile();
            playerScores[currentPlayer.GetId()] = currentPlayer.GetScore();

            gameUI.SetPlayerScore(currentPlayer.GetScore());
            UpdateLeaderboard();
        }

        FinishTurn();
    }

    // Add card to tile
    public void AddCard(TileCardData card)
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
    public void PrepareCard(TileCardData card)
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
        UpdateLeaderboard();
        gameUI.ShowHUD();

        Debug.Log("Game Started");
    }

    // Check if game is loaded
    public bool GetGameLoaded()
    {
        return gameLoaded;
    }

    // Add player to the game
    public void AddPlayer(int id, string name, int points, int position, int numTurns, string avatarURL)
    {
        if (currentPlayer != null && id == currentPlayer.GetId())
        {
            webSocketClientController.ResendLastMessage();
        }
        else
        {
            Player newPlayer = Instantiate(playerPrefab, new Vector3(0, 0, -3), Quaternion.identity);

            newPlayer.name = name;
            newPlayer.SetGameController(this);
            newPlayer.SetId(id);
            newPlayer.SetName(name);
            newPlayer.SetScore(points);
            newPlayer.SetPosition(position);
            newPlayer.SetNumTurns(numTurns);

            IEnumerator coroutine = LoadAvatar(avatarURL, newPlayer);
            StartCoroutine(coroutine);

            players[id] = newPlayer;
            playerTurns[id] = newPlayer.GetNumTurns();
            playerScores[id] = newPlayer.GetScore();
            playerNames[id] = newPlayer.GetPlayerName();
        }
    }

    // Remove player from the game
    public void RemovePlayer(int id)
    {
        if (currentPlayer.GetId() != id)
        {
            Debug.Log(players[id].GetPlayerName() + " left");
            Destroy(players[id].gameObject);
            playerTurns.Remove(id);
            players.Remove(id);
        }
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
    public void SendNewGameMessage()
    {
        webSocketClientController.SendBoardRequest("new game", "Histopolio");
    }

    // Send load save files message to server
    public void SendLoadSavesMessage()
    {
        webSocketClientController.SendBoardRequest("load saves", "Histopolio");
    }

    // Show save files on menu
    public void ShowSaveFiles(List<string> files)
    {
        mainMenuController.ShowSaveFiles(files);
    }

    // Load data from save file
    public void LoadSaveFile(string fileName)
    {
        saveFile = fileName;

        LoadFileData loadFileData = new LoadFileData();
        loadFileData.board = board;
        loadFileData.file = saveFile;
        string message = JsonUtility.ToJson(loadFileData);

        SendMessageToServer(message);
    }

    // Load image from url and set on player and join menu
    IEnumerator LoadAvatar(string avatar, Player player)
    {
        WWW www = new WWW(avatar);
        yield return www;

        // Square image
        float width = www.texture.width, height = www.texture.height;
        float startWidth = 0, startHeight = 0, endWidth = width, endHeight = height;

        if (width > height)
        {
            startWidth = (width - height) / 2;
            endWidth = width - (width - height) / 2;
        }

        if (height > width)
        {
            startHeight = (height - width) / 2;
            endHeight = height - (height - width) / 2;
        }

        Sprite sprite = Sprite.Create(www.texture, new Rect(startWidth, startHeight, endWidth, endHeight), new Vector2(0, 0));

        playerSprites[player.GetId()] = sprite;
        player.SetAvatar(sprite);
        mainMenuController.ShowNewPlayer(sprite);
    }

    // Load leadeboard
    public void LoadLeaderboard(JArray players) {
        foreach (JObject player in players)
        {
            playerScores[(int)player["userId"]] = (int)player["points"];
            playerNames[(int)player["userId"]] = (string)player["name"];
        }

        IEnumerator coroutine = LoadAvatars(players);
        StartCoroutine(coroutine);
    }

    // Load avatars for leaderboard
    IEnumerator LoadAvatars(JArray players)
    {
        List<KeyValuePair<int, int>> sortedScores = playerScores.OrderByDescending(kvp => kvp.Value).ToList();

        int leaderboardLength = 3;
        if (sortedScores.Count < leaderboardLength) leaderboardLength = sortedScores.Count;

        foreach (JObject player in players)
        {
            for (int i = 0; i < leaderboardLength; i++) {
                if ((int)player["userId"] == sortedScores[i].Key) {
                    // Load avatar
                    WWW www = new WWW((string)player["avatar"]);
                    yield return www;

                    // Square image
                    float width = www.texture.width, height = www.texture.height;
                    float startWidth = 0, startHeight = 0, endWidth = width, endHeight = height;

                    if (width > height)
                    {
                        startWidth = (width - height) / 2;
                        endWidth = width - (width - height) / 2;
                    }

                    if (height > width)
                    {
                        startHeight = (height - width) / 2;
                        endHeight = height - (height - width) / 2;
                    }

                    Sprite sprite = Sprite.Create(www.texture, new Rect(startWidth, startHeight, endWidth, endHeight), new Vector2(0, 0));
                    playerSprites[(int)player["userId"]] = sprite;

                    break;
                }
            }
        }

        gameLoaded = true;

        SimpleData simpleData = new SimpleData();
        simpleData.type = "ready";
        string message = JsonUtility.ToJson(simpleData);

        SendMessageToServer(message);
    }

    // Update leaderboard
    void UpdateLeaderboard() {
        List<KeyValuePair<int, int>> sortedScores = playerScores.OrderByDescending(kvp => kvp.Value).ToList();

        int leaderboardLength = 3;
        if (sortedScores.Count < leaderboardLength) leaderboardLength = sortedScores.Count;

        for (int i = 0; i < leaderboardLength; i++) {
            gameUI.UpdateLeaderboard(i, playerSprites[sortedScores[i].Key], playerNames[sortedScores[i].Key], sortedScores[i].Value);
        }
    }

    // Show random community card
    public void ShowCommunityCard() {
        cardController.ShowCommunityCard();
    }
}
