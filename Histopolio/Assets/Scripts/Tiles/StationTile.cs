using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationTile : Tile
{
    private int points;
    [SerializeField] private Text pointsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Perform action when player arrives to tile
    public override void PerformAction() {
        return;
    }

    // Set points
    public void SetPoints(int points) {
        this.points = points;
        pointsText.text = "- " + points;
    }

    // Get points
    public int GetPoints() {
        return points;
    }
}
