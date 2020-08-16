using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Entity
{
    public override bool CanWin(Entity entity)
    {
        if (entity is Girl)
            Dead();
        else if (entity is House)
            Dead();
        return false;
    }
}
