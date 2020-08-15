using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    TurnType cp = TurnType.One;

    public TurnType CurrentTurn {
        get {
            return cp;
        }
        set {
            OnTurnEnd();
            if (!IsGameEnd())
            {
                cp = value;
                OnTurnStart();
            }
            else
            {
                cp = TurnType.Empty;
                OnGameEnd();
            }
        }
    }
    public List<Client> players;
    public Client CurrentPlayer { get; set; }

    public event Action OnTurnStartEvent;
    public event Action OnTurnEndEvent;
    public event Action OnGameStartEvent;
    public event Action OnGameEndEvent;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        OnGameStart();
        OnTurnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGameStart()
    {
        Log("OnGameStart");
        OnGameStartEvent?.Invoke();
    }
    void OnGameEnd()
    {
        Log("OnGameEnd");
        OnGameEndEvent?.Invoke();
    }
    void OnTurnStart()
    {
        Log("OnTurnStart");
        CurrentPlayer = players.Find((a) => a.activeType == CurrentTurn);
        OnTurnStartEvent?.Invoke();
        CurrentPlayer.OnTurnStart();
    }
    void OnTurnEnd()
    {
        Log("OnTurnEnd");
        CurrentPlayer.OnTurnEnd();
        OnTurnEndEvent?.Invoke();
    }

    void Change()
    {
        if (CurrentTurn == TurnType.One)
            CurrentTurn = TurnType.Two;
        else if (CurrentTurn == TurnType.Two)
            CurrentTurn = TurnType.One;
    }

    public void MoveEnd()
    {
        Change();
    }

    bool IsGameEnd()
    {
        bool ret = false;
        ret |= CardSystem.Instance.IsDeckEmpty();
        return ret;
    }
    public bool IsPlayer(TurnType player)
    {
        return CurrentTurn == player;
    }
}
