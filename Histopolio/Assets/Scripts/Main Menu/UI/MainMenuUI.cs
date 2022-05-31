using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private MainMenuController mainMenuController;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject gameCreation;
    [SerializeField] private InputField newGameField;

    [Header("Initial Menu")]
    [SerializeField] private GameObject initialMenu;

    [Header("Join Menu")]
    [SerializeField] private GameObject joinMenu;
    [SerializeField] private GameObject playersContainer;
    [SerializeField] private JoinPlayer joinPlayerPrefab;
    [SerializeField] private Text title;

    [Header("Saves Menu")]
    [SerializeField] private GameObject savesMenu;
    [SerializeField] private GameObject savesContainer;
    [SerializeField] private SaveSlot saveSlotPrefab;

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
        mainMenuController.LoadGame();
        initialMenu.SetActive(false);
        savesMenu.SetActive(true);
    }

    // Show save files
    public void ShowSaveFiles(List<string> files) {
        foreach (string file in files) {
            SaveSlot saveSlot = Instantiate(saveSlotPrefab);
            saveSlot.transform.SetParent(savesContainer.transform, false);
            saveSlot.SetFileName(file);
            saveSlot.SetMainMenuController(mainMenuController);
        }
    }

    // OnNewGameClick is called when the new game button is clicked
    public void OnNewGameClick() {
        newGameButton.SetActive(false);
        gameCreation.SetActive(true);
    }

    // OnGoClick is called when the GO button is clicked
    public void OnGoClick() {
        mainMenuController.NewGame();

        string fileName = newGameField.text;
        mainMenuController.LoadSaveFile(fileName);
        
        initialMenu.SetActive(false);
        joinMenu.SetActive(true);
    }

    // OnStartClick when the start button is clicked
    public void OnStartClick() {
        if (!mainMenuController.GetGameLoaded()) return;

        mainMenuController.StartGame();
        joinMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    // Show new player on join menu
    public void ShowNewPlayer(Sprite avatar) {
        JoinPlayer joinPlayer = Instantiate(joinPlayerPrefab);
        joinPlayer.transform.SetParent(playersContainer.transform);
        joinPlayer.SetAvatar(avatar);
    }

    // Hide saves menu
    public void HideSavesMenu() {
        savesMenu.SetActive(false);
    }

    // Show join menu
    public void ShowJoinMenu(string board, string save) {
        joinMenu.SetActive(true);
        title.text = board + " - " + save;
    }
}
