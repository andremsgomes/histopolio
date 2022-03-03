using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupPropertyTile : Tile
{
    private int points;
    private Color groupColor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add points when player arrives to tile
    public override void PerformAction() {
        return;
    }

    // Set points
    public void SetPoints(int points) {
        this.points = points;
    }

    // Get points
    public int GetPoints() {
        return points;
    }

    // Set group color
    public void SetGroupColor(Color color) {
        this.groupColor = color;
    }

    // Get group color
    public Color GetGroupColor() {
        return groupColor;
    }
}
