using WebSocketSharp;
using UnityEngine;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");   // TODO: mudar para variavel
        
        ws.OnMessage += (sender, e) => {
            Debug.Log("Message received from " + e.Data);
        };

        ws.Connect();

        Debug.Log("web socket set");
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null)
            Debug.Log("web socket???");
        else {
            if (Input.GetKeyDown(KeyCode.Space))
                ws.Send("Hello");
        }
    }
}
