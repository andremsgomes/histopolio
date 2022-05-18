using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PointsTile : Tile
{
    protected int points;
    [SerializeField] protected Text pointsText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set points
    public abstract void SetPoints(int points);

    // Get points
    public int GetPoints() {
        return points;
    }
}