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
    void UseCard();
    void CardTurnEnd();
    void MoveTurnStart();
    void MoveTurnEnd();
    void TurnEnd();
    void ChangeTurn();
    void GameEnd();
}

public class GameManager : MonoSingleton<GameManager>, IGameLogic
{
    public PlayerType CurrentTurn { get; set;}

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

    private void Awake()
    {
        OnGameStartEvent += () => Log("OnGameStart");
        OnTurnStartEvent += () => Log("OnTurnStartEvent");
        OnCardTurnStartEvent += () => Log("OnCardTurnStartEvent");
        OnCardTurnEndEvent += () => Log("OnCardTurnEndEvent");
        OnMoveTurnStartEvent += () => Log("OnMoveTurnStartEvent");
        OnMoveTurnEndEvent += () => Log("OnMoveTurnEndEvent");
        OnTurnEndEvent += () => Log("OnTurnEndEvent");
        OnGameEndEvent += ()=> Log("OnGameEnd");
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
            CurrentPlayer.TriggerArea();
    }

    public void UseCard()
    {
        CurrentPlayer.UseCard();
        CardTurnEnd();
    }

    public void CardTurnEnd()
    {
        OnCardTurnEndEvent?.Invoke();
    }

    public void MoveTurnStart()
    {
        OnMoveTurnStartEvent?.Invoke();
        CurrentPlayer.MoveTurnStart();
    }


    public void MoveTurnEnd()
    {
        OnMoveTurnEndEvent?.Invoke();
        CurrentPlayer.MoveTurnEnd();
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
