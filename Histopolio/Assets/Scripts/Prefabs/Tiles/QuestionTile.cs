using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuestionTile : Tile
{
    protected int points;
    private List<QuestionData> questions = new List<QuestionData>();
    [SerializeField] protected Text pointsText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Ask a random question
    public override void PerformAction() {
        int index = Random.Range(0,questions.Count);
        
        gameController.PrepareQuestion(questions[index]);
    }

    // Set points
    public abstract void SetPoints(int points);

    // Get points
    public int GetPoints() {
        return points;
    }

    // Add question
    public void AddQuestion(QuestionData question) {
        questions.Add(question);
    }
}