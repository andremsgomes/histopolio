using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private Text fileText;

    private MainMenuController mainMenuController;
    private string fileName;

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
    public void SetFileName(string fileName) {
        this.fileName = fileName;
        this.fileText.text = fileName;
    }

    // OnButtonClick is called when the button is clicked
    public void OnButtonClick() {
        mainMenuController.LoadSaveFile(fileName);
    }
}
