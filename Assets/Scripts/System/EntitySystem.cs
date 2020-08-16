using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySystem : MonoSingleton<EntitySystem>
{
    public GameObject girl;
    public GameObject ant;
    public GameObject house;

    Dictionary<Vector3Int, Entity> entitys = new Dictionary<Vector3Int, Entity>();

    public Entity GetEntity(Vector3Int index)
    {
        if (entitys.ContainsKey(index))
        {
            return entitys[index];
        }
        return null;
    }

    public void GenerateEntity(Setting.EntityType type, Vector3Int index)
    {
        Entity entity = null;
        Client player = null;
        switch (type)
        {
            case Setting.EntityType.Girl:
                entity = Instantiate(girl).GetComponent<Entity>();
                player = GameManager.Instance.PlayerOne;
                break;
            case Setting.EntityType.Ant:
                entity = Instantiate(ant).GetComponent<Entity>();
                player = GameManager.Instance.PlayerTwo;
                break;
            case Setting.EntityType.House:
                entity = Instantiate(house).GetComponent<Entity>();
                break;
            default:
                break;
        }
        entitys[index] = entity;
        if (player)
        {
            player.AddEntity(entity);
            entity.Init(index, player);
        }
    }

    public void DestroyEntity(Entity entity)
    {
        entity.player.RemoveEntity(entity);
        entitys.Remove(entity.Index);
    }

    public void Attack(Entity entity, Vector3Int index)
    {
        Log("Attack");
        if (entity.Index == index)
            return;
        var oldIndex = entity.Index;
        entitys.Remove(oldIndex);
        entity.Index = index;
        if (entitys.ContainsKey(index))
        {
            var entity2 = entitys[index];
            if (entity2.CanWin(entity))
                entitys[index] = entity2;
            if (entity.CanWin(entity2))
                entitys[index] = entity;
        }
    }

    public void PlayMoveAnimation()
    {

    }
}
