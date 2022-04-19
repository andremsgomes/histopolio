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

        JObject response = JObject.Parse(message);
        string command = (string)response["type"];

        switch (command) {
            case "answer":
                gameController.CheckAnswerFromServer((int)response["answer"]);
                break;
            default:
                Debug.LogError("Unknown message: " + message);
                break;
        }
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
