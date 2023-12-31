using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        cardBack.transform.DOScale(Vector3.one, 0.3f);

    }

    public void ChangeSprite()
    {
        GetComponent<Image>().sprite = spriteIcon;
    }

    public void CheckCard()
    {
        GameManager.instance.CardClicked(this);
    }
}
