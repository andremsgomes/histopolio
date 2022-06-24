using WebSocketSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class WebSocketClientController : MonoBehaviour
{
    private WebSocket ws;
    private GameController gameController;
    private Queue<string> messages = new Queue<string>();
    private string lastMessage = "";

    [SerializeField] private string wsURL;

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket(wsURL);

        ws.OnMessage += (sender, e) =>
        {
            messages.Enqueue(e.Data);
        };

        ConnectWebSocket();
    }

    // Connect and send id message
    void ConnectWebSocket()
    {
        ws.Connect();

        if (gameController.GetAdminId().Length > 0)
        {
            SendId();
        }
    }

    // Send identification
    void SendId()
    {
        IdentificationData identificationData = new IdentificationData();
        identificationData.adminId = gameController.GetAdminId();

        string id = JsonUtility.ToJson(identificationData);
        ws.Send(id);
    }

    // Update is called once per frame
    void Update()
    {
        if (ws.ReadyState == WebSocketState.Closed)
            ConnectWebSocket();

        while (messages.Count > 0)
        {
            ProcessMessage(messages.Dequeue());
        }
    }

    // Process message received
    void ProcessMessage(string message)
    {
        if (message == "ping")
        {
            if (ws.ReadyState == WebSocketState.Closed)
                ConnectWebSocket();
            else
                ws.Send("unityPong");

            return;
        }

        Debug.Log(message);

        JObject dataReceived = JObject.Parse(message);
        string command = (string)dataReceived["type"];

        switch (command)
        {
            case "auth":
                OnAuthReceived(dataReceived);
                break;
            case "answer":
                OnAnswerReceived(dataReceived);
                break;
            case "board":
                OnBoardReceived(dataReceived);
                break;
            case "questions":
                OnQuestionsReceived(dataReceived);
                break;
            case "cards":
                OnCardsReceived(dataReceived);
                break;
            case "badges":
                OnBadgesReceived(dataReceived);
                break;
            case "join game":
                OnJoinGameReceived(dataReceived);
                break;
            case "dice result":
                OnDiceResultReceived(dataReceived);
                break;
            case "save files":
                OnSaveFilesReceived(dataReceived);
                break;
            case "remove player":
                OnRemovePlayerReceived(dataReceived);
                break;
            case "session":
                OnSessionReceived(dataReceived);
                break;
            case "content viewed":
                OnContentViewedReceived();
                break;
            case "new badge":
                OnNewBadgeReceived(dataReceived);
                break;
            case "continue":
                OnContinueReceived();
                break;
            case "next player":
                OnNextPlayerReceived();
                break;
            default:
                Debug.LogError("Unknown message: " + message);
                break;
        }

        message = null;
    }

    // OnAuthReceived is called when the user successfully logins
    void OnAuthReceived(JObject dataReceived)
    {
        gameController.SetAdminId((string)dataReceived["adminId"]);
        SendId();

        List<string> boards = new List<string>();

        foreach (string board in dataReceived["boards"])
        {
            boards.Add(board);
        }

        gameController.ShowBoardsMenu(boards);
    }

    // OnAnswerReceived is called when an answer is received
    void OnAnswerReceived(JObject dataReceived)
    {
        SendTurnData();
        gameController.CheckAnswerFromServer((int)dataReceived["answer"]);
    }

    // Send message to the server
    public void SendMessage(string message)
    {
        if (ws.ReadyState == WebSocketState.Closed)
            ConnectWebSocket();

        ws.Send(message);
        lastMessage = message;
    }

    // Set game controller
    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
    }

    // Send board requests to server
    public void SendBoardRequest(string type, string board)
    {
        BoardSendData boardSendData = new BoardSendData();
        boardSendData.type = type;
        boardSendData.adminId = gameController.GetAdminId();
        boardSendData.board = board;

        string boardSendDataString = JsonUtility.ToJson(boardSendData);

        SendMessage(boardSendDataString);
    }

    // Request board data from server
    public void RequestBoardData(string board)
    {
        SendBoardRequest("load board", board);
    }

    // OnBoardReceived is callend when the board data is received
    void OnBoardReceived(JObject dataReceived)
    {
        List<TileData> tiles = new List<TileData>();

        foreach (JObject tile in dataReceived["board"].ToObject<JArray>())
        {
            TileData tileData = JsonUtility.FromJson<TileData>(Newtonsoft.Json.JsonConvert.SerializeObject(tile));
            tiles.Add(tileData);
        }

        gameController.LoadBoardReceived(tiles);

        RequestQuestionsData(gameController.GetBoard());
    }

    // Request questions data from server
    void RequestQuestionsData(string board)
    {
        SendBoardRequest("load questions", board);
    }

    // OnQuestionsReceived is called when the questions data is received
    void OnQuestionsReceived(JObject dataReceived)
    {
        List<QuestionData> questions = new List<QuestionData>();

        foreach (JObject question in dataReceived["questions"].ToObject<JArray>())
        {
            QuestionData questionData = JsonUtility.FromJson<QuestionData>(Newtonsoft.Json.JsonConvert.SerializeObject(question));
            questions.Add(questionData);
        }

        gameController.LoadQuestionsReceived(questions);

        RequestCardsData(gameController.GetBoard());
    }

    // Request cards data from server
    void RequestCardsData(string board)
    {
        SendBoardRequest("load cards", board);
    }

    // OnCardsReceived is called when the cards data is received
    void OnCardsReceived(JObject dataReceived)
    {
        List<CardData> cards = new List<CardData>();

        foreach (JObject card in dataReceived["cards"].ToObject<JArray>())
        {
            CardData cardData = JsonUtility.FromJson<CardData>(Newtonsoft.Json.JsonConvert.SerializeObject(card));
            cards.Add(cardData);
        }

        gameController.LoadCardsReceived(cards);

        RequestBadgesData(gameController.GetBoard());
    }

    // Request badges data from server
    void RequestBadgesData(string board)
    {
        SendBoardRequest("load badges", board);
    }

    // OnBadgesReceived is called when the badges are received
    void OnBadgesReceived(JObject dataReceived)
    {
        gameController.LoadBadges(dataReceived["badges"].ToObject<JArray>());
    }

    // OnSessionReceived is called when the players data is received
    void OnSessionReceived(JObject dataReceived)
    {
        gameController.SetSessionCode((int)dataReceived["sessionCode"]);
        gameController.LoadLeaderboard(dataReceived["players"].ToObject<JArray>());
    }

    // OnJoinGameReceived is called when a request to join the game is received
    void OnJoinGameReceived(JObject dataReceived)
    {
        List<string> badges = new List<string>();

        foreach (string badge in dataReceived["badges"])
        {
            badges.Add(badge);
        }

        gameController.AddPlayer((string)dataReceived["userId"], (string)dataReceived["name"], (int)dataReceived["points"], (int)dataReceived["position"], (int)dataReceived["numTurns"], (int)dataReceived["totalAnswers"], (int)dataReceived["correctAnswers"], (string)dataReceived["avatar"], badges, (int)dataReceived["multiplier"], (bool)dataReceived["finishedBoard"]);
    }

    // OnDiceResultReceived is called when the dice result is received
    void OnDiceResultReceived(JObject dataReceived)
    {
        SendTurnData();
        gameController.MovePlayer((int)dataReceived["result"]);
    }

    // OnSaveFilesReceived is called when the save files are received
    void OnSaveFilesReceived(JObject dataReceived)
    {
        List<string> saves = new List<string>();

        foreach (JObject save in dataReceived["files"].ToObject<JArray>())
        {
            saves.Add((string)save["name"]);
        }

        gameController.ShowSaveFiles(saves);
    }

    // OnRemovePlayerReceived is called when a player leaves
    void OnRemovePlayerReceived(JObject dataReceived)
    {
        gameController.SetInactivePlayer((string)dataReceived["userId"]);
    }

    // Resend last message sent
    public void ResendLastMessage()
    {
        if (lastMessage.Length > 0) SendMessage(lastMessage);
    }

    // OnContentViewedReceived is called when a player views the content sent
    void OnContentViewedReceived()
    {
        SendTurnData();
        gameController.ContinueTrainCard();
    }

    // OnNewBadgeReceived is called when a player buys a badge
    void OnNewBadgeReceived(JObject dataReceived)
    {
        gameController.AddBadgeToPlayer((string)dataReceived["userId"], (string)dataReceived["badgeId"], (int)dataReceived["points"], (int)dataReceived["multiplier"]);
    }

    // OnContinueReceived is called after player clicks the continue button when a card is showing
    void OnContinueReceived()
    {
        SendTurnData();
        gameController.Continue();
    }

    // OnNextPlayerReceived is called after a player clicks the continue button to finish his turn
    void OnNextPlayerReceived()
    {
        lastMessage = "";
        gameController.ChangeCurrentPlayer();
    }

    // Send turn data is called after a player message is received
    void SendTurnData()
    {
        PlayerTurnData playerTurnData = new PlayerTurnData();
        playerTurnData.type = "turn";
        playerTurnData.userId = gameController.GetCurrentPlayer().GetId();

        string message = JsonUtility.ToJson(playerTurnData);
        SendMessage(message);
    }
}
