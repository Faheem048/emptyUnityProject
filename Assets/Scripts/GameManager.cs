using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Cards Items")]
    [Range(2, 9)]
    public int TotalCards;
    public GameObject cardPrefab;
    public Sprite[] CardsSprites;
    public List<Card> cards = new List<Card>();
    public Transform cardParent;

    private string dataFilePath;
    GameData gameData = new GameData();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        dataFilePath = Path.Combine(Application.persistentDataPath, "GameData.json");

    }


    public void SaveGameData()
    {
 
        gameData.flippedCardIDs.Clear();
        gameData.UnflippedCardIDs.Clear();
        foreach (Card card in cards)
        {
            if (card.isFlipped)
            {
                gameData.flippedCardIDs.Add(card.cardID);
            }
            else
            {
                gameData.UnflippedCardIDs.Add(card.cardID);
            }
        }

        string jsonData = JsonUtility.ToJson(gameData);
        File.WriteAllText(dataFilePath, jsonData);
    }
























    private void InstantiateCardSet(int cardID, int pack, bool Flip)
    {
        for (int i = 0; i < pack; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab);
            Card card = cardGO.GetComponent<Card>();
            card.cardID = cardID;
            card.spriteIcon = CardsSprites[cardID];
            card.ChangeSprite();
            card.isFlipped = Flip;
            cards.Add(card);

        }

    }
}
