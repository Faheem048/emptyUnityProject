using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<int> flippedCardIDs = new List<int>();
    public List<int> UnflippedCardIDs = new List<int>();
    public List<int> ShuffleCards = new List<int>();
}
