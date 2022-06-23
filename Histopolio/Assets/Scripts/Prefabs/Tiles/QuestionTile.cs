using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestionTile : PointsTile
{
    private List<QuestionData> questions = new List<QuestionData>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Ask a random question
    public override void PerformAction()
    {
        if (questions.Count > 0)
        {
            int index = Random.Range(0, questions.Count);

            gameController.PrepareQuestion(questions[index]);
        }
        else
        {
            gameController.FinishTurn();
        }
    }

    // Get points
    public int GetPoints()
    {
        return points;
    }

    // Add question
    public void AddQuestion(QuestionData question)
    {
        questions.Add(question);
    }
}