using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.OnTurnStartEvent += OnTurnStart;
        GameManager.Instance.OnTurnEndEvent += OnTurnEnd;
        GameManager.Instance.OnGameStartEvent += OnGameStart;
        GameManager.Instance.OnGameEndEvent += OnGameEnd;
    }


    void OnTurnStart()
    {
        if (GameManager.Instance.IsPlayer(PlayerType.PlayerOne))
            ;//highlight
        else
            ;//highlight
    }
    void OnTurnEnd()
    {
        if (GameManager.Instance.IsPlayer(PlayerType.PlayerOne))
            ;//cancel highlight
        else
            ;//highlight
    }

    void OnGameStart()
    {

    }

    void OnGameEnd()
    {
        //show ui
        //to main
    }
}
