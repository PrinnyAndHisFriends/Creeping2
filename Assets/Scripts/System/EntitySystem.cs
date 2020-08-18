using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySystem : MonoSingleton<EntitySystem>
{
    public GameObject girl;
    public GameObject ant;
    public GameObject house;
    public GameObject epresent;

    Dictionary<Vector3Int, List<Entity>> entitys = new Dictionary<Vector3Int, List<Entity>>();

    private void Awake()
    {
        for (int i = Setting.MIN_AREA; i <= Setting.MAX_AREA; i++)
        {
            for (int j = Setting.MIN_AREA; j <= Setting.MAX_AREA; j++)
            {
                Vector3Int key = new Vector3Int(i, j, 0);
                if (AreaSystem.Instance.HasArea(key))
                {
                    entitys[key] = new List<Entity>();
                }
            }
        }
    }

    public bool HasEntity(Vector3Int index)
    {
        return entitys.ContainsKey(index) && entitys[index].Count != 0;
    }

    public List<Entity> GetEntitys(Vector3Int index)
    {
        if (entitys.ContainsKey(index))
        {
            return entitys[index];
        }
        return null;
    }

    public void AddEntity(Entity entity)
    {
        entitys[entity.index].Add(entity);
    }
    public void RemoveEntity(Entity entity)
    {
        entitys[entity.index].Remove(entity);
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
            case Setting.EntityType.EPresent:
                entity = Instantiate(epresent).GetComponent<Entity>();
                break;
            default:
                break;
        }
        RegistryEntity(entity, index, player);
    }
    public void DestroyEntity(Entity entity)
    {
        UnregistryEntity(entity);
        Destroy(gameObject);
    }

    public void RegistryEntity(Entity entity, Vector3Int index, Client player=null)
    {
        entitys[index].Add(entity);
        entity.Init(index, player);
        if (player)
        {
            player.AddEntity(entity);
        }
    }

    public void UnregistryEntity(Entity entity)
    {
        entity.player.RemoveEntity(entity);
        entitys[entity.index].Remove(entity);
    }

    public bool CanMoveIfHasSomeEntity(Vector3Int targetIndex)
    {
        var temp = GetEntitys(targetIndex);
        bool ret = true;
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] is House)
                ret = false;
        }
        return ret;
    }

    public void Move(Entity entity, Vector3Int targetIndex)
    {
        var index = entity.index;
        if (AreaSystem.Instance.CanMove(index, targetIndex))
        {
            var oldIndex = entity.index;
            entitys[oldIndex].Remove(entity);
            entitys[index].Add(entity);
            entity.index = index;
            entity.Move(targetIndex);

            Attack(entity, targetIndex);
            AreaSystem.Instance.TriggerArea(targetIndex, ()=> {
                Attack(entity, targetIndex);
                AttackArea(entity, targetIndex);
                GameManager.Instance.TriggerTurn2End();
            });
        }
    }


    public void Attack(Entity entity, Vector3Int index)
    {
        Log("Attack");
        if (entitys.ContainsKey(index))
        {
            var temp = entitys[index];
            bool ret = true;
            for (int i = 0; i < temp.Count; i++)
            {
                var entity2 = temp[i];
                entity2.AttackByAndAlive(entity);
                if (!entity.AttackByAndAlive(entity2))
                    ret = false;
            }
            if (ret)
                entitys[index].Add(entity);
        }
    }
    public void AttackArea(Entity entity, Vector3Int index)
    {
        Log("AttackArea");
        if (entitys.ContainsKey(index))
        {
            Area area = AreaSystem.Instance.GetArea(index);
            if (entity is EPresent && area is GapArea)
            {
                DestroyEntity(entity);
                AreaSystem.Instance.SetArea(index, new GrassArea());
            }
        }
    }
}
