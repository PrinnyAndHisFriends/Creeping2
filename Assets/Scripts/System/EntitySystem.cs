using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySystem : MonoSingleton<EntitySystem>
{
    Dictionary<Vector3Int, Entity> entitys = new Dictionary<Vector3Int, Entity>();
    public void Move(Entity entity, Vector3Int index)
    {
        Log("Move");
        MovePost(entity, index);
        PlayMoveAnimation();
    }

    private void MovePost(Entity entity, Vector3Int index)
    {
        var oldIndex = entity.CurrentAreaIndex;
        entitys[oldIndex] = null;
        entity.CurrentAreaIndex = index;
        entitys[index] = entity;
    }

    public void PlayMoveAnimation()
    {

    }
}
