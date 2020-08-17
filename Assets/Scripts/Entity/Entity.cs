using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonobehaviourExtension
{
    public bool IsFinishMove { get; set; }
    public bool CanDrag { get => player.IsActivePlayer && !IsFinishMove; }
    public Vector3Int index { get; set; }
    public Client player;

    public Vector3 oldPos;

    private void OnMouseDown()
    {
        if (CanDrag)
        {
            oldPos = transform.position;
        }
    }
    private void OnMouseDrag()
    {
        if (CanDrag)
        {
            transform.position = AreaSystem.Instance.mousePosInWorld;
        }
    }

    private void OnMouseUp()
    {
        if (CanDrag)
        {
            var worldPos = AreaSystem.Instance.mousePosInWorld;
            var targetIndex = AreaSystem.Instance.GetIndex(worldPos);
            if (CanMove(targetIndex))
            {
                EntitySystem.Instance.Move(this, targetIndex);
            }
            else
            {
                transform.position = oldPos;
            }
        }
    }

    public virtual void Init(Vector3Int index, Client player=null)
    {
        this.player = player;
        this.index = index;
        SetPosition(AreaSystem.Instance.GetWorldPosition(index));
    }

    public bool CanMove(Vector3Int targetIndex)
    {
        if (index == targetIndex)
            return false;
        return index != targetIndex || EntitySystem.Instance.CanMoveIfHasSomeEntity(targetIndex) || CanMoveToArea(AreaSystem.Instance.GetArea(targetIndex));
    }

    public void Move(Vector3Int targetIndex)
    {
        var targetPos = AreaSystem.Instance.GetWorldPosition(targetIndex);
        SetPosition(targetPos);
        IsFinishMove = true;
    }

    public virtual void Dead()
    {
        EntitySystem.Instance.UnregistryEntity(this);
        StartCoroutine(WaittingForDestroy());
    }

    public virtual void MoveTurnStart()
    {
        IsFinishMove = false;
    }

    public virtual void MoveTurnEnd()
    {
        IsFinishMove = false;
    }

    public virtual bool CanMoveToArea(Area area)
    {
        return true;
    }

    public abstract bool AttackByAndAlive(Entity entity);

    void SetPosition(Vector3 pos)
    {
        transform.position = pos + Vector3.up * 0.5f;
    }

    bool doDestroy = false;
    IEnumerator WaittingForDestroy()
    {
        yield return new WaitUntil(() => doDestroy);
        Destroy(gameObject);
    }

    void DoDestroy()
    {
        doDestroy = true;
    }
}
