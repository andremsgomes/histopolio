using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardController : MonoBehaviour
{
    private CardUI cardUI;
    private int points;
    private string action;
    private string actionValue;
    private GameController gameController;
    private List<CardData> communityCards = new List<CardData>();
    private List<CardData> chanceCards = new List<CardData>();

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
    public void LoadCards(List<CardData> cards) {
        foreach (CardData card in cards)
        {   
            if (card.type == "train")
                gameController.AddCard(card);
            else if (card.subtype == "chance") {
                chanceCards.Add(card);
            }
            else {
                communityCards.Add(card);
            }
        }

        Debug.Log("Cards loaded");
    }

    // Load, set, and show info from card data
    public void LoadCard(CardData cardData) {
        action = "none";
        actionValue = "";
        cardUI.SetInfo(cardData.info);
    }

    // Continue turn
    public void Continue() {
        cardUI.HideCardMenu();
        gameController.GiveCurrentPlayerPoints(points);

        switch (action)
        {
            case "move":
                gameController.MovePlayer(int.Parse(actionValue));
                break; 
            case "tile":
                gameController.MovePlayerTo(int.Parse(actionValue));
                break;
            default:
                string info = "";
                if (points < 0) {
                    info = "Perdeste " + points + " pontos!";
                }
                else if (points > 0)
                    info = "Parabéns! Recebeste " + points * gameController.GetCurrentPlayer().GetMultiplier() + " pontos!";

                gameController.FinishTurn(info);
                break;
        }
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
        action = communityCards[index].action;
        actionValue = communityCards[index].actionValue;
        cardUI.SetInfo(communityCards[index].info);

        ShowCardMenu(false);
        gameController.SendInfoShownMessageToServer();
    }

    // Show random chance card
    public void ShowChanceCard() {
        int index = Random.Range(0, chanceCards.Count);
        
        points = chanceCards[index].points;
        action = chanceCards[index].action;
        actionValue = chanceCards[index].actionValue;
        cardUI.SetInfo(chanceCards[index].info);

        ShowCardMenu(false);
        gameController.SendInfoShownMessageToServer();
    }

    // Get points
    public int GetPoints()
    {
        return points;
    }
}
