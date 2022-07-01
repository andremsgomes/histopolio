using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Linq;

public class GameController : MonoBehaviour
{
    private Dictionary<string, Player> players = new Dictionary<string, Player>();
    private Dictionary<string, int> playerTurns = new Dictionary<string, int>();
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();
    private Dictionary<string, string> playerNames = new Dictionary<string, string>();
    private Dictionary<string, Sprite> playerSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> badgeSprites = new Dictionary<string, Sprite>();
    private HashSet<string> activePlayers = new HashSet<string>();
    private BoardController boardController;
    private CameraController cameraController;
    private WebSocketClientController webSocketClientController;
    private GameUI gameUI;
    private Player currentPlayer;
    private bool gameLoaded = false;
    private bool gameStarted = false;
    private string board = "";
    private string saveFile = "";
    private string adminId = "";
    int sessionCode = 1000;

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
            Tile tile = boardController.GetTileFromPosition(player.GetPosition());

            tile.AddPlayer(player);
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

    // Move player after rolled dice or drawn card
    public void MovePlayer(int diceResult)
    {
        currentPlayer.Move(diceResult);
    }

    // Move player to tile
    public void MovePlayerTo(int tile)
    {
        int spaces = tile - currentPlayer.GetTile().GetId();
        MovePlayer(spaces);
    }

    // Get tile with tile id
    public Tile GetTile(int tileId)
    {
        if (tileId >= 40)
            tileId = tileId - 40;

        return boardController.GetTileFromPosition(tileId);
    }

    // Save current player's position and points
    void SaveCurrentPlayer()
    {
        SavePlayerData savePlayerData = new SavePlayerData();
        savePlayerData.adminId = adminId;
        savePlayerData.userId = currentPlayer.GetId();
        savePlayerData.points = currentPlayer.GetScore();
        savePlayerData.position = currentPlayer.GetTile().GetId();
        savePlayerData.numTurns = currentPlayer.GetNumTurns() + 1;
        savePlayerData.totalAnswers = currentPlayer.GetTotalAnswers();
        savePlayerData.correctAnswers = currentPlayer.GetCorrectAnswers();
        savePlayerData.finishedBoard = currentPlayer.GetFinishedBoard();
        string message = JsonUtility.ToJson(savePlayerData);

        SendMessageToServer(message);
    }

    // Change current player
    public void ChangeCurrentPlayer()
    {
        currentPlayer.AddTurn();
        playerTurns[currentPlayer.GetId()] = currentPlayer.GetNumTurns();

        currentPlayer.SetGlow(false);
        SetCurrentPlayer(players[playerTurns.OrderBy(kvp => kvp.Value).First().Key]);
    }

    // Set tile text on ui
    public void SetTileText(int position, string tile)
    {
        gameUI.SetTileText(position, tile);
    }

    // Set current player
    void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;
        currentPlayer.SetGlow(true);

        gameUI.SetPlayerNameText(currentPlayer.GetPlayerName());
        gameUI.SetAvatar(currentPlayer.GetAvatar());
        gameUI.SetBadges(currentPlayer.GetBadges());

        if (activePlayers.Contains(currentPlayer.GetId()))
            gameUI.HideInactivePlayer();
        else
            gameUI.ShowInactivePlayer(players.Count > 1);

        gameUI.SetTileText(currentPlayer.GetTile().GetId(), currentPlayer.GetTile().GetTileName());

