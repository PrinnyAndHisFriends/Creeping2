using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySystem : MonoSingleton<EntitySystem>
{
    Dictionary<Vector3Int, Entity> entitys = new Dictionary<Vector3Int, Entity>();

    public void Move(Entity entity, Vector3Int index)
    {
        Log("Move");
        PlayMoveAnimation();
        MovePost(entity, index);
    }

    private void MovePost(Entity entity, Vector3Int index)
    {
        var oldIndex = entity.CurrentAreaIndex;
        entitys[oldIndex] = null;
        entity.CurrentAreaIndex = index;
        if (entitys.ContainsKey(index))
        {
            entitys[index].AttackedBy(entity);
            entity.AttackedBy(entitys[index]);
        }
        entitys[index] = entity;
    }

    public void PlayMoveAnimation()
    {

    }
}
