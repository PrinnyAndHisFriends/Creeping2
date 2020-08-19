using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGameLogic
{
    void GameStart();
    void TurnStart();
    void CardTurnStart();
    void WantToUseCard();
    void CardTurnEnd();
    void UseCard();
    void MoveTurnStart();
    void MoveTurnEnd();
    void TurnEnd();
    void ChangeTurn();
    void GameEnd();
}

interface IGameEvent
{
    void OnGameStart();
    void OnTurnStart();
    void OnCardTurnStart();
    void OnCardTurnEnd();
    void OnMoveTurnStart();
    void OnMoveTurnEnd();
    void OnTurnEnd();
    void OnGameEnd();
    void OnCardWillbeUsedEvent();
    void OnCardUsedEvent();
}


public class GameManager : MonoSingleton<GameManager>, IGameLogic
{
    public PlayerType CurrentTurn { get; set; } = PlayerType.PlayerOne;

    public Client CurrentPlayer { get => PlayerOne.playerType == CurrentTurn ? PlayerOne : PlayerTwo; }
    public Client PlayerOne;
    public Client PlayerTwo;

    public event Action OnGameStartEvent;
    public event Action OnTurnStartEvent;
    public event Action OnCardTurnStartEvent;
    public event Action OnCardTurnEndEvent;
    public event Action OnMoveTurnStartEvent;
    public event Action OnMoveTurnEndEvent;
    public event Action OnTurnEndEvent;
    public event Action OnGameEndEvent;

    public event Action OnCardWillbeUsedEvent;
    public event Action OnCardUsedEvent;
    

    private void Awake()
    {
        OnGameStartEvent += () => Log("OnGameStartEvent");
        OnTurnStartEvent += () => Log("OnTurnStartEvent");
        OnCardTurnStartEvent += () => Log("OnCardTurnStartEvent");
        OnCardTurnEndEvent += () => Log("OnCardTurnEndEvent");
        OnMoveTurnStartEvent += () => Log("OnMoveTurnStartEvent");
        OnMoveTurnEndEvent += () => Log("OnMoveTurnEndEvent");
        OnTurnEndEvent += () => Log("OnTurnEndEvent");
        OnGameEndEvent += ()=> Log("OnGameEndEvent");

        OnCardWillbeUsedEvent += () => Log("OnCardWillbeUsedEvent");
        OnCardUsedEvent += () => Log("OnCardUsedEvent");
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
        TurnStart();
        //初始化GameManager - Event - Player
        //结束化Player - Event - GameManager
    }
    public void GameStart()
    {
        OnGameStartEvent?.Invoke();
        PlayerOne.GameStart();
        PlayerTwo.GameStart();
    }

    public void TurnStart()
    {
        OnTurnStartEvent?.Invoke();
        //CurrentPlayer.TurnStart();
        CardTurnStart();
    }

    public void CardTurnStart()
    {
        OnCardTurnStartEvent?.Invoke();
    }

    public void WantToUseCard()
    {
        if (CurrentPlayer.CanUseCard())
        {
            OnCardWillbeUsedEvent?.Invoke();
            CurrentPlayer.TriggerArea();
        }
    }
    public void TriggerTurn1End()
    {
        Log("TriggerTurn1End");
        UseCard();
    }

    public void UseCard()
    {
        OnCardUsedEvent?.Invoke();
        CurrentPlayer.UseCard();
        CardTurnEnd();
    }

    public void CardTurnEnd()
    {
        OnCardTurnEndEvent?.Invoke();
        MoveTurnStart();
    }

    public void MoveTurnStart()
    {
        OnMoveTurnStartEvent?.Invoke();
        CurrentPlayer.MoveTurnStart();
    }

    public void TriggerTurn2End()
    {
        CurrentPlayer.CheckEntity();
    }

    public void MoveTurnEnd()
    {
        OnMoveTurnEndEvent?.Invoke();
        CurrentPlayer.MoveTurnEnd();
        TurnEnd();
    }

    public void TurnEnd()
    {
        CurrentPlayer.TurnEnd();
        OnTurnEndEvent?.Invoke();
        if (!IsGameEnd())
        {
            ChangeTurn();
        }
        else
        {
            GameEnd();
        }
    }

    public void ChangeTurn()
    {
        if (CurrentTurn == PlayerType.PlayerOne)
            CurrentTurn = PlayerType.Two;
        else if (CurrentTurn == PlayerType.Two)
            CurrentTurn = PlayerType.PlayerOne;
        TurnStart();
    }

    public void GameEnd()
    {
        OnGameEndEvent?.Invoke();
    }


    bool IsGameEnd()
    {
        bool ret = false;
        ret |= CardSystem.Instance.IsDeckEmpty();
        return ret;
    }
    public bool IsPlayer(PlayerType player)
    {
        return CurrentTurn == player;
    }
}
