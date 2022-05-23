[System.Serializable]
public class SavePlayerData {
    public string type = "save";
    public int userId;
    public int points;
    public int position;
    public int numTurns;
    public int totalAnswers;
    public int correctAnswers;
    public bool finishedBoard;
}