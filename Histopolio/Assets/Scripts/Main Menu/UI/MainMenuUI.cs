using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private MainMenuController mainMenuController;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;

    [Header("Initial Menu")]
    [SerializeField] private GameObject initialMenu;

    [Header("Join Menu")]
    [SerializeField] private GameObject joinMenu;
    [SerializeField] private GameObject[] players = new GameObject[24];
    [SerializeField] private Text[] playerNames = new Text[24];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set main menu controller
    public void SetMainMenuController(MainMenuController mainMenuController) {
        this.mainMenuController = mainMenuController;
    }

    // OnLoadGameClick is called when the load game button is clicked
    public void OnLoadGameClick() {
        // menu.SetActive(false);
        // mainMenuController.LoadGame();
    }

    // OnNewGameClick is called when the new game button is clicked
    public void OnNewGameClick() {
        initialMenu.SetActive(false);
        joinMenu.SetActive(true);
        mainMenuController.LoadNewGame();
    }

    // OnStartClick when the start button is clicked
    public void OnStartClick() {
        if (!mainMenuController.GetGameLoaded()) return;

        mainMenuController.StartNewGame();
        joinMenu.SetActive(false);
        mainMenu.SetActive(false);
    }
}
