using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionController : MonoBehaviour
{
    private QuestionUI questionUI;
    private int correctAnswer;
    private GameController gameController;
    private List<QuestionData> questions = new List<QuestionData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Load questions from file
    public void LoadQuestions(List<QuestionData> questionsData) {
        foreach (QuestionData question in questionsData) {
            gameController.AddQuestion(question);
            questions.Add(question);
        }

        Debug.Log("Questions loaded");
    }

    // Load, set, and show question from question data
    public void LoadQuestion(QuestionData questionData) {
        questionUI.SetQuestion(questionData.question);
        questionUI.SetAnswers(questionData.answers);

        correctAnswer = questionData.correctAnswer;

        SendQuestionToServer(questionData);
    }

    public void LoadRandomQuestion() {
        int index = Random.Range(0,questions.Count);
        LoadQuestion(questions[index]);
    }

    // Check if answer is correct
    public void CheckAnswer(int answer) {
        questionUI.HideQuestionMenu();

        gameController.FinishQuestion(answer == correctAnswer);
    }

    // Set game manager
    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    // Connect with UI
    public void SetQuestionComponents() {
        questionUI = this.GetComponent<QuestionUI>();

        questionUI.SetQuestionController(this);
    }

    // Activate question menu
    public void ShowQuestionMenu() {
        questionUI.ShowQuestionMenu();
    }

    // Send question to the server
    void SendQuestionToServer(QuestionData questionData) {
        gameController.SendQuestionToServer(questionData);
    }
}
