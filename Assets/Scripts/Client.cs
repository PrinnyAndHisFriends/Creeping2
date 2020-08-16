using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonobehaviourExtension
{
    private enum InnerState { Empty, Card, Move }

    private InnerState State { get; set; } = InnerState.Empty;

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

    public void MoveEntity(int id, Vector3Int index)
    {
        if (State == InnerState.Move)
        {
            if (AreaSystem.Instance.CanMove(entityList[id].Index, index))
            {
                EntitySystem.Instance.Move(entityList[id], index);
            }
            CheckEntity();
        }
    }

    private void CheckEntity()
    {
        if (entityList.TrueForAll((a) => a.IsFinishMove))
        {
            State = InnerState.Empty;
            GameManager.Instance.MoveEndAndChangeTurn();
        }
    }
}
