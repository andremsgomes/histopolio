using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private GameManager gameManager;
    private bool coroutineAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Roll the dice
    public void RollDice() {
        if (coroutineAllowed)
            StartCoroutine("RollDiceCoroutine");
    }

    private IEnumerator RollDiceCoroutine() {
        coroutineAllowed = false;
        int randomRotations = Random.Range(10,26);
        int randomDiceSide = 1;

        for (int i = 0; i < randomRotations; i++) {
            randomDiceSide = Random.Range(1,7);
            gameManager.ChangeDiceSide(randomDiceSide);
            
            yield return new WaitForSeconds(0.08f);
        }

        gameManager.MovePlayer(randomDiceSide);
    }

    // Set game manager
    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
    }

    // Allow coroutine
    public void AllowCoroutine() {
        coroutineAllowed = true;
    }
}
