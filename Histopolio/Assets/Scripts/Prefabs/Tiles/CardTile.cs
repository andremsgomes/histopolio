using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTile : Tile
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

    // Add card
    public void AddCard(CardData card) {
        cards.Add(card);
    }

    // Draw a random card
    public override void PerformAction() {
        int index = Random.Range(0,cards.Count);
        
        gameController.PrepareCard(cards[index]);
    }
}
