using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : Entity
{
    public override bool CanWin(Entity entity)
    {
        if (entity is Ant)
            return true;
        else if (entity is House)
            return false;
        return false;
    }
}
