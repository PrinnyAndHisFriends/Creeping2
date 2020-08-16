using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    public Text remainedCards;

    private void Start()
    {
        remainedCards = GameObject.Find("Deck/Text").GetComponent<Text>();
        remainedCards.text = "127";
    }

    void Update()
    {
        remainedCards = GameObject.Find("Deck/Text").GetComponent<Text>();
        string cnt = CardSystem.Instance.CountRemainedCard().ToString();

        if (remainedCards.text != cnt)
        {
            remainedCards.text = cnt; 
        }
    }

}
