using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonobehaviourExtension
{
    private enum InnerState { Empty, Card, Move }

    private InnerState State { get; set; } = InnerState.Empty;

    public TurnType activeType;
    List<Entity> entityList = new List<Entity>();
    Card currentCard;


    public void OnTurnStart()
    {
        enabled = GameManager.Instance.IsPlayer(activeType);
        if (enabled)
        {
            currentCard = CardSystem.Instance.ShowCard();
            entityList.ForEach((a) => a.IsFinishMove = false);
            State = InnerState.Card;
            Log(activeType.ToString());
        }
    }

    public void OnTurnEnd()
    {
        enabled = false;
    }


    public void UseCard()
    {
        if (State == InnerState.Card)
        {
            Vector3Int index;
            if (!AreaSystem.Instance.GetTile(out index))
                return;
            CardSystem.Instance.UseCard(currentCard, index);
            State = InnerState.Move;
        }
    }

    public void MoveEntity(int id, Vector3Int index)
    {
        if (State == InnerState.Move)
        {
            if (AreaSystem.Instance.CanMove(entityList[id].CurrentAreaIndex, index))
            {
                EntitySystem.Instance.Move(entityList[id], index);
            }

            if (entityList.TrueForAll((a) => a.IsFinishMove))
            {
                GameManager.Instance.MoveEnd();
            }
        }
    }
}
