using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonobehaviourExtension
{
    public enum InnerState { Idle, Moved, Dead }
    InnerState state = InnerState.Idle;
    public InnerState State
    {
        get
        {
            return state;
        }
        set
        {
            switch (state)
            {
                case InnerState.Idle:
                    break;
                case InnerState.Moved:
                    break;
                case InnerState.Dead:
                    break;
                default:
                    break;
            }
            state = value;
            switch (state)
            {
                case InnerState.Idle:
                    break;
                case InnerState.Moved:
                    break;
                case InnerState.Dead:
                    break;
                default:
                    break;
            }
        }
    }

    public bool IsFinishMove { get => State == InnerState.Moved; }
    public bool IsDead { get => State == InnerState.Dead; }

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

    public void Dead()
    {
        Log("Dead");
        EntitySystem.Instance.DestroyEntity(this);
        Destroy(gameObject);
    }

    public void Init(Vector3Int index, Client player)
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

    public void TurnInit()
    {
        State = InnerState.Idle;
    }

    void SetPosition(Vector3 pos)
    {
        transform.position = pos + Vector3.up * 0.5f;
    }
}
