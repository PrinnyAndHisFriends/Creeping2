using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoSingleton<CardSystem>
{
    Queue<Card> cardQueue = new Queue<Card>();
    public event Action<Card> OnShowCardEvent;

    void Awake()
    {
        //Init
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card ShowCard()
    {
        var c = cardQueue.Dequeue();
        OnShowCardEvent?.Invoke(c);
        return c;
    }

    public void UseCard(Card card, Vector3Int index)
    {
        AreaSystem.Instance.SetArea(index, card.ToArea());
    }
}
