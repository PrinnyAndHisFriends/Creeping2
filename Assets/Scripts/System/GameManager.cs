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
            OnTurnEnd();
            if (!IsGameEnd())
            {
                cp = value;
                OnTurnStart();
            }
            else
            {
                cp = PlayerType.Empty;
                OnGameEnd();
            }
        }
    }
    public List<Client> players;
    public Client CurrentPlayer { get; set; }
    public Client PlayerOne;
    public Client PlayerTwo;

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

    public void MoveEndAndChangeTurn()
    {
        if (CurrentTurn == PlayerType.PlayerOne)
            CurrentTurn = PlayerType.Two;
        else if (CurrentTurn == PlayerType.Two)
            CurrentTurn = PlayerType.PlayerOne;
    }

    public void EndGame()
    {
        OnGameEnd();
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
