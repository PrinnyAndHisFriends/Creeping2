using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : Entity
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Init(Vector3Int index, Client player)
    {
        base.Init(index, player);
        animator.SetTrigger("Start");
    }
    public override void MoveTurnStart()
    {
        base.MoveTurnStart();
        animator.SetBool("Idle", true);
    }
    public override void MoveTurnEnd()
    {
        base.MoveTurnEnd();
        animator.SetBool("Idle", false);
    }

    public override bool AttackByAndAlive(Entity entity)
    {
        return true;
    }

    public override bool CanMoveToArea(Area area)
    {
        if (area is GapArea)
            return false;
        return true;
    }
}
