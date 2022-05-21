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
    [SerializeField] private GameObject[] leaderboardPlaces = new GameObject[3];
    [SerializeField] private Image[] leaderboardAvatars = new Image[3];
    [SerializeField] private Text[] leaderboardScores = new Text[3];
    [SerializeField] private GameObject badgesContainer;
    [SerializeField] private Badge badgePrefab;

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

    // Set current player badges
    public void SetBadges(List<Sprite> badges) {
        // Remove previous badges
        foreach (Transform child in badgesContainer.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Sprite sprite in badges)
        {
            Badge badge = Instantiate(badgePrefab);
            badge.transform.SetParent(badgesContainer.transform);
            badge.SetImage(sprite);
        }
    }

    // OnFinishTurnClick is called when finish turn button is clicked
    public void OnFinishTurnClick() {
        turnButton.SetActive(false);
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

    // Show place in leaderboard
    public void UpdateLeaderboard(int index, Sprite avatar, string name, int points) {
        leaderboardPlaces[index].SetActive(true);
        leaderboardAvatars[index].sprite = avatar; 
        leaderboardScores[index].text = name + " - " + points;
    }
}
