using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonobehaviourExtension
{
    private enum InnerState { Empty, Card, Move }

    private InnerState State { get; set; } = InnerState.Empty;

    public bool IsCardMode { get => State== InnerState.Card; }
    public bool IsMoveMode { get => State== InnerState.Move; }

    public PlayerType activeType;
    List<Entity> entityList = new List<Entity>();
    Card currentCard;

    private void Start()
    {
        if (activeType == PlayerType.PlayerOne)
        {
            EntitySystem.Instance.GenerateEntity(Setting.EntityType.Girl, Setting.girlStartIndex);
            EntitySystem.Instance.GenerateEntity(Setting.EntityType.House, Setting.houseStartIndex);
        }
        else
        {
            EntitySystem.Instance.GenerateEntity(Setting.EntityType.Ant, Setting.antStartIndex);
        }
    }


    public void OnTurnStart()
    {
        enabled = GameManager.Instance.IsPlayer(activeType);
        if (enabled)
        {
            currentCard = CardSystem.Instance.ShowCard();
            entityList.ForEach((a) => a.TurnInit());
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
            CheckEntity();
        }
    }

    public void AddEntity(Entity entity)
    {
        entityList.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        entityList.Remove(entity);
    }

    public void CheckEntity()
    {
        //Debug.Log(entityList.Count);
        //Debug.Log(entityList[0].GetType());
        //Debug.Log(entityList[1].GetType());
        if (entityList.TrueForAll((a) => a.IsFinishMove))
        {
            State = InnerState.Empty;
            GameManager.Instance.MoveEndAndChangeTurn();
        }
    }
}
