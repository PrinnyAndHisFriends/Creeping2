using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public abstract void Rotate();
    public abstract Area ToArea();
    public abstract Sprite GetSprite();
}

public class EmptyCard : Card
{
    public override Sprite GetSprite()
    {
        return null;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}