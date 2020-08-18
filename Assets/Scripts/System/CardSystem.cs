using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoSingleton<CardSystem>, IGameEvent
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

        GameManager.Instance.OnCardTurnStartEvent += OnCardTurnStart;
        GameManager.Instance.OnCardTurnEndEvent += OnCardTurnEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetANewCard()
    {
        var c = cardQueue.Dequeue();
        currentCard = c;
    }


    public bool CanUseCard(Vector3Int index)
    {
        Area area = AreaSystem.Instance.GetArea(index);
        return !EntitySystem.Instance.HasEntity(index) && currentCard.CanUseCard(area);
    }
    public bool CanSetCard(Vector3Int index)
    {
        Area area = AreaSystem.Instance.GetArea(index);
        return currentCard.CanSetCard(area);
    }

    public bool IsDeckEmpty()
    {
        return cardQueue.Count == 0;
    }

    public int CountRemainedCard()
    {
        return cardQueue.Count;
    }

    public void OnGameStart()
    {
        throw new NotImplementedException();
    }

    public void OnTurnStart()
    {
        throw new NotImplementedException();
    }

    public void OnCardTurnStart()
    {
        GetANewCard();
        OnShowCardEvent?.Invoke(currentCard);
    }

    public void OnCardTurnEnd()
    {
    }

    public void OnMoveTurnStart()
    {
        throw new NotImplementedException();
    }

    public void OnMoveTurnEnd()
    {
        throw new NotImplementedException();
    }

    public void OnTurnEnd()
    {
        throw new NotImplementedException();
    }

    public void OnGameEnd()
    {
        throw new NotImplementedException();
    }

    public void OnCardWillbeUsedEvent()
    {
        throw new NotImplementedException();
    }

    public void OnCardUsedEvent()
    {
        throw new NotImplementedException();
    }
}
