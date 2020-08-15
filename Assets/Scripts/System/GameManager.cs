using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    TurnType cp;

    public TurnType CurrentPlayer {
        get {
            return cp;
        }
        set {
            OnTurnEnd?.Invoke();
            cp = value;
            OnTurnStart?.Invoke();

            //OnTurnEnd();
            //switch (cp)
            //{
            //    case TurnType.Empty:
            //        break;
            //    case TurnType.One:
            //        break;
            //    case TurnType.Two:
            //        break;
            //    default:
            //        break;
            //}
            //cp = value;

            //OnTurnStart();
            //switch (cp)
            //{
            //    case TurnType.Empty:
            //        break;
            //    case TurnType.One:
            //        break;
            //    case TurnType.Two:
            //        break;
            //    default:
            //        break;
            //}
        }
    }

    public event Action OnTurnStart;
    public event Action OnTurnEnd;

    private void Awake()
    {
        CurrentPlayer = TurnType.One;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change()
    {
        if (CurrentPlayer == TurnType.One)
            CurrentPlayer = TurnType.Two;
        else if (CurrentPlayer == TurnType.Two)
            CurrentPlayer = TurnType.One;
    }

    public void MoveEnd()
    {
        Change();
    }

    public static bool IsPlayer(TurnType player)
    {
        return Instance.CurrentPlayer == player;
    }
}
