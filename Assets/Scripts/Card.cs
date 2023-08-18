using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;
    public bool isFlipped;
    public GameObject cardBack;
    public Sprite spriteIcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void FlipCard()
    {
        isFlipped = true;
        cardBack.SetActive(false);

    }
    public void UnflipCard()
    {
        isFlipped = false;
        cardBack.SetActive(true);

    }

    public void ChangeSprite()
    {
        GetComponent<Image>().sprite = spriteIcon;
    }

}
