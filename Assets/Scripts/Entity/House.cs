using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Entity
{
    public int life = Setting.LIFE;
    public override bool CanWin(Entity entity)
    {
        if (entity is Ant)
        {
            life--;
            if (life == 0)
                GameManager.Instance.EndGame();
        }
        return true;
    }
}
