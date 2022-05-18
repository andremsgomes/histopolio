using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupPropertyTile : QuestionTile
{
    private Color groupColor;
    [SerializeField] private GameObject group;

    // Set points
    public override void SetPoints(int points) {
        this.points = points;
        pointsText.text = "+ " + points;
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
}
