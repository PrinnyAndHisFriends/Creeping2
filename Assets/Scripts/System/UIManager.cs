using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>, IGameEvent
{
    // Start is called before the first frame update
    void Awake()
    {
    }

    public void OnGameStart()
    {
    }

    public void OnTurnStart()
    {
        if (GameManager.Instance.IsPlayer(PlayerType.PlayerOne))
            ;//highlight
        else
            ;//highlight
    }

    public void OnCardTurnStart()
    {
        throw new NotImplementedException();
    }

    public void OnCardTurnEnd()
    {
        throw new NotImplementedException();
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
        if (GameManager.Instance.IsPlayer(PlayerType.PlayerOne))
            ;//cancel highlight
        else
            ;//highlight
    }

    public void OnGameEnd()
    {
        //show ui
        //to main
    }
}
