using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    private int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set id
    public void SetId(int id) {
        this.id = id;
    }

    // Get id
    public int GetId() {
        return id;
    }

    // Perform tile action when player arrives at tile
    public abstract void PerformAction();
}
