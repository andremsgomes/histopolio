using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private GameController gameController;

    [SerializeField] private Text playerNameText;
    [SerializeField] private Image avatar;
    [SerializeField] private Text playerScoreText;
    [SerializeField] private GameObject turnButton;
    [SerializeField] private GameObject HUD;

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
        gameController.ChangeCamera();
    }

    // Set game manager
    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    // Set current player name text
    public void SetPlayerNameText(string name) {
        playerNameText.text = name;
    }

    // Set current player avatar
    public void SetAvatar(Sprite avatar) {
        this.avatar.sprite = avatar;
    }

    // Set current player score
    public void SetPlayerScore(int score) {
        playerScoreText.text = score.ToString() + " Pontos";
    }

    // OnFinishTurnClick is called when finish turn button is clicked
    public void OnFinishTurnClick() {
        turnButton.SetActive(false);
        gameController.SaveCurrentPlayer();
        gameController.ChangeCurrentPlayer();
        // gameController.ShowDice();
    }

    // DisplayFinishTurn is called after player moved
    public void DisplayFinishTurn() {
        // gameController.HideDice();
        turnButton.SetActive(true);
    }

    // Show HUD
    public void ShowHUD() {
        HUD.SetActive(true);
    }
}
