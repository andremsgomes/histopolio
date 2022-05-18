using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    private CardController cardController;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private Text info;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnContinueClick is called when continue button is clicked
    public void OnContinueClick() {
        menu.SetActive(false);
        cardController.Continue();
    }

    // Set card controller
    public void SetCardController(CardController cardController) {
        this.cardController = cardController;
    }

    // Set info
    public void SetInfo(string info) {
        this.info.text = info;
    }

    // Show card menu
    public void ShowCardMenu(bool showButton) {
        menu.SetActive(true);
        continueButton.SetActive(showButton);
    }

    // Hide card menu
    public void HideCardMenu() {
        menu.SetActive(false);
    }
}
