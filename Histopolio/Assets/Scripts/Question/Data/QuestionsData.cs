using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class QuestionsData {
    public string board;
    public List<QuestionData> questions = new List<QuestionData>();
}