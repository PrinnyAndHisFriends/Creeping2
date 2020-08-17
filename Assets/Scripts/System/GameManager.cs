using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    PlayerType cp = PlayerType.PlayerOne;

    public PlayerType CurrentTurn {
        get {
            return cp;
        }
        set {
            OnTurnEndEvent?.Invoke();
            if (!IsGameEnd())
            {
                cp = value;
                OnTurnStartEvent?.Invoke();
            }
            else
            {
                cp = PlayerType.Empty;
                OnGameEndEvent?.Invoke();
            }
        }
    }
    public List<Client> players;
    public Client CurrentPlayer { get; set; }
    public Client PlayerOne;
    public Client PlayerTwo;

    public event Action OnGameStartEvent;
    public event Action OnTurnStartEvent;
    public event Action OnCardTurnStartEvent;
    public event Action OnCardTurnTriggerAreaEvent;
    public event Action OnMoveTurnStartEvent;
    public event Action OnMoveTurnTriggerAreaEvent;
    public event Action OnTurnEndEvent;
    public event Action OnGameEndEvent;

    private void Awake()
    {
        OnGameStartEvent += () => Log("OnGameStart");
        OnTurnStartEvent += () => Log("OnTurnStartEvent");
        OnCardTurnStartEvent += () => Log("OnCardTurnStartEvent");
        OnCardTurnTriggerAreaEvent += () => Log("OnCardTurnTriggerAreaEvent");
        OnMoveTurnStartEvent += () => Log("OnMoveTurnStartEvent");
        OnMoveTurnTriggerAreaEvent += () => Log("OnMoveTurnTriggerAreaEvent");
        OnTurnEndEvent += () => Log("OnTurnEndEvent");
        OnGameEndEvent += ()=> Log("OnGameEnd");
    }

    // Start is called before the first frame update
    void Start()
    {
        OnGameStartEvent?.Invoke();
        OnTurnStartEvent?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTurnStart()
    {
        Log("OnTurnStart");
        CurrentPlayer = players.Find((a) => a.activeType == CurrentTurn);
        CurrentPlayer.OnTurnStart();
    }
    void OnTurnEnd()
    {
        Log("OnTurnEnd");
        CurrentPlayer.OnTurnEnd();
        OnTurnEndEvent?.Invoke();
    }

    public void MoveEndAndChangeTurn()
    {
        Log("MoveEndAndChangeTurn");
        if (CurrentTurn == PlayerType.PlayerOne)
            CurrentTurn = PlayerType.Two;
        else if (CurrentTurn == PlayerType.Two)
            CurrentTurn = PlayerType.PlayerOne;
    }

    public void EndGame()
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
