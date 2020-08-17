using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonobehaviourExtension
{
    #region Entity
    List<Entity> entityList = new List<Entity>();
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
            GameManager.Instance.MoveTurnEnd();
        }
    }
    #endregion


    #region Game Logic
    public PlayerType playerType;
    public bool IsActivePlayer { get => GameManager.Instance.IsPlayer(playerType); }
    Vector3Int index;

    public void GameStart()
    {
        if (playerType == PlayerType.PlayerOne)
        {
            EntitySystem.Instance.GenerateEntity(Setting.EntityType.Girl, Setting.girlStartIndex);
            EntitySystem.Instance.GenerateEntity(Setting.EntityType.House, Setting.houseStartIndex);
        }
        else
        {
            EntitySystem.Instance.GenerateEntity(Setting.EntityType.Ant, Setting.antStartIndex);
        }
    }

    public bool CanUseCard()
    {
        return AreaSystem.Instance.GetMouseIndex(out index)
            && CardSystem.Instance.CanUseCard(index);
    }

    public void TriggerArea()
    {
        AreaSystem.Instance.TriggerArea(index, GameManager.Instance.UseCard);
    }

    public void UseCard()
    {
        if (CardSystem.Instance.CanSetCard(index))
            AreaSystem.Instance.SetArea(index, CardSystem.Instance.currentCard.ToArea());
    }

    public void MoveTurnStart()
    {
        entityList.ForEach((a) => a.MoveTurnStart());
    }

    public void MoveTurnEnd()
    { 
        entityList.ForEach((a) => a.MoveTurnEnd());
    }

    public void TurnEnd()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeTurn()
    {
        throw new System.NotImplementedException();
    }

    public void GameEnd()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
