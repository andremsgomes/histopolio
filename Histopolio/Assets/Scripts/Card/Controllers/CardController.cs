using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardController : MonoBehaviour
{
    private CardUI cardUI;
    private int points;
    private GameController gameController;
    private List<NoTileCardData> communityCards = new List<NoTileCardData>();

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
    public void LoadCards(CardsData cardsData) {
        foreach (NoTileCardData card in cardsData.communityCards)
        {   
            communityCards.Add(card);
        }

        foreach (TrainCardData card in cardsData.trainCards)
        {
            gameController.AddCard(card);
        }

        Debug.Log("Cards loaded");
    }

    // Load, set, and show info from card data
    public void LoadCard(TrainCardData cardData) {
        points = cardData.points;
        cardUI.SetInfo(cardData.info);
    }

    // Continue turn
    public void Continue() {
        gameController.GiveCurrentPlayerPoints(points);
        gameController.SendInfoShownMessageToServer();
        gameController.FinishTurn();
    }

    // Activate card menu
    public void ShowCardMenu(bool showButton) {
        cardUI.ShowCardMenu(showButton);
    }

    // Deactivate card menu
    public void HideCardMenu() {
        cardUI.HideCardMenu();
    }

    // Show random community card
    public void ShowCommunityCard() {
        int index = Random.Range(0, communityCards.Count);
        
        points = communityCards[index].points;
        cardUI.SetInfo(communityCards[index].info);

        ShowCardMenu(true);
    }

    // Get points
    public int GetPoints()
    {
        return points;
    }
}
