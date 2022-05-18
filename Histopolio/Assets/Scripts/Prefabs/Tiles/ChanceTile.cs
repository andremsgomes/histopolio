using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceTile : CardTile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Draw a random card
    public override void PerformAction() {
        gameController.ShowChanceCard();
    }
}
