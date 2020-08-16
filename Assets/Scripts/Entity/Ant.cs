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

    public override void TurnStart()
    {
        base.TurnStart();
        animator.SetBool("Idle", true);
    }
    public override void TurnEnd()
    {
        base.TurnEnd();
        animator.SetBool("Idle", false);
    }

    public override bool CanWin(Entity entity)
    {
        if (entity is Girl)
            Dead();
        else if (entity is House)
            Dead();
        return false;
    }
}
