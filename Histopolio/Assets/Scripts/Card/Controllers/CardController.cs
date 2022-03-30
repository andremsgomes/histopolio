using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardController : MonoBehaviour
{
    private CardUI cardUI;
    private int correctAnswer;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Connect with UI
    public void SetCardComponents() {
        cardUI = this.GetComponent<CardUI>();

        cardUI.SetCardController(this);
    }

    // Set game controller
    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    // Load cards from file
    public void LoadCards(string fileRelativePath) {
        string jsonString = File.ReadAllText(Application.dataPath + "/" + fileRelativePath);
        CardsData cardsData = JsonUtility.FromJson<CardsData>(jsonString);

        foreach (CardData card in cardsData.cards) {
            gameController.AddCard(card);
        }
    }

    // Load, set, and show info from card data
    public void LoadCard(CardData cardData) {
        cardUI.SetInfo(cardData.info);
    }

    // Continue turn
    public void Continue() {
        gameController.FinishTurn();
    }

    // Activate card menu
    public void ShowCardMenu() {
        cardUI.ShowCardMenu();
    }
}
