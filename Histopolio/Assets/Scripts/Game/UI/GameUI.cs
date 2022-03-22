using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private Text playerNameText;
    [SerializeField] private Image playerColor;
    [SerializeField] private Text playerScoreText;
    [SerializeField] private GameObject turnButton;

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

    // Set current player name text
    public void SetPlayerNameText(string name) {
        playerNameText.text = name;
    }

    // Set current player color
    public void SetPlayerColor(Color color) {
        playerColor.color = color;
    }

    // Set current player score
    public void SetPlayerScore(int score) {
        playerScoreText.text = score.ToString() + " Pontos";
    }

    // OnFinishTurnClick is called when finish turn button is clicked
    public void OnFinishTurnClick() {
        turnButton.SetActive(false);
        gameManager.ChangeCurrentPlayer();
        gameManager.ShowDice();
    }

    // DisplayFinishTurn is called after player moved
    public void DisplayFinishTurn() {
        gameManager.HideDice();
        turnButton.SetActive(true);
    }
}
