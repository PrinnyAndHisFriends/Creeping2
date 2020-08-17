using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Area
{
    public Tile tile;
    public Tile back;
    public bool IsShowed { get; private set; }
    bool isTriggerred = false;
    public Area(Tile tile)
    {
        this.tile = tile;
        this.back = TileManager.Instance.back;
    }

    public virtual void Trigger(Vector3Int index)
    {
        if (!isTriggerred)
        {
            isTriggerred = true;
            OnTrigger(index);
        }
    }

    public virtual void ShowForward(Tilemap map, Vector3Int index)
    {
        IsShowed = true;
        map.SetTile(index, tile);
    }
    public virtual void ShowBack(Tilemap map, Vector3Int index)
    {
        IsShowed = false;
        map.SetTile(index, back);
    }

    protected abstract void OnTrigger(Vector3Int index);
    public abstract void Clear();
}

public class EmptyArea : Area
{
    public EmptyArea() : base(TileManager.Instance.empty) { }

    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class AntArea : Area
{
    public AntArea() : base(TileManager.Instance.ant) { }

    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        EntitySystem.Instance.GenerateEntity(Setting.EntityType.Ant, index);
    }
}

public class RotateArea : Area
{
    public RotateArea() : base(TileManager.Instance.rotate) { }

    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class ExchangeArea : Area
{
    public ExchangeArea() : base(TileManager.Instance.exchange) { }

    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class PresentArea : Area
{
    public PresentArea() : base(TileManager.Instance.present) { }

    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class HouseArea : Area
{
    public HouseArea() : base(TileManager.Instance.house) { }

    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        EntitySystem.Instance.GenerateEntity(Setting.EntityType.House, index);
    }
}

public class EpresentArea : Area
{
    public EpresentArea() : base(TileManager.Instance.epresent) { }

    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        EntitySystem.Instance.GenerateEntity(Setting.EntityType.EPresent, index);
    }
}

public class GrassArea : Area
{
    public GrassArea() : base(TileManager.Instance.grass) { }

    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class GapArea : Area
{
    public GapArea() : base(TileManager.Instance.gap) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

interface IWayArea
{

}

public class Way1_2Area : Area, IWayArea
{
    public Way1_2Area(Tile tile) : base(tile) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class Way1_3Area : Area, IWayArea
{
    public Way1_3Area(Tile tile) : base(tile) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class Way1_4Area : Area, IWayArea
{
    public Way1_4Area(Tile tile) : base(tile) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class Way1_2_4Area : Area, IWayArea
{
    public Way1_2_4Area(Tile tile) : base(tile) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class Way1_3_4Area : Area, IWayArea
{
    public Way1_3_4Area(Tile tile) : base(tile) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }

}

public class Way1_3_5Area : Area, IWayArea
{
    public Way1_3_5Area(Tile tile) : base(tile) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class Way1_3_4_6Area : Area, IWayArea
{
    public Way1_3_4_6Area(Tile tile) : base(tile) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}

public class Way1_2_3_4_5_6Area : Area, IWayArea
{
    public Way1_2_3_4_5_6Area(Tile tile) : base(tile) { }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
    }
}