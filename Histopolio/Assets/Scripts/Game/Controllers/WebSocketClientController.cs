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

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");   // TODO: mudar para variavel
        
        ws.OnMessage += (sender, e) => {
            messages.Enqueue(e.Data);
        };

        ws.Connect();

        string id = JsonUtility.ToJson(new IdentificationData());
        SendMessage(id);
    }

    // Update is called once per frame
    void Update()
    {
        while (messages.Count > 0) {
            ProcessMessage(messages.Dequeue());
        }
    }

    // Process message received
    void ProcessMessage(string message) {
        Debug.Log(message);

        JObject dataReceived = JObject.Parse(message);
        string command = (string)dataReceived["type"];

        switch (command) {
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
            case "join game":
                OnJoinGameReceived(dataReceived);
                break;
            case "dice result":
                OnDiceResultReceived(dataReceived);
                break;
            case "save files":
                OnSaveFilesReceived(dataReceived);
                break;
            default:
                Debug.LogError("Unknown message: " + message);
                break;
        }

        message = null;
    }

    // OnAnswerReceived is called when an answer is received
    void OnAnswerReceived(JObject dataReceived) {
        gameController.CheckAnswerFromServer((int)dataReceived["answer"]);
    }

    // Send message to the server
    public void SendMessage(string message) {
        ws.Send(message);
    }

    // Set game controller
    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    // Send board requests to server
    public void SendBoardRequest(string type, string board) {
        BoardSendData boardSendData = new BoardSendData();
        boardSendData.type = type;
        boardSendData.board = board;

        string boardSendDataString = JsonUtility.ToJson(boardSendData);

        SendMessage(boardSendDataString);
    }

    // Request board data from server
    public void RequestBoardData(string board) {
        SendBoardRequest("load board", board);
    }

    // OnBoardReceived is callend when the board data is received
    void OnBoardReceived(JObject dataReceived) {
        BoardData boardData = JsonUtility.FromJson<BoardData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["board"]));
        gameController.LoadBoardReceived(boardData);

        RequestQuestionsData(boardData.name);
    }

    // Request questions data from server
    void RequestQuestionsData(string board) {
        SendBoardRequest("load questions", board);
    }

    // OnQuestionsReceived is called when the questions data is received
    void OnQuestionsReceived(JObject dataReceived) {
        QuestionsData questionsData = JsonUtility.FromJson<QuestionsData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["questions"]));
        gameController.LoadQuestionsReceived(questionsData);

        RequestCardsData(questionsData.board);
    }

    // Request cards data from server
    void RequestCardsData(string board) {
        SendBoardRequest("load cards", board);
    }

    // OnCardsReceived is called when the cards data is received
    void OnCardsReceived(JObject dataReceived) {
        CardsData cardsData = JsonUtility.FromJson<CardsData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["cards"]));
        gameController.LoadCardsReceived(cardsData);

        gameController.SetGameLoaded(true);
    }

    // OnJoinGameReceived is called when a request to join the game is received
    void OnJoinGameReceived(JObject dataReceived) {
        gameController.AddPlayer((int)dataReceived["userId"], (string)dataReceived["name"], (int)dataReceived["points"], (int)dataReceived["position"], (string)dataReceived["image"]);
    }

    // OnDiceResultReceived is called when the dice result is received
    void OnDiceResultReceived(JObject dataReceived) {
        gameController.MovePlayer((int)dataReceived["result"]);
    }

    // OnSaveFilesReceived is called when the save files are received
    void OnSaveFilesReceived(JObject dataReceived) {
        List<string> files = new List<string>();

        foreach (string file in dataReceived["files"]) {
            files.Add(file);
        }

        gameController.ShowSaveFiles(files);
    }
}
