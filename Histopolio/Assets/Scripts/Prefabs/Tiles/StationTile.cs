using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationTile : PointsTile
{
    private List<CardData> cards = new List<CardData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set points
    public override void SetPoints(int points) {
        this.points = points;
        pointsText.text = "+ " + points;
    }

    // Draw a random card
    public override void PerformAction() {
        int index = Random.Range(0,cards.Count);
        
        gameController.PrepareCard(cards[index]);
    }

    // Add card
    public void AddCard(CardData card) {
        cards.Add(card);
    }
}
