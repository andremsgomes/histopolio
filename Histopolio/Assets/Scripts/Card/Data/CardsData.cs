using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CardsData {
    public List<NoTileCardData> communityCards = new List<NoTileCardData>();
    public List<TileCardData> cards = new List<TileCardData>();
}