        if (!currentPlayer.GetFinishedBoard())
        {
            // play dice
            PlayerTurnData playerTurnData = new PlayerTurnData();
            playerTurnData.type = "dice";
            playerTurnData.userId = currentPlayer.GetId();
            string message = JsonUtility.ToJson(playerTurnData);

            SendMessageToServer(message);
        }
        else
        {
            // ask random question once finished board
            questionController.LoadRandomQuestion();
        }
    }

    // Give current player points
    public void GiveCurrentPlayerPoints(int points)
    {
        currentPlayer.AddPoints(points * currentPlayer.GetMultiplier());
        playerScores[currentPlayer.GetId()] = currentPlayer.GetScore();

        UpdateLeaderboard();
    }

    // Display finish turn button and hide dice button
    public void FinishTurn(string info = "", string bodyColor = null)
    {
        SaveCurrentPlayer();
        SendFinishTurn(info, bodyColor);

        // gameUI.DisplayFinishTurn();
        // dice.AllowCoroutine();
    }

    void SendFinishTurn(string info, string bodyColor = null)
    {
        FinishTurnData finishTurnData = new FinishTurnData();
        finishTurnData.userId = currentPlayer.GetId();
        finishTurnData.info = info;
        finishTurnData.bodyColor = bodyColor;
        string message = JsonUtility.ToJson(finishTurnData);

        SendMessageToServer(message);
    }

    // FinishQuestion is called after player answers question
    public void FinishQuestion(bool correctAnswer)
    {
        currentPlayer.AddAnswer();

        if (correctAnswer)
            currentPlayer.AddCorrectAnswer();

        string info = "";
        string bodyColor = null;

        if (currentPlayer.GetFinishedBoard() && correctAnswer)
        {
            GiveCurrentPlayerPoints(20);

            info = "Resposta certa! Recebeste " + 20 * currentPlayer.GetMultiplier() + " pontos!";
            bodyColor = "#00c800";
        }
        else if (!currentPlayer.GetFinishedBoard() && ((((QuestionTile)currentPlayer.GetTile()).GetPoints() >= 0 && correctAnswer) || (((QuestionTile)currentPlayer.GetTile()).GetPoints() < 0 && !correctAnswer)))
        {
            currentPlayer.ReceivePointsFromTile();
            playerScores[currentPlayer.GetId()] = currentPlayer.GetScore();

            UpdateLeaderboard();

            if (((QuestionTile)currentPlayer.GetTile()).GetPoints() > 0)
            {
                info = "Resposta certa! Recebeste " + ((QuestionTile)currentPlayer.GetTile()).GetPoints() * currentPlayer.GetMultiplier() + " pontos!";
                bodyColor = "#00c800";
            }
            else if (((QuestionTile)currentPlayer.GetTile()).GetPoints() < 0)
            {
                info = "Resposta errada! Perdeste " + ((QuestionTile)currentPlayer.GetTile()).GetPoints() * (-1) + " pontos!";
                bodyColor = "#dc0000";
            }
            else
            {
                info = "Resposta certa! Evistaste recuar para a " + boardController.GetTileFromPosition(10).GetTileName() + "!";
                bodyColor = "#00c800";
            }
        }
        else if (!currentPlayer.GetFinishedBoard())
        {
            if (((QuestionTile)currentPlayer.GetTile()).GetPoints() > 0)
            {
                info = "Resposta errada! Não conseguiste receber pontos desta vez!";
                bodyColor = "#dc0000";
            }
            else if (((QuestionTile)currentPlayer.GetTile()).GetPoints() < 0)
            {
                info = "Resposta certa! Evitaste perder pontos!";
                bodyColor = "#00c800";
            }
            else
            {
                info = "Resposta errada! Terás que recuar para a " + boardController.GetTileFromPosition(10).GetTileName() + "!";
                bodyColor = "#dc0000";
            }
        }
        else
        {
            info = "Resposta errada! Não conseguiste receber pontos desta vez!";
            bodyColor = "#dc0000";
        }

        if (!correctAnswer && currentPlayer.GetTile().GetId() == 30)
        {
            // If answer wrong on go to prison tile send info
            SendInfoShownMessageToServer(info, bodyColor);
        }
        else
        {
            FinishTurn(info, bodyColor);
        }
    }

    // Add card to tile
    public void AddCard(CardData card)
    {
        ((StationTile)boardController.GetTile(card.tileId)).AddCard(card);
    }

    // Add question to tile
    public void AddQuestion(QuestionData question)
    {
        ((QuestionTile)boardController.GetTile(question.tileId)).AddQuestion(question);

        int tilePosition = boardController.GetTile(question.tileId).GetId();

        if (tilePosition < 10)
        {
            // Add to prison tile
            ((QuestionTile)boardController.GetTileFromPosition(10)).AddQuestion(question);
        }
        else if (tilePosition < 20)
        {
            // Add to parking tile
            ((QuestionTile)boardController.GetTileFromPosition(20)).AddQuestion(question);
        }

        if (tilePosition == 4 || tilePosition == 12 || tilePosition == 28)
        {
            // Add to go to prison tile
            ((QuestionTile)boardController.GetTileFromPosition(30)).AddQuestion(question);
        }
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
        ContentData contentData = new ContentData();
        contentData.userId = currentPlayer.GetId();
        contentData.content = card.content;
        string message = JsonUtility.ToJson(contentData);

        SendMessageToServer(message);

        cardController.LoadCard(card, currentPlayer.GetTile().GetTileName());
        cardController.ShowCardMenu(false);
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

        int points = 20;
        if (currentPlayer.GetTile().GetId() != 0)
        {
            points = ((QuestionTile)currentPlayer.GetTile()).GetPoints();
        }

        questionSendData.tile = "Casa " + currentPlayer.GetTile().GetId() + " - " + currentPlayer.GetTile().GetTileName() + " (" + points + " pontos)";
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
    public void RequestBoardData(string board)
    {
        this.board = board;
        webSocketClientController.RequestBoardData(board);
    }

    // Load board received from server
    public void LoadBoardReceived(List<TileData> tiles)
    {
        boardController.LoadBoard(tiles);
    }

    // Load questions received from server
    public void LoadQuestionsReceived(List<QuestionData> questions)
    {
        questionController.LoadQuestions(questions);
    }

    // Load cards received from server
    public void LoadCardsReceived(List<CardData> cards)
    {
        cardController.LoadCards(cards);
    }

    // Load badges received from server
    public void LoadBadges(JArray badges)
    {
        IEnumerator coroutine = LoadBadgeSprites(badges);
        StartCoroutine(coroutine);
    }

    IEnumerator LoadBadgeSprites(JArray badges)
    {
        foreach (JObject badge in badges)
        {
            // Load image
            WWW www = new WWW((string)badge["image"]);
            yield return www;

            Sprite sprite = CreateSquareSprite(www);
            badgeSprites[(string)badge["_id"]] = sprite;
        }
    }

    // Start new game
    public void StartGame()
    {
        gameStarted = true;
        SpawnPlayers();
        UpdateLeaderboard();
        gameUI.ShowHUD(sessionCode);

        Debug.Log("Game Started");
    }

    // Check if game is loaded
    public bool GetGameLoaded()
    {
        return gameLoaded;
    }

    // Add player to the game
    public void AddPlayer(string id, string name, int points, int position, int numTurns, int totalAnswers, int correctAnswers, string avatarURL, List<string> badges, int multiplier, bool finishedBoard)
    {
        if (currentPlayer != null && id == currentPlayer.GetId())
        {
            webSocketClientController.ResendLastMessage();
            gameUI.HideInactivePlayer();
        }

        if (!players.ContainsKey(id))
        {
            Player newPlayer = Instantiate(playerPrefab, new Vector3(0, 0, -3), Quaternion.identity);

            newPlayer.name = name;
            newPlayer.SetGameController(this);
            newPlayer.SetId(id);
            newPlayer.SetName(name);
            newPlayer.SetScore(points);
            newPlayer.SetPosition(position);
            newPlayer.SetNumTurns(numTurns);
            newPlayer.SetTotalAnswers(totalAnswers);
            newPlayer.SetCorrectAnswers(correctAnswers);

            List<Sprite> playerBadgeSprites = new List<Sprite>();
            foreach (string badge in badges)
            {
                playerBadgeSprites.Add(badgeSprites[badge]);
            }

            newPlayer.SetBadges(playerBadgeSprites);
            newPlayer.SetMultiplier(multiplier);
            newPlayer.SetFinishedBoard(finishedBoard);

            IEnumerator coroutine = LoadAvatar(avatarURL, newPlayer);
            StartCoroutine(coroutine);

            players[id] = newPlayer;
            playerTurns[id] = newPlayer.GetNumTurns();
            playerScores[id] = newPlayer.GetScore();
            playerNames[id] = newPlayer.GetPlayerName();

            if (gameStarted)
            {
                Tile tile = boardController.GetTileFromPosition(newPlayer.GetPosition());

                tile.AddPlayer(newPlayer);
                newPlayer.SetTile(tile);

                if (!activePlayers.Contains(currentPlayer.GetId())) gameUI.ShowInactivePlayer(true);
            }
        }

        activePlayers.Add(id);
    }

    // Remove player from the game
    public void SetInactivePlayer(string id)
    {
        if (currentPlayer != null && id == currentPlayer.GetId())
        {
            gameUI.ShowInactivePlayer(players.Count > 1);
        }

        activePlayers.Remove(id);
    }

    // Remove current player
    public void RemoveCurrentPlayer()
    {
        currentPlayer.GetTile().RemovePlayer(currentPlayer);
        players.Remove(currentPlayer.GetId());
        playerTurns.Remove(currentPlayer.GetId());
        Destroy(currentPlayer.gameObject);

        SetCurrentPlayer(players[playerTurns.OrderBy(kvp => kvp.Value).First().Key]);
    }

    // Send info shown message
    public void SendInfoShownMessageToServer(string info = "", string bodyColor = null)
    {
        InfoShownData infoShownData = new InfoShownData();
        infoShownData.info = info;
        infoShownData.bodyColor = bodyColor;
        infoShownData.userId = currentPlayer.GetId();
        string message = JsonUtility.ToJson(infoShownData);

        SendMessageToServer(message);
    }

    // Send new game message to server
    public void SendNewGameMessage()
    {
        webSocketClientController.SendBoardRequest("new game", board);
    }

    // Send load save files message to server
    public void SendLoadSavesMessage()
    {
        webSocketClientController.SendBoardRequest("load saves", board);
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
        loadFileData.adminId = adminId;
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

        Sprite sprite = CreateSquareSprite(www);

        playerSprites[player.GetId()] = sprite;
        player.SetAvatar(sprite);
        mainMenuController.ShowNewPlayer(sprite);
    }

    // Load leadeboard
    public void LoadLeaderboard(JArray players)
    {
        foreach (JObject player in players)
        {
            playerScores[(string)player["userId"]] = (int)player["points"];
            playerNames[(string)player["userId"]] = (string)player["name"];
        }

        IEnumerator coroutine = LoadAvatars(players);
        StartCoroutine(coroutine);
    }

    // Load avatars for leaderboard
    IEnumerator LoadAvatars(JArray players)
    {
        List<KeyValuePair<string, int>> sortedScores = playerScores.OrderByDescending(kvp => kvp.Value).ToList();

        int leaderboardLength = 3;
        if (sortedScores.Count < leaderboardLength) leaderboardLength = sortedScores.Count;

        foreach (JObject player in players)
        {
            for (int i = 0; i < leaderboardLength; i++)
            {
                if ((string)player["userId"] == sortedScores[i].Key)
                {
                    // Load avatar
                    WWW www = new WWW((string)player["avatar"]);
                    yield return www;

                    Sprite sprite = CreateSquareSprite(www);
                    playerSprites[(string)player["userId"]] = sprite;

                    break;
                }
            }
        }

        gameLoaded = true;

        ReadyData readyData = new ReadyData();
        readyData.adminId = adminId;
        string message = JsonUtility.ToJson(readyData);

        SendMessageToServer(message);
    }

    // Create a square sprite
    Sprite CreateSquareSprite(WWW www)
    {
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

        return Sprite.Create(www.texture, new Rect(startWidth, startHeight, endWidth, endHeight), new Vector2(0, 0));
    }

    // Update leaderboard
    void UpdateLeaderboard()
    {
        List<KeyValuePair<string, int>> sortedScores = playerScores.OrderByDescending(kvp => kvp.Value).ToList();

        int leaderboardLength = 3;
        if (sortedScores.Count < leaderboardLength) leaderboardLength = sortedScores.Count;

        for (int i = 0; i < leaderboardLength; i++)
        {
            gameUI.UpdateLeaderboard(i, playerSprites[sortedScores[i].Key], playerNames[sortedScores[i].Key], sortedScores[i].Value);
        }
    }

    // Show random community card
    public void ShowCommunityCard()
    {
        cardController.ShowCommunityCard(currentPlayer.GetTile().GetTileName());
    }

    // Show random chance card
    public void ShowChanceCard()
    {
        cardController.ShowChanceCard(currentPlayer.GetTile().GetTileName());
    }

    // Hide card menu and give points
    public void ContinueTrainCard()
    {
        currentPlayer.ReceivePointsFromTile();
        playerScores[currentPlayer.GetId()] = currentPlayer.GetScore();

        UpdateLeaderboard();

        cardController.HideCardMenu();

        string info = "Parabéns! Recebeste " + ((PointsTile)currentPlayer.GetTile()).GetPoints() * currentPlayer.GetMultiplier() + " pontos!";
        FinishTurn(info);
    }

    // Continue action
    public void Continue()
    {
        // If in go to prison tile
        if (currentPlayer.GetTile().GetId() == 30)
            MovePlayerTo(10);
        else
            cardController.Continue();  // If card is showing
    }

    // Get board
    public string GetBoard()
    {
        return board;
    }

    // Add badge to player
    public void AddBadgeToPlayer(string userId, string badgeId, int points, int multiplier)
    {
        Player player = players[userId];
        Sprite badgeSprite = badgeSprites[badgeId];

        player.AddBadge(badgeSprite);
        player.SetScore(points);
        playerScores[userId] = points;
        player.SetMultiplier(multiplier);

        UpdateLeaderboard();

        if (currentPlayer.GetId() == userId)
        {
            gameUI.SetBadges(currentPlayer.GetBadges());
        }
    }

    // Get current player
    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    // Set the session code with a random number
    public void SetSessionCode(int code)
    {
        sessionCode = code;
        mainMenuController.ShowSessionCode(sessionCode);
    }

    // Get session code
    public int GetSessionCode()
    {
        return sessionCode;
    }

    // Send Login message to server
    public void Login(string email, string password)
    {
        LoginData loginData = new LoginData();
        loginData.email = email;
        loginData.password = password;

        string message = JsonUtility.ToJson(loginData);

        SendMessageToServer(message);
    }

    // Show boards menu
    public void ShowBoardsMenu(List<string> boards)
    {
        mainMenuController.ShowBoardsMenu(boards);
    }

    // Set admin id
    public void SetAdminId(string adminId)
    {
        this.adminId = adminId;
    }

    // Get admin id
    public string GetAdminId()
    {
        return adminId;
    }
}
