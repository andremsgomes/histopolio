using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 boardPosition;    

    // Start is called before the first frame update
    void Start()
    {
        boardPosition = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Move is called after a dice is rolled
    public void Move(int spaces) {
        boardPosition.x += spaces;
        transform.position = new Vector3(transform.position.x-spaces, transform.position.y, transform.position.z);
    }
}
