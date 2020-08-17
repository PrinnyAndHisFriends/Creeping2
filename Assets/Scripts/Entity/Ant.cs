using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Entity
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
        if (entity is Girl)
        {
            Dead();
            return false;
        }
        else if (entity is House)
        {
            Dead();
            return false;
        }
        return true;
    }
}
