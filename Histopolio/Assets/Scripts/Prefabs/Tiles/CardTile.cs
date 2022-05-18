using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTile : Tile
{
    private List<TrainCardData> cards = new List<TrainCardData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add card
    public void AddCard(TrainCardData card) {
        cards.Add(card);
    }

    // Draw a random card
    public override void PerformAction() {
        int index = Random.Range(0,cards.Count);
        
        gameController.PrepareCard(cards[index]);
    }
}
