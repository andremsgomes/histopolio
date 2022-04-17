[System.Serializable]
public class QuestionData {
    public string type = "question";
    public int tileId;
    public string question;
    public string[] answers;
    public int correctAnswer;
}