using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionController : MonoBehaviour
{
    private QuestionUI questionUI;
    private int correctAnswer;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Load questions from file
    public void LoadQuestions(string fileRelativePath) {
        string jsonString = File.ReadAllText(Application.dataPath + "/" + fileRelativePath);
        QuestionsData questionsData = JsonUtility.FromJson<QuestionsData>(jsonString);

        foreach (QuestionData question in questionsData.questions) {
            gameManager.AddQuestion(question);
        }
    }

    // Load, set, and show question from question data
    public void LoadQuestion(QuestionData questionData) {
        questionUI.SetQuestion(questionData.question);
        questionUI.SetAnswers(questionData.answers);

        correctAnswer = questionData.correctAnswer;
    }

    // Check if answer is correct
    public void CheckAnswer(int answer) {
        gameManager.FinishQuestion(answer == correctAnswer);
    }

    // Set game manager
    public void SetGameManager(GameManager gameManager) {
        this.gameManager = gameManager;
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
}
