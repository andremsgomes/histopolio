using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupPropertyTile : Tile
{
    private int points;
    private Color groupColor;
    [SerializeField] private Text pointsText;
    [SerializeField] private GameObject group;


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
        pointsText.text = "+ " + points;
    }

    // Get points
    public int GetPoints() {
        return points;
    }

    // Set group color
    public void SetGroupColor(Color color) {
        this.groupColor = color;
        group.GetComponent<SpriteRenderer>().color = color;
    }

    // Get group color
    public Color GetGroupColor() {
        return groupColor;
    }

    // Get type
    public override string GetTileType()
    {
        return "groupPropertyTile";
    }
}
