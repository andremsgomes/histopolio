using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private GameController gameController;
    private MainMenuUI mainMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set game controller
    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    // Connect with UI
    public void SetMainMenuComponents() {
        mainMenuUI = this.GetComponent<MainMenuUI>();

        mainMenuUI.SetMainMenuController(this);
    }

    // Show load game menu
    public void LoadGame() {
        gameController.SendLoadSavesMessage();
        gameController.RequestBoardData();
    }

    // Show choose board menu
    public void NewGame() {
        gameController.RequestBoardData();
    }

    // Start new game
    public void StartGame() {
        gameController.StartGame();
    }

    // Check if game is loaded
    public bool GetGameLoaded() {
        return gameController.GetGameLoaded();
    }

    // Show new player on join menu
    public void ShowNewPlayer(Sprite avatar) {
        mainMenuUI.ShowNewPlayer(avatar);
    }

    // Show save files
    public void ShowSaveFiles(List<string> files) {
        mainMenuUI.ShowSaveFiles(files);
    }

    // Load save file from server and show join screen
    public void LoadSaveFile(string fileName) {
        gameController.LoadSaveFile(fileName);
        mainMenuUI.HideSavesMenu();
        mainMenuUI.ShowJoinMenu(gameController.GetBoard(), fileName.Substring(0, fileName.IndexOf(".json")));
    }
}
