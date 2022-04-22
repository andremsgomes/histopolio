using WebSocketSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class WebSocketClientController : MonoBehaviour
{
    private WebSocket ws;
    private GameController gameController;
    private string message;
    private bool processAllowed = false;

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");   // TODO: mudar para variavel
        
        ws.OnMessage += (sender, e) => {
            message = e.Data;
            processAllowed = true;
        };

        ws.Connect();

        string id = JsonUtility.ToJson(new IdentificationData());
        SendMessage(id);
    }

    // Update is called once per frame
    void Update()
    {
        if (processAllowed) {
            processAllowed = false;
            ProcessMessage();
        }
    }

    // Process message received
    void ProcessMessage() {
        Debug.Log(message);

        JObject dataReceived = JObject.Parse(message);
        string command = (string)dataReceived["type"];

        switch (command) {
            case "answer":
                OnAnswerReceived(dataReceived);
                break;
            default:
                Debug.LogError("Unknown message: " + message);
                break;
        }
    }

    // OnAnswerReceived is called when an answer is received
    public void OnAnswerReceived(JObject dataReceived) {
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
}
