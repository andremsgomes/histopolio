using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    private GameController gameController;
    private DiceUI diceUI;
    private bool coroutineAllowed = true;

    [Header("Test")]
    [SerializeField] private bool test;
    [SerializeField] private int diceRoll;

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
        if (coroutineAllowed) {
            if (test)
                gameController.MovePlayer(diceRoll);
            else
                StartCoroutine("RollDiceCoroutine");
        }
    }

    private IEnumerator RollDiceCoroutine() {
        coroutineAllowed = false;
        int randomRotations = Random.Range(10,26);
        int randomDiceSide = 1;

        for (int i = 0; i < randomRotations; i++) {
            randomDiceSide = Random.Range(1,7);
            diceUI.ChangeDiceSide(randomDiceSide);
            
            yield return new WaitForSeconds(0.08f);
        }

        gameController.MovePlayer(randomDiceSide);
    }

    // Set game manager
    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    // Set dice UI
    public void SetDiceComponents() {
        diceUI = this.GetComponent<DiceUI>();

        diceUI.SetDiceController(this);
    }

    // Allow coroutine
    public void AllowCoroutine() {
        coroutineAllowed = true;
    }

    // Show dice
    public void ShowDice() {
        diceUI.ShowDice();
    }

    // Hide dice
    public void HideDice() {
        diceUI.HideDice();
    }
}
