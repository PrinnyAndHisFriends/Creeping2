using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonobehaviourExtension
{
    public enum InnerState { Idle, Moved, Dead }

    public InnerState State { get; set; } = InnerState.Idle;

    public bool IsFinishMove { get => State == InnerState.Moved; }

    public Vector3Int Index { get; set; }
    public Client player;


    private void OnMouseDrag()
    {
        if (player.IsMoveMode && !IsFinishMove)
        {
            transform.position = AreaSystem.Instance.mousePosInWorld;
        }
    }

    private void OnMouseUp()
    {
        if (player.IsMoveMode && !IsFinishMove)
        {
            var worldPos = AreaSystem.Instance.mousePosInWorld;
            var targetIndex = AreaSystem.Instance.GetIndex(worldPos);
            player.Move(this, Index, targetIndex);
        }
    }

    public abstract bool CanWin(Entity entity);

    public virtual void Dead()
    {
        EntitySystem.Instance.DestroyEntity(this);
        StartCoroutine(WaittingForDestroy());
    }

    public virtual void Init(Vector3Int index, Client player)
    {
        Index = index;
        this.player = player;
        SetPosition(AreaSystem.Instance.GetWorldPosition(index));
    }

    public void MoveTo(Vector3 targetPos)
    {
        State = InnerState.Moved;
        SetPosition(targetPos);
    }

    public virtual void TurnStart()
    {
        State = InnerState.Idle;
    }

    public virtual void TurnEnd()
    {
        State = InnerState.Idle;
    }

    void SetPosition(Vector3 pos)
    {
        transform.position = pos + Vector3.up * 0.5f;
    }

    bool doDestroy = false;
    IEnumerator WaittingForDestroy()
    {
        yield return new WaitUntil(()=>doDestroy);
        Destroy(gameObject);
    }
    
    void DoDestroy()
    {
        doDestroy = true;
    }
}
