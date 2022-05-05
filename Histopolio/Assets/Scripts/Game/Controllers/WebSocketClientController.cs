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

    // Request load data from server
    void RequestLoadData(string type, string board) {
        LoadDataRequest loadDataRequest = new LoadDataRequest();
        loadDataRequest.type = type;
        loadDataRequest.board = board;

        string loadDataRequestString = JsonUtility.ToJson(loadDataRequest);

        SendMessage(loadDataRequestString);
    }

    // Request board data from server
    public void RequestBoardData(string board) {
        RequestLoadData("load board", board);
    }

    // OnBoardReceived is callend when the board data is received
    void OnBoardReceived(JObject dataReceived) {
        BoardData boardData = JsonUtility.FromJson<BoardData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["board"]));
        gameController.LoadBoardReceived(boardData);

        RequestQuestionsData(boardData.name);
    }

    // Request questions data from server
    void RequestQuestionsData(string board) {
        RequestLoadData("load questions", board);
    }

    // OnQuestionsReceived is called when the questions data is received
    void OnQuestionsReceived(JObject dataReceived) {
        QuestionsData questionsData = JsonUtility.FromJson<QuestionsData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["questions"]));
        gameController.LoadQuestionsReceived(questionsData);

        RequestCardsData(questionsData.board);
    }

    // Request cards data from server
    void RequestCardsData(string board) {
        RequestLoadData("load cards", board);
    }

    // OnCardsReceived is called when the cards data is received
    void OnCardsReceived(JObject dataReceived) {
        CardsData cardsData = JsonUtility.FromJson<CardsData>(Newtonsoft.Json.JsonConvert.SerializeObject(dataReceived["cards"]));
        gameController.LoadCardsReceived(cardsData);

        gameController.SetGameLoaded(true);
    }

    // OnJoinGameReceived is called when a request to join the game is received
    void OnJoinGameReceived(JObject dataReceived) {
        gameController.AddPlayer((int)dataReceived["id"], (string)dataReceived["name"]);
    }
}
