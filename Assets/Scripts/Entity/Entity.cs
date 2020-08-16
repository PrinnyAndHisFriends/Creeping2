using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
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

    public abstract void AttackedBy(Entity entity);

    public void Dead()
    {
        EntitySystem.Instance.DestroyEntity(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


    public void Init(Vector3Int index, Client player)
    {
        Index = index;
        this.player = player;
        transform.position = AreaSystem.Instance.GetWorldPosition(index);
    }

    public void TurnInit()
    {
        State = InnerState.Idle;
    }
}
