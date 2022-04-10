using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private MainMenuController mainMenuController;

    [SerializeField] private GameObject menu;

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
        menu.SetActive(false);
        mainMenuController.LoadGame();
    }

    // OnNewGameClick is called when the new game button is clicked
    public void OnNewGameClick() {
        menu.SetActive(false);
        mainMenuController.NewGame();
    }
}
