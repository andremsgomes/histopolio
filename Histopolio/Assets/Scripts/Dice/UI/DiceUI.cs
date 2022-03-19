using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    private DiceController diceController;

    [SerializeField] private GameObject dice;
    [SerializeField] private Text diceSide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set dice controller
    public void SetDiceController(DiceController diceController) {
        this.diceController = diceController;
    }

    // OnDiceClick is called when roll dice button is clicked
    public void OnDiceClick() {
        diceController.RollDice();
    }
    
    // Change dice side after rotation
    public void ChangeDiceSide(int side) {
        diceSide.text = side.ToString();
    }

    // Show dice with face 1
    public void ShowDice() {
        diceSide.text = "1";
        dice.SetActive(true);
    }

    // Hide dice
    public void HideDice() {
        dice.SetActive(false);
    }
}
