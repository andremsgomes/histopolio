using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    private QuestionController questionController;

    [SerializeField] private Text question;
    [SerializeField] private GameObject[] answers = new GameObject[6];
    [SerializeField] private Text[] answersText = new Text[6];
    [SerializeField] private GameObject menu;
    [SerializeField] private Beardy.GridLayoutGroup grid;


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
            this.answers[i].SetActive(true);
            this.answersText[i].text = answers[i];
        }

        if (answers.Length == 2)
            grid.cellSize = new Vector2(910, 600);
        else if (answers.Length == 3)
            grid.cellSize = new Vector2(600, 600);
        else if (answers.Length == 4)
            grid.cellSize = new Vector2(910, 300);
        else
            grid.cellSize = new Vector2(600,300);
    }

    // Show question menu
    public void ShowQuestionMenu() {
        menu.SetActive(true);
    }

    // Hide question menu
    public void HideQuestionMenu() {
        for (int i = 0; i < answers.Length; i++) {
            answers[i].SetActive(false);
        }
        menu.SetActive(false);
    }
}
