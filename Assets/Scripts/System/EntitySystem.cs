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
                player = GameManager.Instance.PlayerOne;
                break;
            default:
                break;
        }
        player.AddEntity(entity);
        entitys[index] = entity;
        entity.Init(index, player);
    }

    public void DestroyEntity(Entity entity)
    {
        entity.player.RemoveEntity(entity);
        entitys[entity.Index] = null;
        entity.Dead();
    }

    public void Move(Entity entity, Vector3Int index)
    {
        Log("Move");
        PlayMoveAnimation();
        MovePost(entity, index);
    }

    private void MovePost(Entity entity, Vector3Int index)
    {
        var oldIndex = entity.Index;
        entitys[oldIndex] = null;
        entity.Index = index;
        if (entitys.ContainsKey(index))
        {
            entitys[index].AttackedBy(entity);
            entity.AttackedBy(entitys[index]);
        }
    }

    public void PlayMoveAnimation()
    {

    }
}
