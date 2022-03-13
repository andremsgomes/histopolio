using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private Text playerNameText;
    [SerializeField] private Image playerColor;
    [SerializeField] private Text playerScoreText;
    [SerializeField] private GameObject dice;
    [SerializeField] private GameObject turnButton;
    [SerializeField] private Text diceSide;

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

    // OnDiceClick is called when roll dice button is clicked
    public void OnDiceClick() {
        gameManager.PlayTurn();
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
        diceSide.text = "1";
        dice.SetActive(true);
    }

    // Change dice side after rotation
    public void ChangeDiceSide(int side) {
        diceSide.text = side.ToString();
    }

    // DisplayFinishTurn is called after player moved
    public void DisplayFinishTurn() {
        dice.SetActive(false);
        turnButton.SetActive(true);
    }
}
