using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Area
{
    public Tile tile;
    public Area(Tile tile)
    {
        this.tile = tile;
    }

    public abstract void Trigger();
    public abstract void Clear();
    public abstract void Show();
    public abstract void Init();
}

public class EmptyArea : Area
{
    public EmptyArea() : base(TileManager.Instance.empty) { }

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


public class GapArea : Area
{
    public GapArea(Tile tile) : base(tile) { }
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