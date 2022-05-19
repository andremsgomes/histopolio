using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CardsData {
    public List<DeckCardData> communityCards = new List<DeckCardData>();
    public List<DeckCardData> chanceCards = new List<DeckCardData>();
    public List<TrainCardData> trainCards = new List<TrainCardData>();
}