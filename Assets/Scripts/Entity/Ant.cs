using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Entity
{
    public override void AttackedBy(Entity entity)
    {
        if (entity is Girl)
            Dead();
    }
}
