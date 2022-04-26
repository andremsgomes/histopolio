using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionController : MonoBehaviour
{
    private QuestionUI questionUI;
    private int correctAnswer;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Load questions from file
    public void LoadQuestions(QuestionsData questionsData) {
        foreach (QuestionData question in questionsData.questions) {
            gameController.AddQuestion(question);
        }
    }

    // Load, set, and show question from question data
    public void LoadQuestion(QuestionData questionData) {
        questionUI.SetQuestion(questionData.question);
        questionUI.SetAnswers(questionData.answers);

        correctAnswer = questionData.correctAnswer;

        SendQuestionToServer(questionData);
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
        string message = JsonUtility.ToJson(questionData);

        gameController.SendMessageToServer(message);
    }
}
