using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

 
}
