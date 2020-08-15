using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Area 
{
    public abstract void Trigger();
    public abstract void Clear();
    public abstract void Show();
    public abstract void Init();
}

public class EmptyArea : Area
{
    public override void Clear()
    {
    }

    public override void Init()
    {
    }

    public override void Show()
    {
    }

    public override void Trigger()
    {
    }
}