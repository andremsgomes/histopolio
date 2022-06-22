using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardSlot : MonoBehaviour
{
    [SerializeField] private Text boardText;

    private MainMenuController mainMenuController;
    private string board;

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

    // Set file name
    public void SetBoard(string board) {
        this.board = board;
        this.boardText.text = board;
    }

    // OnButtonClick is called when the button is clicked
    public void OnButtonClick() {
        mainMenuController.RequestBoardData(board);
        mainMenuController.ShowBoardMenu();
    }
}
