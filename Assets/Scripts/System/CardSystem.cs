using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoSingleton<CardSystem>
{
    List<Card> cards = new List<Card>();
    Queue<Card> cardQueue = new Queue<Card>();
    public event Action<Card> OnShowCardEvent;
    public Card currentCard;


    void Awake()
    {
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Gap]; i++)
            cards.Add(new GapCard());
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Way1_2]; i++)
            cards.Add(new Way1_2Card());
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Way1_3]; i++)
            cards.Add(new Way1_3Card());
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Way1_4]; i++)
            cards.Add(new Way1_4Card());
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Way1_2_4]; i++)
            cards.Add(new Way1_2_4Card());
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Way1_3_4]; i++)
            cards.Add(new Way1_3_4Card());
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Way1_3_5]; i++)
            cards.Add(new Way1_3_5Card());
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Way1_3_4_6]; i++)
            cards.Add(new Way1_3_4_6Card());
        for (int i = 0; i < Setting.cardCount[Setting.CardType.Way1_2_3_4_5_6]; i++)
            cards.Add(new Way1_2_3_4_5_6Card());

        for (int i = 0; i < Setting.CARD_DECK_COUNT; i++)
        {
            int id = UnityEngine.Random.Range(0, cards.Count);
            //Debug.LogError(id + " " + cards.Count);
            var data = cards[id];
            cards.RemoveAt(id);
            cardQueue.Enqueue(data);
        }

        if (cards.Count != 0)
            Debug.LogError(ToString() + "Card count error");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card ShowCard()
    {
        var c = cardQueue.Dequeue();
        OnShowCardEvent?.Invoke(c);
        currentCard = c;
        return c;
    }

    public void UseCard(Card card, Vector3Int index)
    {
        AreaSystem.Instance.SetArea(index, card.ToArea());
    }

    public bool IsDeckEmpty()
    {
        return cardQueue.Count == 0;
    }

    public int CountRemainedCard()
    {
        Debug.Log("totle:" + Setting.CARD_DECK_COUNT + "remained:" + cardQueue.Count);
        return cardQueue.Count;
    }
    
}
