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
        else
        {
            AreaSystem.Instance.OnTriggerAreaFinish();
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
        AreaSystem.Instance.OnTriggerAreaFinish();
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
        AreaSystem.Instance.OnTriggerAreaFinish();
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
        AreaSystem.Instance.SetMode(new RotateMode());
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
        AreaSystem.Instance.SetMode(new ExchangeMode());
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
        //AreaSystem.Instance.OnTriggerAreaFinish();
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
        AreaSystem.Instance.OnTriggerAreaFinish();
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
        AreaSystem.Instance.OnTriggerAreaFinish();
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
        AreaSystem.Instance.OnTriggerAreaFinish();
    }
}

interface IWayArea
{
    void Rotate();
}

public class Way1_2Area : Area, IWayArea
{
    int i;
    public Way1_2Area(Tile tile, int i) : base(tile) { this.i = i; }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        AreaSystem.Instance.OnTriggerAreaFinish();
    }
    public void Rotate()
    {
        i++;
        i %= 6;
        switch (i)
        {
            case 0:
                tile = TileManager.Instance.way1_2_a;
                break;
            case 1:
                tile = TileManager.Instance.way1_2_b;
                break;
            case 2:
                tile = TileManager.Instance.way1_2_c;
                break;
            case 3:
                tile = TileManager.Instance.way1_2_d;
                break;
            case 4:
                tile = TileManager.Instance.way1_2_e;
                break;
            case 5:
                tile = TileManager.Instance.way1_2_f;
                break;
            default:
                break;
        }
    }
}

public class Way1_3Area : Area, IWayArea
{
    int i;
    public Way1_3Area(Tile tile, int i) : base(tile) { this.i = i; }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        AreaSystem.Instance.OnTriggerAreaFinish();
    }


    public void Rotate()
    {
        i++;
        i %= 6;
        switch (i)
        {
            case 0:
                tile = TileManager.Instance.way1_3_a;
                break;
            case 1:
                tile = TileManager.Instance.way1_3_b;
                break;
            case 2:
                tile = TileManager.Instance.way1_3_c;
                break;
            case 3:
                tile = TileManager.Instance.way1_3_d;
                break;
            case 4:
                tile = TileManager.Instance.way1_3_e;
                break;
            case 5:
                tile = TileManager.Instance.way1_3_f;
                break;
            default:
                break;
        }
    }
}

public class Way1_4Area : Area, IWayArea
{
    int i;
    public Way1_4Area(Tile tile, int i) : base(tile) { this.i = i; }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        AreaSystem.Instance.OnTriggerAreaFinish();
    }
    public void Rotate()
    {
        i++;
        i %= 6;
        switch (i)
        {
            case 0:
                tile = TileManager.Instance.way1_4_a;
                break;
            case 1:
                tile = TileManager.Instance.way1_4_b;
                break;
            case 2:
                tile = TileManager.Instance.way1_4_c;
                break;
            case 3:
                tile = TileManager.Instance.way1_4_d;
                break;
            case 4:
                tile = TileManager.Instance.way1_4_e;
                break;
            case 5:
                tile = TileManager.Instance.way1_4_f;
                break;
            default:
                break;
        }
    }
}

public class Way1_2_4Area : Area, IWayArea
{
    int i;
    public Way1_2_4Area(Tile tile, int i) : base(tile) { this.i = i; }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        AreaSystem.Instance.OnTriggerAreaFinish();
    }
    public void Rotate()
    {
        i++;
        i %= 6;
        switch (i)
        {
            case 0:
                tile = TileManager.Instance.way1_2_4_a;
                break;
            case 1:
                tile = TileManager.Instance.way1_2_4_b;
                break;
            case 2:
                tile = TileManager.Instance.way1_2_4_c;
                break;
            case 3:
                tile = TileManager.Instance.way1_2_4_d;
                break;
            case 4:
                tile = TileManager.Instance.way1_2_4_e;
                break;
            case 5:
                tile = TileManager.Instance.way1_2_4_f;
                break;
            default:
                break;
        }
    }
}

public class Way1_3_4Area : Area, IWayArea
{
    int i;
    public Way1_3_4Area(Tile tile, int i) : base(tile) { this.i = i; }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        AreaSystem.Instance.OnTriggerAreaFinish();
    }
    public void Rotate()
    {
        i++;
        i %= 6;
        switch (i)
        {
            case 0:
                tile = TileManager.Instance.way1_3_4_a;
                break;
            case 1:
                tile = TileManager.Instance.way1_3_4_b;
                break;
            case 2:
                tile = TileManager.Instance.way1_3_4_c;
                break;
            case 3:
                tile = TileManager.Instance.way1_3_4_d;
                break;
            case 4:
                tile = TileManager.Instance.way1_3_4_e;
                break;
            case 5:
                tile = TileManager.Instance.way1_3_4_f;
                break;
            default:
                break;
        }
    }
}

public class Way1_3_5Area : Area, IWayArea
{
    int i;
    public Way1_3_5Area(Tile tile, int i) : base(tile) { this.i = i; }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        AreaSystem.Instance.OnTriggerAreaFinish();
    }
    public void Rotate()
    {
        i++;
        i %= 2;
        switch (i)
        {
            case 0:
                tile = TileManager.Instance.way1_3_5_a;
                break;
            case 1:
                tile = TileManager.Instance.way1_3_5_b;
                break;
            default:
                break;
        }
    }
}

public class Way1_3_4_6Area : Area, IWayArea
{
    int i;
    public Way1_3_4_6Area(Tile tile, int i) : base(tile) { this.i = i; }
    public override void Clear()
    {
    }

    protected override void OnTrigger(Vector3Int index)
    {
        AreaSystem.Instance.OnTriggerAreaFinish();
    }
    public void Rotate()
    {
        i++;
        i %= 3;
        switch (i)
        {
            case 0:
                tile = TileManager.Instance.way1_3_4_6_a;
                break;
            case 1:
                tile = TileManager.Instance.way1_3_4_6_b;
                break;
            case 2:
                tile = TileManager.Instance.way1_3_4_6_c;
                break;
            default:
                break;
        }
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
        AreaSystem.Instance.OnTriggerAreaFinish();
    }
    public void Rotate()
    {
    }
}