using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private Text playerNameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnCameraChangeClick is called when camera change button is clicked
    public void OnCameraChangeClick()
    {
        gameManager.ChangeCamera();
    }

    // Set game manager
    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }

    // OnRollDiceClick is called when roll dice button is clicked
    public void OnRollDiceClick() {
        gameManager.MovePlayer();
    }

    // Set current player name text
    public void SetPlayerNameText(string name) {
        playerNameText.text = name;
    }
}
