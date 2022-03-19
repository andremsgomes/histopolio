using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    private QuestionController questionController;

    [SerializeField] private Text question;
    [SerializeField] private Text[] answers = new Text[4];
    [SerializeField] private GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnAnswerClick is called when an answer is clicked
    public void OnAnswerClick(int answer) {
        menu.SetActive(false);
        questionController.CheckAnswer(answer);
    }

    // Set question controller
    public void SetQuestionController(QuestionController questionController) {
        this.questionController = questionController;
    }

    // Set question
    public void SetQuestion(string question) {
        this.question.text = question;
    }

    // Set answers
    public void SetAnswers(string[] answers) {
        for (int i = 0; i < answers.Length; i++) {
            this.answers[i].text = answers[i];
        }
    }

    // Show question menu
    public void ShowQuestionMenu() {
        menu.SetActive(true);
    }
}
