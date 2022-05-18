using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayTile : QuestionTile
{
    // Set points
    public override void SetPoints(int points) {
        this.points = -1*points;
        pointsText.text = "- " + points;
    }
}
