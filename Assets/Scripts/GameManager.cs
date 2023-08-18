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

    [Header("Cards Result")]
    public Text ScoreTxt;
    public Text ResultTxt;
    public Text TotalMoveTxt;
    public Text ChanceTxt;

    public GameObject ResultPanel;
    public AudioSource audioSource;
    public AudioClip FlipedAudio;
    public AudioClip UnFlipedAudio;




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


    public void LoadGameData()
    {
        StartCoroutine(LoadingData());

    }

    IEnumerator LoadingData()
    {
        foreach (Card item in cards)
        {
            DestroyImmediate(item.gameObject);
        }


        cards.Clear();
        yield return new WaitForSeconds(2f);
        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            gameData = JsonUtility.FromJson<GameData>(jsonData);

            foreach (int cardID in gameData.flippedCardIDs)
            {

                InstantiateCardSet(cardID, 1, true);

            }
            foreach (int cardID in gameData.UnflippedCardIDs)
            {
                InstantiateCardSet(cardID, 1, false);

            }

        }
    }

    public void GameStartFunction()
    {
        for (int i = 0; i < TotalCards; i++)
        {
            InstantiateCardSet(Random.Range(1, 9), 2, false);
        }

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

        StartCoroutine(ShuffleCardPostions());
    }





    private IEnumerator ShuffleCardPostions()
    {

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.parent = cardParent;
           // cards[i].transform.DOScale(Vector3.one, 0.5f);

            if (cards[i].isFlipped)
            {
                cards[i].FlipCard();
            }
            else
            {
                cards[i].UnflipCard();
            }
        }


        ShuffleCardFunction();


    }


    void ShuffleCardFunction()
    {
        for (int i = 0; i < cards.Count * 2; i++)
        {
            cards[Random.Range(0, cardParent.childCount)].transform.SetAsFirstSibling();
        }
    }

    private Card firstCard = null;
    private Card secondCard = null;
    int score;
    int ChanceCount;
    int MoveCount;

    public void CardClicked(Card clickedCard)
    {
        MoveCount++;
        if (firstCard == null)
        {
            firstCard = clickedCard;
            firstCard.FlipCard();
        }
        else if (secondCard == null)
        {
            secondCard = clickedCard;
            secondCard.FlipCard();

         
            if (firstCard.cardID == secondCard.cardID)
            {
            
             
                firstCard = null;
                secondCard = null;
                score += 100;
                ScoreTxt.text = "Score: " + score.ToString();
                audioSource.PlayOneShot(FlipedAudio);
            }
            else
            {
                audioSource.PlayOneShot(UnFlipedAudio);
                ChanceCount++;
                if (ChanceCount == 4)
                {
                    ResultPanel.SetActive(true);
                    ResultTxt.text = "TOTAL SCORE: " + score.ToString();
                    TotalMoveTxt.text = "TOTAL MOVE: " + MoveCount.ToString();
                }
                ChanceTxt.text = "Chance " + ChanceCount.ToString() + "/3";
             
            }
        }
    }





}
