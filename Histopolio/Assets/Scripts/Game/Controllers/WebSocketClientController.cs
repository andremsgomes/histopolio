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

        string id = JsonUtility.ToJson(new IdentificationData());
        ws.Send(id);
    }

    // Update is called once per frame
    void Update()
    {
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
            case "players":
                OnPlayersReceived(dataReceived);
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

    // OnAnswerReceived is called when an answer is received
    void OnAnswerReceived(JObject dataReceived)
    {
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
        BoardData boardData = JsonUtility.FromJson<BoardData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["board"]));
        gameController.LoadBoardReceived(boardData);

        RequestQuestionsData(boardData.name);
    }

    // Request questions data from server
    void RequestQuestionsData(string board)
    {
        SendBoardRequest("load questions", board);
    }

    // OnQuestionsReceived is called when the questions data is received
    void OnQuestionsReceived(JObject dataReceived)
    {
        QuestionsData questionsData = JsonUtility.FromJson<QuestionsData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["questions"]));
        gameController.LoadQuestionsReceived(questionsData);

        RequestCardsData(questionsData.board);
    }

    // Request cards data from server
    void RequestCardsData(string board)
    {
        SendBoardRequest("load cards", board);
    }

    // OnCardsReceived is called when the cards data is received
    void OnCardsReceived(JObject dataReceived)
    {
        CardsData cardsData = JsonUtility.FromJson<CardsData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["cards"]));
        gameController.LoadCardsReceived(cardsData);

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

    // OnPlayersReceived is called when the players data is received
    void OnPlayersReceived(JObject dataReceived)
    {
        gameController.LoadLeaderboard(dataReceived["players"].ToObject<JArray>());
    }

    // OnJoinGameReceived is called when a request to join the game is received
    void OnJoinGameReceived(JObject dataReceived)
    {
        List<int> badges = new List<int>();

        foreach (int badge in dataReceived["badges"])
        {
            badges.Add(badge);
        }

        gameController.AddPlayer((int)dataReceived["userId"], (string)dataReceived["name"], (int)dataReceived["points"], (int)dataReceived["position"], (int)dataReceived["numTurns"], (int)dataReceived["totalAnswers"], (int)dataReceived["correctAnswers"], (string)dataReceived["avatar"], badges, (int)dataReceived["multiplier"], (bool)dataReceived["finishedBoard"]);
    }

    // OnDiceResultReceived is called when the dice result is received
    void OnDiceResultReceived(JObject dataReceived)
    {
        gameController.MovePlayer((int)dataReceived["result"]);
    }

    // OnSaveFilesReceived is called when the save files are received
    void OnSaveFilesReceived(JObject dataReceived)
    {
        List<string> files = new List<string>();

        foreach (string file in dataReceived["files"])
        {
            files.Add(file);
        }

        gameController.ShowSaveFiles(files);
    }

    // OnRemovePlayerReceived is called when a player leaves
    void OnRemovePlayerReceived(JObject dataReceived)
    {
        gameController.RemovePlayer((int)dataReceived["userId"]);
    }

    // Resend last message sent
    public void ResendLastMessage()
    {
        SendMessage(lastMessage);
    }

    // OnContentViewedReceived is called when a player views the content sent
    void OnContentViewedReceived()
    {
        gameController.ContinueTrainCard();
    }

    // OnNewBadgeReceived is called when a player buys a badge
    void OnNewBadgeReceived(JObject dataReceived)
    {
        gameController.AddBadgeToPlayer((int)dataReceived["userId"], (int)dataReceived["badgeId"], (int)dataReceived["points"], (int)dataReceived["multiplier"]);
    }

    // OnContinueReceived is called after player clicks the continue button when a card is showing
    void OnContinueReceived()
    {
        gameController.ContinueCard();
    }

    // OnNextPlayerReceived is called after a player clicks the continue button to finish his turn
    void OnNextPlayerReceived()
    {
        gameController.ChangeCurrentPlayer();
    }
}
