using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonobehaviourExtension
{
    private enum InnerState { Empty, Card, Trigger, Move }
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
            entityList.ForEach((a) => a.TurnStart());
            State = InnerState.Card;
            Log("OnTurnStart");
        }
    }

    public void OnTurnEnd()
    {
        enabled = false;
        entityList.ForEach((a) => a.TurnEnd());
        Log("OnTurnEnd");
    }


    public void UseCard()
    {
        if (State == InnerState.Card)
        {
            Vector3Int index;
            if (!AreaSystem.Instance.GetTile(out index))
                return;
            if (CardSystem.Instance.UseCard(currentCard, index))
            {
                State = InnerState.Trigger;
                if ()
                AreaSystem.Instance.TriggerArea(index,
                    () => {
                        AreaSystem.Instance.SetArea(index, currentCard.ToArea());
                        State = InnerState.Move;
                    });
            }
        }
    }

    public void Move(Entity entity, Vector3Int oriIndex, Vector3Int targetIndex)
    {
        if (State == InnerState.Move)
        {
            if (oriIndex == targetIndex)
                return;
            var targetPos = AreaSystem.Instance.GetWorldPosition(targetIndex);
            if (AreaSystem.Instance.CanMove(oriIndex, targetIndex))
            {
                entity.MoveTo(targetPos);
                EntitySystem.Instance.Attack(entity, targetIndex);
                AreaSystem.Instance.TriggerArea(targetIndex, ()=> {
                    EntitySystem.Instance.Attack(entity, targetIndex);
                    CheckEntity();
                });
            }
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
        if (entityList.TrueForAll((a) => a.IsFinishMove))
        {
            State = InnerState.Empty;
            GameManager.Instance.MoveEndAndChangeTurn();
        }
    }
}
