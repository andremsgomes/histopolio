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
        // TODO: mostrar novo menu
    }

    // Show choose board menu
    public void LoadNewGame() {
        gameController.RequestBoardData();
    }

    // Start new game
    public void StartNewGame() {
        gameController.StartNewGame();
    }

    // Check if game is loaded
    public bool GetGameLoaded() {
        return gameController.GetGameLoaded();
    }

    // Show new player on join menu
    public void ShowNewPlayer(int index, Color color) {
        mainMenuUI.ShowNewPlayer(index, color);
    }
}
